using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not and interface because all this functions can be shared by dish and task
public abstract class Request
{
    /// <summary>
    /// Name of the Dish/Tasks
    /// </summary>
    public string Name;

    /// <summary>
    /// List of tasks
    /// </summary>
    public List<Request> tasks;

    /// <summary>
    /// Index of current task. -1 if list of tasks is empty
    /// </summary>
    protected int CurrentTask = 0;

    /// <summary>
    /// Time left for the tasks not yet executed/executing. (it excludes the current task)
    /// </summary>
    protected float timeOfRemainingTasks = 0.0f;

    // Start is called before the first frame update
    public Request()
    {
        Name = "REQUEST";
        tasks = new List<Request>();

    }

    /// <summary>
    /// Returns the next task in the list. null if list is empty or no tasks remaining.
    /// </summary>
    public Request GetNextRequest()
    {
        if (tasks.Count <= CurrentTask + 1)
        {
            CurrentTask++;
            return null;
        }
        else
        {
            CurrentTask++;
            return tasks[CurrentTask];
        }
    }

    /// <summary>
    /// Returns the next task in the list. null there is no current task.
    /// </summary>
    public Request GetCurrentRequest()
    {
        if (tasks.Count >= 0 && CurrentTask < tasks.Count)
            return tasks[CurrentTask];
        else
            return null;
    }

    /// <summary>
    /// Returns the ramaining tasks, excluding the current one. Returns empty list if there are no remaining tasks.
    /// </summary>
    public List<Request> GetRemainingTasks()
    {
        List<Request> remaining = new List<Request>();
        if (tasks.Count == 0 || CurrentTask + 1 == tasks.Count) //Actually not needed, because the for loop already does this. But it makes it easier to read
            return remaining;
        for(int i = CurrentTask; i < tasks.Count; i++)
        {
            remaining.Add(tasks[i]);
        }
        return remaining;
    }

    /// <summary>
    /// Returns the number of ramaining tasks, excluding the current one. Returns 0 if there are no remaining tasks.
    /// </summary>
    public virtual int GetNumberOfRemainingTasks()
    {
        int total = 0;
        foreach(Request r in tasks)
        {
            total += r.GetNumberOfRemainingTasks();
        }
        return total;
    }

    /// <summary>
    /// Returns the time left for the remaining tasks, excluding the current one. Returns 0 if there are no remaining tasks.
    /// </summary>
    public virtual float GetRemainingTasksTime()
    {
        float total = 0;
        foreach (Request r in tasks)
        {
            total += r.GetRemainingTasksTime();
        }
        return total;
    }

    /// <summary>
    /// Adds an Request to the end of the list
    /// </summary>
    public virtual void AddRequest(Request request)
    {
        tasks.Add(request);
    }

    /// <summary>
    /// Executes the current task and returns true if succeded, false if failed.
    /// </summary>
    public virtual bool Execute()
    {
        Request currentRequest = GetCurrentRequest();
        if (currentRequest == null) return false;
        return currentRequest.Execute();
    }

}
