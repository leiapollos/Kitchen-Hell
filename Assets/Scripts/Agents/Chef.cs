using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chef : MonoBehaviour
{
    /// <summary>
    /// Head Chef, gives tasks and receives help requests
    /// </summary
    Gordon GordonRamsay;

    /// <summary>
    /// The current task the chef is executing
    /// </summary
    public Request CurrentRequest = null;
    public Tasks CurrentTasks = null;
    public Task CurrentTask = null;

    public List<Request> helpTask;

    /// <summary>
    /// Ingredients the chef is currently carrying. 
    /// </summary
    public List<Ingredient> HeldIngredients;


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


    public bool isFree = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = stopDistance - 1.0f;
        isFree = true;
        GordonRamsay = GameObject.FindGameObjectWithTag("Gordon").GetComponent<Gordon>();
        HeldIngredients = new List<Ingredient>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentRequest != null)
        {
            Dish dish = CurrentRequest as Dish;
            if (BeginStation == null || EndStation == null)
            {
                CurrentTasks = dish.GetCurrentRequest() as Tasks;
                CurrentTask = CurrentTasks.GetCurrentRequest() as Task;
                ReserveBeginStation(CurrentTask.BeginStationType);
                ReserveEndStation(CurrentTask.EndStationType);
            }
            if(BeginStation == null || EndStation == null)
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
                        GordonRamsay.UpdateReward(dish);
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
        }

    }

    protected bool ReserveBeginStation(StationType type)
    {
        if(BeginStation != null)
        {
            return true;
        }
        List<Station> stations = new List<Station>(FindObjectsOfType<Station>());
        foreach (Station st in stations)
        {
            if (st.type == type && st.IsReserved() == false)
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
            if (st.type == type && st.IsReserved() == false)
            {
                st.Reserve();
                EndStation = st;
                return true;
            }
        }
        return false;
    }


    
}
