using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandwich : Dish
{
    public Sandwich() : base()
    {
        this.reward = 55;
        this.successProbability = 0.965f;
        this.sessionProbability = this.successProbability;

        //Ingredients
        GetIngredient get_ingredient = new GetIngredient();

        Ingredient ing1 = new Ingredient();
        ing1.ingredient = IngredientType.Bread;
        Ingredient ing2 = new Ingredient();
        ing2.ingredient = IngredientType.Ham;
        Ingredient ing3 = new Ingredient();
        ing3.ingredient = IngredientType.Cheese;

        get_ingredient.AddIngredient(ing1);
        get_ingredient.AddIngredient(ing2);
        get_ingredient.AddIngredient(ing3);

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

        Name = "Sandwich";




    }
}
