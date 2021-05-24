using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : Request
{

    // Start is called before the first frame update
    public int reward;
    public float timeToComplete = 0f;
    public float stopwatch = 0.0f;

    public float successProbability;
    public float sessionProbability;

    protected float defaultOverallTime = 20.0f;
    //public int dificulty;
    public Dish() : base()
    {
        Name = "DISH";
        timeToComplete = GetRemainingTasksTime();
        stopwatch = 0.0f;
    }

    // Update is called once per frame
    public void UpdateTime()
    {
        stopwatch += Time.fixedDeltaTime;
    }

    public float GetStopwatchTime()
    {
        return stopwatch;
    }

    public void Scenario1()
    {
        Tasks get_ingredient_list = this.tasks[0] as Tasks;
        GetIngredient get_ingredient = get_ingredient_list.tasks[0] as GetIngredient;
        float timetocompleteCut = 0.0f;
        float timetocompleteCook = 0.0f;
        float timetocompleteAssemble = 0.0f;
        foreach (Ingredient i in get_ingredient.ingredients)
        {
            timetocompleteCut += i.TimeToCut();
            timetocompleteCook += i.TimeToBoil();
            timetocompleteAssemble += i.TimeToAssemble();
        }

        Tasks cut_list = this.tasks[1] as Tasks;
        CutIngredient cut = cut_list.tasks[0] as CutIngredient;
        cut.timeToComplete = timetocompleteCut;

        Tasks cook_list = this.tasks[2] as Tasks;
        CookIngredients cook = cook_list.tasks[0] as CookIngredients;
        cook.timeToComplete = timetocompleteCook;

        Tasks assembly_list = this.tasks[3] as Tasks;
        AssembleDish assemble = assembly_list.tasks[0] as AssembleDish;
        assemble.timeToComplete = timetocompleteAssemble;
    }
}
