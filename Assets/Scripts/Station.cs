using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    /// <summary>
    /// Station type
    /// </summary>
    public StationType type;

    /// <summary>
    /// Total slots
    /// </summary>
    public int totalSlots;

    /// <summary>
    /// Station position
    /// </summary>
    //public Transform transform;

    /// <summary>
    /// Number of slots available at the current moment
    /// </summary>
    private int freSlots { get; set; }

    /// <summary>
    /// List of ingredients in the station
    /// </summary>
    public List<Ingredient> ingredients;

    /// <summary>
    /// True if its reserved
    /// </summary>
    public bool reserved = false;

    // Start is called before the first frame update
    void Start()
    {
        this.freSlots = totalSlots;
        //this.transform = GetComponent<Transform>();
        this.ingredients = new List<Ingredient>();
    }

    public void AddIngredient(List<Ingredient> ing)
    {
        foreach(Ingredient i in ing)
        {
            ingredients.Add(i);
        }
    }

    public bool IsReserved()
    {
        return reserved;
    }

    public bool Reserve()
    {
        if (reserved)
            return false;
        reserved = true;
        return true;
    }

    public void StopReservation()
    {
        reserved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
