

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour {

    public static OrderManager Instance;

    public string[] dishTags;

    public TMP_Text orderText;
    public TMP_Text scoreText;

    private string currentOrderTag;
    private int score = 0;


    void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        NewOrder();
        UpdateScoreText();
    }

    public void NewOrder() {
        if(dishTags.Length == 0) {
            Debug.LogWarning("No dish tags assigned to OrderManager");
            return;
        }
        currentOrderTag = dishTags[Random.Range(0, dishTags.Length) ];
        UpdateOrderText();
    }

    public bool IsCorrectDish(string dishTag) {
        return dishTag == currentOrderTag;
    }

    public void DishServed() {
        score++;
        UpdateScoreText();

        //Quit Game
        if(score == 5) {
            orderText.text = "You've completed all the orders !";
            enabled = false;    //Stops running this script
            //Need to add something here for scene.load after x amount of time elapses ? 
        }
        else {
            NewOrder();
        }  
    }

    
    void UpdateOrderText() {
        if (orderText != null) {
            orderText.text = "Make: " + currentOrderTag;
        }
    }

    void UpdateScoreText() {
        if (scoreText != null) {
            scoreText.text = "Dishes completed: " + score;
        }
    }
}
