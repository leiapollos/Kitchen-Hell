using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambledEggs : Dish
{
    public ScrambledEggs() : base()
    {
        this.reward = 50;
        this.successProbability = 1.0f;
        this.sessionProbability = this.successProbability;

        //Ingredients
        GetIngredient get_ingredient = new GetIngredient();

        Ingredient egg1 = new Ingredient();
        egg1.ingredient = IngredientType.Egg;
        Ingredient egg2 = new Ingredient();
        egg2.ingredient = IngredientType.Egg;

        get_ingredient.AddIngredient(egg1);
        get_ingredient.AddIngredient(egg2);

        //Actions
        Tasks listOfActions = new Tasks();
        listOfActions.AddRequest(get_ingredient);

        ///////////////////////////////////////////////////////

        CutIngredient cut = new CutIngredient();
        cut.UpdateTimer(defaultOverallTime * 0.3f);

        //Actions
        Tasks listOfActions2 = new Tasks();
        listOfActions2.AddRequest(cut);

        ///////////////////////////////////////////////////////

        CookIngredients cook = new CookIngredients();
        cook.UpdateTimer(defaultOverallTime * 0.4f);

        //Actions
        Tasks listOfActions3 = new Tasks();
        listOfActions3.AddRequest(cook);

        ///////////////////////////////////////////////////////

        AssembleDish assemble = new AssembleDish();
        assemble.UpdateTimer(defaultOverallTime * 0.2f);

        //Actions
        Tasks listOfActions4 = new Tasks();
        listOfActions4.AddRequest(assemble);

        ///////////////////////////////////////////////////////

        //Add actions
        AddRequest(listOfActions);
        AddRequest(listOfActions2);
        AddRequest(listOfActions3);
        AddRequest(listOfActions4);

        Name = "ScrambledEggs";
    }
}
