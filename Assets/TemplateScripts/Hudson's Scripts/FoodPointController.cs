using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodPointController : MonoBehaviour
{
    public int foodPoints;
    public bool doorLock;
    public TMP_Text PointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foodPoints = 0;
        doorLock = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (foodPoints >= 5)
        {
            doorLock = false;
            PointText.text = "Door Unlocked";
        }
        else
        {
            doorLock = true;
            PointText.text = $"Foods Found: {foodPoints}/5";
        }
    }
}
