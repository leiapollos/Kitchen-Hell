using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Gordon : MonoBehaviour
{
    /// <summary>
    /// List of all dishes for a session
    /// </summary
    public LinkedList<Dish> Dishes; 
    public LinkedList<Dish> DishesToUpdate;
    

    /// <summary>
    /// List of all chefs in the kitchen
    /// </summary
    public List<Chef> Chefs;

    LinkedList<Task> HelpRequests;

    protected float AccumulativeReward = 0.0f;

    protected float timer = 0.0f;
    public float spawnDishTimer = 5.0f;

    public float sessionTime = 400.0f;
    public float startTime = 0;

    /// <summary>
    /// Time before reward of tasks starts to decline.
    /// </summary
    static float defaultGraceTimeForDishes = 65.0f;

    public float timeScale = 1.0f;
    public Text text;
    public Text timeText;
    public Text NumberOfDishesText;

    protected bool finished = false;
    protected string data;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
        AccumulativeReward = 0;
        Chefs = FindObjectsOfType<Chef>().ToList<Chef>();
        Dishes = new LinkedList<Dish>();
        DishesToUpdate = new LinkedList<Dish>();
        HelpRequests = new LinkedList<Task>();


        for(int i = 0; i < 5; i++)
        {
            AddNewDishToQueue();
        }
        Sort();
        data = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.startTime += Time.fixedDeltaTime;
        
        if (startTime < sessionTime)
        {
            timeText.text = "Time " + startTime.ToString();
            foreach (Chef chef in Chefs)
            {
                if (chef.isFree)
                {
                    AssignTask(chef);
                }

            }

            timer += Time.fixedDeltaTime;
            if (timer >= spawnDishTimer)
            {
                timer = 0.0f;
                AddNewDishToQueue();
            }

            //Update dishes timers:
            foreach (Dish d in DishesToUpdate)
            {
                if (d != null)
                {
                    d.UpdateTime();
                }
            }
            NumberOfDishesText.text = "Dishes: " + Dishes.Count.ToString();
        }
        else
        {
            if (!finished)
            {
                foreach (Dish d in Dishes)
                {
                    AccumulativeReward -= 10.0f * (d.GetStopwatchTime() / defaultGraceTimeForDishes);
                    text.text = "Reward: " + AccumulativeReward.ToString();
                }
                finished = true;
                string destination = Application.dataPath + "/save.txt";
                FileStream file;

                if (File.Exists(destination)) file = File.OpenWrite(destination);
                else file = File.Create(destination);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, data);

                file.Close();
            }
        }
        

    }

    protected void AddNewDishToQueue()
    {
        Dish dish = GenerateDish();
        dish.Scenario1();
        Dishes.AddLast(dish);
        DishesToUpdate.AddLast(dish);
    }

    protected Dish GenerateDish()
    {
        float ran = Random.Range(0.0f,100.0f);
        if(ran < 8.3f)
        {
            return new EggsWithVegetables();
        }
        else if (ran < 16f)
        {
            return new TunaPasta();
        }
        if (ran < 25f)
        {
            return new ChickenWithRice();
        }
        if (ran < 33.3f)
        {
            return new FriedShrimps();
        }
        if (ran < 41.6f)
        {
            return new GourmetHamburger();
        }
        if (ran < 50)
        {
            return new Lasagna();
        }
        if (ran < 58.3)
        {
            return new OmeletteCheese();
        }
        if (ran < 66.6)
        {
            return new SalmonWithPotatoes();
        }
        if (ran < 75)
        {
            return new Sandwich();
        }
        if (ran < 83.3)
        {
            return new ScrambledEggs();
        }
        if (ran < 91.6)
        {
            return new Sushi();
        }
        if (ran <= 100)
        {
            return new TacosAlPastor();
        }
        return new TunaPasta();
    }


    public void AssignTask(Chef chef)
    {
        Sort();

        if(Dishes.Count > 0)
        {
            chef.isFree = false;
            chef.CurrentRequest = Dishes.First();
            Dishes.RemoveFirst();
        }
        else
        {
            Debug.Log("No more dishes!");
        }
        //chef.CurrentRequest = HelpRequests.First();
        //HelpRequests.RemoveFirst();
    }

    public void UpdateReward(Dish dish)
    {
        if(dish.GetStopwatchTime() < defaultGraceTimeForDishes)
        {
            AccumulativeReward += dish.reward;
        }
        else
        {
            if(dish.GetStopwatchTime() < 2 * defaultGraceTimeForDishes)
            {
                float reward = dish.reward * (1.0f - ((dish.GetStopwatchTime() % defaultGraceTimeForDishes) / defaultGraceTimeForDishes));
                AccumulativeReward += reward;
            }
            else
            {
                //Time exceeded. Reward is negative
                AccumulativeReward -= 10.0f * (dish.GetStopwatchTime()/defaultGraceTimeForDishes);
            }
        }
        text.text = "Reward: " + AccumulativeReward.ToString();

        data += AccumulativeReward.ToString() + "\t : " + startTime + "\t : " + dish.Name + "\n";
        Debug.Log(AccumulativeReward.ToString() + " : " + startTime + " : " + dish.Name);
    }

    public void Sort()
    {
        //Comparator do sorting
        GFG gg = new GFG();

        //Transform linked lsit in list to be able to sort
        List<Dish> temp = new List<Dish>();
        foreach(Dish d in Dishes)
        {
            temp.Add(d);
        }
        int i = 1;
        foreach (Dish d in temp)
        {
            Debug.Log(d.Name + i++);
        }
        //Sort list
        temp.Sort(gg);
        foreach (Dish d in temp)
        {
            Debug.Log(d.Name + i++);
        }
        //Convert back to list
        Dishes = new LinkedList<Dish>();
        foreach(Dish d in temp)
        {
            Dishes.AddLast(d);
        }
    }

    //Sorter based on reward per time
    class GFG : IComparer<Dish>
    {
        public int Compare(Dish x, Dish y)
        {
            float xTime = x.GetRemainingTasksTime();
            float yTime = y.GetRemainingTasksTime();
            float res = ((x.reward / xTime) - (y.reward / yTime));
            return res > 0 ? 1 : -1;
        }
    }

    //Sorter based on time left
    class GFG2 : IComparer<Dish>
    {
        public int Compare(Dish x, Dish y)
        {
            float xTime = x.GetRemainingTasksTime();
            float yTime = y.GetRemainingTasksTime();
            if ((defaultGraceTimeForDishes - x.stopwatch + xTime) > (defaultGraceTimeForDishes - y.stopwatch + yTime))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    //Sorter based on ratio between time and reward
    class GFG3 : IComparer<Dish>
    {
        public int Compare(Dish x, Dish y)
        {
            float xTime = x.GetRemainingTasksTime();
            float yTime = y.GetRemainingTasksTime();
            xTime = defaultGraceTimeForDishes - x.stopwatch + xTime;
            yTime = defaultGraceTimeForDishes - y.stopwatch + yTime;
            if(xTime >= 1 && yTime >= 1)
            {
                float res = ((x.reward / xTime) - (y.reward / yTime));
                return res > 0 ? 1 : -1;
            }
            else
            {
                if(xTime < 1 && yTime < 1)
                {
                    float res = (x.reward / x.GetRemainingTasksTime() - y.reward / y.GetRemainingTasksTime());
                    return res > 0 ? 1 : -1;
                }
                if(xTime <= 1)
                {
                    return 1;
                }
                if(yTime <= 1)
                {
                    return -1;
                }
            }
            return 1;
        }
    }
}
