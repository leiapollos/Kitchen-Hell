using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class AdvancedChef : MonoBehaviour
{
    /// <summary>
    /// Head Chef, gives tasks and receives help requests
    /// </summary
    GameSession Session; 
    
    /// <summary>
    /// Time before reward of tasks starts to decline.
    /// </summary
    static float defaultGraceTimeForDishes = 90.0f;
    public float AccumulativeReward = 0.0f;
    public Text text;

    /// <summary>
    /// The current task the chef is executing
    /// </summary
    public Request CurrentRequest = null;
    public Tasks CurrentTasks = null;
    public Task CurrentTask = null;

    public int NumberOfTotalTask;
    public int NumberOfSucessTask;

    public List<Request> helpTask;

    /// <summary>
    /// Ingredients the chef is currently carrying. 
    /// </summary
    public List<Ingredient> HeldIngredients;

    [Range(0,1)]public float RiskBehaviour;


    /// <summary>
    /// Station to execute task.
    /// </summary>
    public Station BeginStation;
    public Station EndStation;

    /// <summary>
    /// Maximum time the chef can wait at a station without working
    /// </summary
    float MaxWaitTime;

    [HideInInspector] public NavMeshAgent agent;
    public float moveSpeed = 3.0f;
    public float stopDistance = 2.0f;
    public Vector3 destination;

    public float time = 0; 


    public bool isFree = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = stopDistance - 1.0f;
        isFree = true;
        Session = GameObject.FindGameObjectWithTag("GameSession").GetComponent<GameSession>();
        HeldIngredients = new List<Ingredient>();
        NumberOfSucessTask = 0;
        NumberOfTotalTask = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (CurrentRequest != null)
        {
           // Debug.Log(CurrentRequest.Name);
            Dish dish = CurrentRequest as Dish;
            if (BeginStation == null || EndStation == null)
            {
                CurrentTasks = dish.GetCurrentRequest() as Tasks;
                CurrentTask = CurrentTasks.GetCurrentRequest() as Task;
                ReserveBeginStation(CurrentTask.BeginStationType);
                ReserveEndStation(CurrentTask.EndStationType);
            }
            if (BeginStation == null || EndStation == null)
            {
                return;
            }

            CurrentTask = CurrentTasks.GetCurrentRequest() as Task;
            bool isFinish = CurrentTask.Execute(this);
            if (isFinish)
            {
                CurrentTask = CurrentTasks.GetNextRequest() as Task;
                if (CurrentTask == null)
                {
                    CurrentTasks = CurrentRequest.GetNextRequest() as Tasks;
                    if (CurrentTasks == null)
                    {
                        UpdateReward();
                        BeginStation.StopReservation();
                        EndStation.StopReservation();
                        BeginStation = null;
                        EndStation = null;
                        CurrentRequest = null;
                        return;
                    }
                    CurrentTask = CurrentTasks.GetCurrentRequest() as Task;
                }
                BeginStation.StopReservation();
                BeginStation = EndStation;
                ReserveEndStation(CurrentTask.EndStationType);
                EndStation = null;
            }
        }
        else
        {
            isFree = true;
            if(time <= Session.timeSession)
            {
                ChooseTask();
            }

        }

    }

    public virtual void ChooseTask()
    {

        LinkedList<Dish> Dishes = Session.Dishes;
        float dif = Mathf.Infinity;
        foreach (Dish dish in Dishes)
        {
            float currDif = Math.Abs(dish.sessionProbability - RiskBehaviour);
            if (currDif < dif)
            {
                CurrentRequest = dish;
                dif = currDif;
            }
        }
        switch(CurrentRequest.Name)
        {
            case ("ChickenWithRice"):
                CurrentRequest = new ChickenWithRice();
                break;
            case ("FriedShrimps"):
                CurrentRequest = new FriedShrimps();
                break;
            case ("GourmetHamburger"):
                CurrentRequest = new GourmetHamburger();
                break;
            case ("Lasagna"):
                CurrentRequest = new Lasagna();
                break;
            case ("OmeletteCheese"):
                CurrentRequest = new OmeletteCheese();
                break;
            case ("SalmonWithPotatoes"):
                CurrentRequest = new SalmonWithPotatoes();
                break;
            case ("Sandwich"):
                CurrentRequest = new Sandwich();
                break;
            case ("ScrambledEggs"):
                CurrentRequest = new ScrambledEggs();
                break;
            case ("Sushi"):
                CurrentRequest = new Sushi();
                break;
            case ("TacosAlPastor"):
                CurrentRequest = new TacosAlPastor();
                break;
            default:
                break;
        }
        //Debug.Log(CurrentRequest.Name);

    }

    protected bool ReserveBeginStation(StationType type)
    {
        if (BeginStation != null)
        {
            return true;
        }
        List<Station> stations = new List<Station>(FindObjectsOfType<Station>());
        foreach (Station st in stations)
        {
            if (st.type == type)
            {
                st.Reserve();
                BeginStation = st;
                return true;
            }
        }
        return false;
    }

    protected bool ReserveEndStation(StationType type)
    {
        if (EndStation != null)
        {
            return true;
        }
        List<Station> stations = new List<Station>(FindObjectsOfType<Station>());
        foreach (Station st in stations)
        {
            if (st.type == type)
            {
                st.Reserve();
                EndStation = st;
                return true;
            }
        }
        return false;
    }

    public virtual void UpdateReward()
    {
        Dish dish = CurrentRequest as Dish;
        
        AccumulativeReward += dish.reward;
        Session.UpdateDish(dish, NumberOfTotalTask, NumberOfSucessTask);
        NumberOfTotalTask = 0;
        NumberOfSucessTask = 0;


        text.text = "Static Agent Reward: " + AccumulativeReward.ToString();
    }
}
