using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class hm_SceneChange : MonoBehaviour
{
    public FoodPointController FPC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        if (!FPC.doorLock)
        {
            SceneManager.LoadScene("SushiScene");
            Debug.Log("Scene Changed");
        }
    }
}
