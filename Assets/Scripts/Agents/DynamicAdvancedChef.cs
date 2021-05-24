using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAdvancedChef : AdvancedChef
{
    [Range(0,100)]public float Threshold = 10;
    public bool dynamicBehaviour = false;
    [Range(0, 10)] public float riskChangeRate = 5.0f;
    public override void ChooseTask()
    {
        List<AdvancedChef> chefs = new List<AdvancedChef>(FindObjectsOfType<AdvancedChef>());
        foreach(AdvancedChef chef in chefs)
        {
            if(chef.gameObject.name != this.gameObject.name && (this.AccumulativeReward + Threshold) < chef.AccumulativeReward )
            {
                if (dynamicBehaviour)
                {
                    float changedBehaviour = Threshold / 2000;
                    this.RiskBehaviour -= changedBehaviour;
                }
                else
                {
                    this.RiskBehaviour -= riskChangeRate;
                }
                break;
            }
            else if(chef.gameObject.name != this.gameObject.name && this.AccumulativeReward  >= (chef.AccumulativeReward +Threshold))
            {
                if (dynamicBehaviour)
                {
                    float changedBehaviour = Threshold / 2000;
                    this.RiskBehaviour += changedBehaviour;
                }
                else
                {
                    this.RiskBehaviour += riskChangeRate;
                }
                break;
            }

        }
        RiskBehaviour = Mathf.Clamp(RiskBehaviour, 0, 1);
        base.ChooseTask();
    }

    public override void UpdateReward()
    {
        base.UpdateReward();
        text.text = "Dynamic Agent Reward: " + AccumulativeReward.ToString();
       
    }

}
