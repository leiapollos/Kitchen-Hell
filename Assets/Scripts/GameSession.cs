using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    /// <summary>
    /// List of all dishes for a session
    /// </summary
    public LinkedList<Dish> Dishes;

    protected float timer = 0.0f;


    public float timeScale = 1.0f;

    public float timeSession = 400.0f;

    float time = 0;

    public Text timeText;

    [Range(0,1)] public float LearningRate = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
        Dishes = new LinkedList<Dish>();
       
        AddNewDishToQueue();
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time <= timeSession)
        {
            timeText.text = "Time " + time;
        }
    }

    protected void AddNewDishToQueue()
    {
        Dishes.AddLast(new ChickenWithRice());
        Dishes.AddLast(new FriedShrimps());
        Dishes.AddLast(new GourmetHamburger());
        Dishes.AddLast(new Lasagna());
        Dishes.AddLast(new OmeletteCheese());
        Dishes.AddLast(new SalmonWithPotatoes());
        Dishes.AddLast(new Sandwich());
        Dishes.AddLast(new ScrambledEggs());
        Dishes.AddLast(new Sushi());
        Dishes.AddLast(new TacosAlPastor());
    }

    public void UpdateDish(Dish dish, int totalTask, int successTask)
    {
        foreach (Dish currDish in Dishes)
        {
            
            if (currDish.Name == dish.Name)
            {
                float successRate = successTask / totalTask;
                currDish.sessionProbability = currDish.sessionProbability + LearningRate * (successRate - currDish.sessionProbability);
                currDish.sessionProbability = Mathf.Clamp(currDish.sessionProbability, 0, 1);
                return;
            }
        }
    }



}
