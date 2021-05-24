using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunaPasta : Dish
{
    // Start is called before the first frame update
    public TunaPasta() : base()
    {
        this.reward = 50;
        this.successProbability = 0.8f;
        this.sessionProbability = this.successProbability;
        //Ingredients
        GetIngredient get_ingredient = new GetIngredient();

        Ingredient tuna = new Ingredient();
        tuna.ingredient = IngredientType.Tuna;
        Ingredient pasta = new Ingredient();
        pasta.ingredient = IngredientType.Pasta;

        get_ingredient.AddIngredient(tuna);
        get_ingredient.AddIngredient(pasta);

        //Actions
        Tasks listOfActions = new Tasks();
        listOfActions.AddRequest(get_ingredient);

        ///////////////////////////////////////////////////////

        CutIngredient cut = new CutIngredient();
        cut.UpdateTimer(tuna.TimeToCut() + pasta.TimeToCut());

        //Actions
        Tasks listOfActions2 = new Tasks();
        listOfActions2.AddRequest(cut);

        ///////////////////////////////////////////////////////

        CookIngredients cook = new CookIngredients();
        cook.UpdateTimer(tuna.TimeToBoil() + pasta.TimeToBoil());

        //Actions
        Tasks listOfActions3 = new Tasks();
        listOfActions3.AddRequest(cook);

        ///////////////////////////////////////////////////////

        AssembleDish assemble = new AssembleDish();
        assemble.UpdateTimer(tuna.TimeToAssemble() + pasta.TimeToAssemble());

        //Actions
        Tasks listOfActions4 = new Tasks();
        listOfActions4.AddRequest(assemble);

        ///////////////////////////////////////////////////////

        //Add actions
        AddRequest(listOfActions);
        AddRequest(listOfActions2);
        AddRequest(listOfActions3);
        AddRequest(listOfActions4);

        Name = "TunaPasta";
    }
}
