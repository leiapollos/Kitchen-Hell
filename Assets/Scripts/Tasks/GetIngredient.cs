using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetIngredient : Task
{
    /// <summary>
    /// List of Ingridients
    /// </summary>
    public List<Ingredient> ingredients;

    protected bool arrived = false;
    protected bool timerfinished = false;

    // Start is called before the first frame update
    public GetIngredient() : base()
    {
        type = TaskType.GetIngredient;
        timeToComplete = 2;
        ingredients = new List<Ingredient>();
        BeginStationType = StationType.WareHouseStation;
        EndStationType = StationType.CuttingStation;
    }

    /// <summary>
    /// Adds an Ingredient to the list of ingredients to get. Returns true if succeded, false if it already has too many ingredients.
    /// </summary>
    public bool AddIngredient(Ingredient ingredient_)
    {
        this.ingredients.Add(ingredient_);
        return true;
    }

    /// <summary>
    /// Executes the action of getting an Ingridient and returns true if succeded, false if failed.
    /// </summary>
    public override bool Execute(Chef chef)
    {
        if(!arrived || !timerfinished)
        {
            chef.agent.SetDestination(chef.BeginStation.transform.position);
            if (Vector3.Distance(chef.agent.transform.position, chef.BeginStation.transform.position) <= chef.stopDistance || arrived)
            {
                arrived = true;
                bool isFinish = timer.StartTimer();
                if (isFinish)
                {
                    timerfinished = true;
                    chef.HeldIngredients = new List<Ingredient>();
                    foreach (Ingredient i in ingredients)
                    {
                        chef.HeldIngredients.Add(i);
                    }
                    
                }
            }
        }
        else
        {
            chef.agent.SetDestination(chef.EndStation.transform.position);
            if (Vector3.Distance(chef.agent.transform.position, chef.EndStation.transform.position) <= chef.stopDistance)
            {
                //chef.EndStation.AddIngredient(ingredients);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Executes the action of getting an Ingridient and returns true if succeded, false if failed.
    /// </summary>
    public override bool Execute(AdvancedChef chef)
    {
        if (!arrived || !timerfinished)
        {
            chef.agent.SetDestination(chef.BeginStation.transform.position);
            if (Vector3.Distance(chef.agent.transform.position, chef.BeginStation.transform.position) <= chef.stopDistance || arrived)
            {
                arrived = true;
                bool isFinish = timer.StartTimer();
                if (isFinish)
                {
                    timerfinished = true;
                    chef.HeldIngredients = new List<Ingredient>();
                    foreach (Ingredient i in ingredients)
                    {
                        chef.HeldIngredients.Add(i);
                    }

                }
            }
        }
        else
        {
            chef.agent.SetDestination(chef.EndStation.transform.position);
            if (Vector3.Distance(chef.agent.transform.position, chef.EndStation.transform.position) <= chef.stopDistance)
            {
                //chef.EndStation.AddIngredient(ingredients);
                return true;
            }
        }
        return false;
    }
}