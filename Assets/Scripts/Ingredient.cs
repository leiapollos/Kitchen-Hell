using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    /// <summary>
    /// Ingredient state.
    /// </summary>
    public IngredientState state;

    /// <summary>
    /// Ingredient type.
    /// </summary>
    public IngredientType ingredient;

    // Start is called before the first frame update
    public Ingredient()
    {
        state = IngredientState.Default;
    }

    public float TimeToCut()
    {
        Dictionary<IngredientType, float> timeToCut = new Dictionary<IngredientType, float>();
        timeToCut.Add(IngredientType.Vegetables, 5.0f);
        timeToCut.Add(IngredientType.Egg, 1.0f);
        timeToCut.Add(IngredientType.Pasta, 1.0f);
        timeToCut.Add(IngredientType.Tuna, 0.0f);
        timeToCut.Add(IngredientType.Tomato, 2.5f);
        timeToCut.Add(IngredientType.GroundMeat, 3.5f);
        timeToCut.Add(IngredientType.Bun, 0.5f);
        timeToCut.Add(IngredientType.Rice, 0.0f);
        timeToCut.Add(IngredientType.Salmon, 3.5f);
        timeToCut.Add(IngredientType.Chicken, 2.5f);
        timeToCut.Add(IngredientType.Potato, 1.5f);
        timeToCut.Add(IngredientType.Shrimp, 2.5f);
        timeToCut.Add(IngredientType.SoySauce, 0.0f);
        timeToCut.Add(IngredientType.Bread, 0.5f);
        timeToCut.Add(IngredientType.Ham, 0.5f);
        timeToCut.Add(IngredientType.Cheese, 0.5f);
        timeToCut.Add(IngredientType.Meat, 2.0f);
        timeToCut.Add(IngredientType.Onion, 2.0f);
        timeToCut.Add(IngredientType.Fajita, 0.5f);
        timeToCut.Add(IngredientType.LasagnaNoodles, 0.5f);
        timeToCut.Add(IngredientType.Seaweed, 3.5f);

        return timeToCut[ingredient];
    }

    public float TimeToBoil()
    {
        Dictionary<IngredientType, float> timeToBoil = new Dictionary<IngredientType, float>();
        timeToBoil.Add(IngredientType.Vegetables, 4.0f);
        timeToBoil.Add(IngredientType.Egg, 1.0f);
        timeToBoil.Add(IngredientType.Pasta, 5.0f);
        timeToBoil.Add(IngredientType.Tuna, 1.0f);
        timeToBoil.Add(IngredientType.Tomato, 2.0f);
        timeToBoil.Add(IngredientType.GroundMeat, 1.5f);
        timeToBoil.Add(IngredientType.Bun, 0.5f);
        timeToBoil.Add(IngredientType.Rice, 2.0f);
        timeToBoil.Add(IngredientType.Salmon, 0.5f);
        timeToBoil.Add(IngredientType.Chicken, 1.5f);
        timeToBoil.Add(IngredientType.Potato, 2.0f);
        timeToBoil.Add(IngredientType.Shrimp, 2.5f);
        timeToBoil.Add(IngredientType.SoySauce, 0.5f);
        timeToBoil.Add(IngredientType.Bread, 0.5f);
        timeToBoil.Add(IngredientType.Ham, 0.0f);
        timeToBoil.Add(IngredientType.Cheese, 0.5f);
        timeToBoil.Add(IngredientType.Fajita, 1.5f);
        timeToBoil.Add(IngredientType.Meat, 2.0f);
        timeToBoil.Add(IngredientType.Onion, 0.5f);
        timeToBoil.Add(IngredientType.LasagnaNoodles, 1.5f);
        timeToBoil.Add(IngredientType.Seaweed, 0.0f);

        return timeToBoil[ingredient];
    }

    public float TimeToAssemble()
    {
        Dictionary<IngredientType, float> timeToAssemble = new Dictionary<IngredientType, float>();
        timeToAssemble.Add(IngredientType.Vegetables, 2.0f);
        timeToAssemble.Add(IngredientType.Egg, 0.5f);
        timeToAssemble.Add(IngredientType.Pasta, 1.0f);
        timeToAssemble.Add(IngredientType.Tuna, 0.5f);
        timeToAssemble.Add(IngredientType.Tomato, 1.5f);
        timeToAssemble.Add(IngredientType.GroundMeat, 0.5f);
        timeToAssemble.Add(IngredientType.Bun, 0.5f);
        timeToAssemble.Add(IngredientType.Rice, 0.5f);
        timeToAssemble.Add(IngredientType.Salmon, 1.5f);
        timeToAssemble.Add(IngredientType.Chicken, 0.5f);
        timeToAssemble.Add(IngredientType.Potato, 0.5f);
        timeToAssemble.Add(IngredientType.Shrimp, 1.5f);
        timeToAssemble.Add(IngredientType.SoySauce, 0.0f);
        timeToAssemble.Add(IngredientType.Bread, 0.5f);
        timeToAssemble.Add(IngredientType.Ham, 0.5f);
        timeToAssemble.Add(IngredientType.Cheese, 0.5f);
        timeToAssemble.Add(IngredientType.Onion, 0.0f);
        timeToAssemble.Add(IngredientType.Meat, 0.5f);
        timeToAssemble.Add(IngredientType.Fajita, 1.0f);
        timeToAssemble.Add(IngredientType.LasagnaNoodles, 1.0f);
        timeToAssemble.Add(IngredientType.Seaweed, 1.0f);

        return timeToAssemble[ingredient];
    }

}
