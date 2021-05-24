using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookIngredients : Task
{
    // Start is called before the first frame update
    public CookIngredients() : base()
    {
        type = TaskType.Boil;
        timeToComplete = 4;
        BeginStationType = StationType.CookingStation;
        EndStationType = StationType.AssemblingStation;
        Name = "cook";
    }

    public override bool Execute(Chef chef)
    {
        bool isFinish = timer.StartTimer();
        if (isFinish)
        {
            foreach (Ingredient i in chef.HeldIngredients)
            {
                i.state = IngredientState.Cooked;
            }

            chef.agent.SetDestination(chef.EndStation.transform.position);

            if (Vector3.Distance(chef.agent.transform.position, chef.EndStation.transform.position) <= chef.stopDistance)
            {
                return true;
            }
        }
        return false;
    }

    public override bool Execute(AdvancedChef chef)
    {
        if (!succeed)
        {
            bool isFinish = timer.StartTimer();
            if (isFinish)
            {
                chef.NumberOfTotalTask++;
                float random = Random.Range(0.0f, 1.0f);
                Dish dish = chef.CurrentRequest as Dish;
                if (random > dish.successProbability)
                {
                    Debug.Log("Failed!" + random + " : " + dish.successProbability);
                    timer = new Timer();
                    timer.SetMaxTime(timeToComplete);
                    return false;
                }
                chef.NumberOfSucessTask++;
                succeed = true;
                foreach (Ingredient i in chef.HeldIngredients)
                {
                    i.state = IngredientState.Cooked;
                }
            }
        }
       
        else
        {
            chef.agent.SetDestination(chef.EndStation.transform.position);

            if (Vector3.Distance(chef.agent.transform.position, chef.EndStation.transform.position) <= chef.stopDistance)
            {
                return true;
            }
        }
        return false;
    }
}
