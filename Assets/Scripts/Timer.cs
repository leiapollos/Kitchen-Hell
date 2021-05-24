using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    /// <summary>
    /// Value for the timer to start counting from. Default value is 5 seconds;
    /// </summary>
    protected float maxTime = 3f;

    /// <summary>
    /// Ttime left to reach 0.
    /// </summary>
    protected float time;

    /// <summary>
    /// If the timer is active it's true, otherwise it's false. Default value is false.
    /// </summary>
    protected bool isActive = false;


    protected bool finished = false;

    // Start is called before the first frame update
    public Timer()
    {
        time = maxTime;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (isActive)
        {
            if (time - Time.deltaTime < 0)
            {
                time = 0.0f;
                isActive = false;
            }
            else
                time -= Time.deltaTime;
        }
    }*/

    /// <summary>
    /// Starts the timer. Returns true if succeeded, False if failed. (It fails if the timer is already running)
    /// </summary>
    public bool StartTimer()
    {
        if (finished)
            return true;

        if (isActive)
        {
            if (time - Time.fixedDeltaTime < 0)
            {
                time = 0.0f;
                isActive = false;
                finished = true;
            }
            else
                time -= Time.fixedDeltaTime;
        }

        if (isActive)
            return false;

        isActive = true;
        time = maxTime;

        return false;
    }

    /// <summary>
    /// Returns the time left for the timer to end. Returns 0 if the timer is not active.
    /// </summary>
    public float GetTimeLeft()
    {
        return time;
    }

    /// <summary>
    /// Returns true if the timer is active. False othewise.
    /// </summary>
    public bool IsTimerActive()
    {
        return isActive;
    }

    /// <summary>
    /// Sets the max time for the countdown.
    /// </summary>
    public void SetMaxTime(float maxTime_)
    {
        maxTime = maxTime_;
    }
}
