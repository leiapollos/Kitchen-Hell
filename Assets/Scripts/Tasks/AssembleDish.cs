using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleDish : Task
{
    public AssembleDish() : base()
    {
        type = TaskType.Assemble;
        timeToComplete = 4;
        BeginStationType = StationType.AssemblingStation;
        EndStationType = StationType.DeliverStation;
        Name = "assemble";
    }

    public override bool Execute(Chef chef)
    {
        bool isFinish = timer.StartTimer();
        if (isFinish)
        {
            foreach (Ingredient i in chef.HeldIngredients)
            {
                i.state = IngredientState.Assembled;
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
                foreach (Ingredient i in chef.HeldIngredients)
                {
                    i.state = IngredientState.Assembled;
                }

                succeed = true;
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
