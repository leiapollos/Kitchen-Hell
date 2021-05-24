using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Abstract class for the atomic tasks to inherit from.
/// </summary>
public abstract class Task : Request
{
    /// <summary>
    /// Enum of possible types of tasks.
    /// </summary>
    protected enum TaskType { GetIngredient, Cut, Boil, Bake, Fry, GetPlate, Assemble }

    /// <summary>
    /// Current type of the task.
    /// </summary>
    protected TaskType type;

    /// <summary>
    /// Timer for the task.
    /// </summary>
    protected Timer timer;

    /// <summary>
    /// Time to complete in seconds
    /// </summary>
    public float timeToComplete = 5;

    /// <summary>
    /// Station to execute task.
    /// </summary>
    public StationType BeginStationType;
    public StationType EndStationType;

    public bool succeed = false;


    // Start is called before the first frame update
    public Task() : base()
    {
        timer = new Timer();
        timer.SetMaxTime(timeToComplete);
    }

    /// <summary>
    /// Executes the current task and returns true if succeded, false if failed.
    /// </summary>
    public virtual bool Execute(Chef agent)
    {
            return true;
    }

    /// <summary>
    /// Executes the current task and returns true if succeded, false if failed.
    /// </summary>
    public virtual bool Execute(AdvancedChef agent)
    {
        return true;
    }


    /// <summary>
    /// Returns the number of remaining tasks. Since it is an atomic task it returns 1.
    /// </summary>
    public override int GetNumberOfRemainingTasks()
    {
        return 1;
    }

    /// <summary>
    /// Returns the time to complete the task.
    /// </summary>
    public override float GetRemainingTasksTime()
    {
        return timeToComplete;
    }

    public void UpdateTimer(float newTimeToComplete)
    {
        timeToComplete = newTimeToComplete;
        timer.SetMaxTime(timeToComplete);
    }
}
