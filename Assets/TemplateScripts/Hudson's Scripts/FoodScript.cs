using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    public FoodPointController FPC;
    public bool doFoodDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doFoodDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gainFP()
    {
        
           FPC.foodPoints += 1;
           Debug.Log("Gained 1 point");
           Destroy(this.transform.gameObject);
        
    }
}
