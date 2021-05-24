using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsWithVegetables : Dish
{
    // Start is called before the first frame update
    public EggsWithVegetables() : base()
    {
        this.reward = 50;
        this.successProbability = 0.6f;
        this.sessionProbability = this.successProbability;
        //Ingredients
        GetIngredient get_ingredient = new GetIngredient();

        Ingredient eggs = new Ingredient();
        eggs.ingredient = IngredientType.Tuna;
        Ingredient veggies = new Ingredient();
        veggies.ingredient = IngredientType.Vegetables;


        get_ingredient.AddIngredient(eggs);
        get_ingredient.AddIngredient(veggies);

        //Actions
        Tasks listOfActions = new Tasks();
        listOfActions.AddRequest(get_ingredient);

        ///////////////////////////////////////////////////////

        CutIngredient cut = new CutIngredient();
        cut.UpdateTimer(eggs.TimeToCut() + veggies.TimeToCut());

        //Actions
        Tasks listOfActions2 = new Tasks();
        listOfActions2.AddRequest(cut);

        ///////////////////////////////////////////////////////

        CookIngredients cook = new CookIngredients();
        cook.UpdateTimer(eggs.TimeToBoil() + veggies.TimeToBoil());

        //Actions
        Tasks listOfActions3 = new Tasks();
        listOfActions3.AddRequest(cook);

        ///////////////////////////////////////////////////////

        AssembleDish assemble = new AssembleDish();
        assemble.UpdateTimer(eggs.TimeToAssemble() + veggies.TimeToAssemble());

        //Actions
        Tasks listOfActions4 = new Tasks();
        listOfActions4.AddRequest(assemble);

        ///////////////////////////////////////////////////////

        //Add actions
        AddRequest(listOfActions);
        AddRequest(listOfActions2);
        AddRequest(listOfActions3);
        AddRequest(listOfActions4);

        Name = "EggsWithVegetables";
    }

}
