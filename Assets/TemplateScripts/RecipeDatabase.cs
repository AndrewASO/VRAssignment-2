using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Crafting/RecipeDatabase") ]
public class RecipeDatabase : ScriptableObject {
    
    [System.Serializable]
    public struct Recipe {
        public string ingredient1Tag;   //e.g., shrimp
        public string ingredient2Tag;   //e.g., rice, can also be empty to simply account for cooking things like shrimp into tempura where no 2nd ingredient is needed
        public float cookTime;  //How long it takes to cook an ingredient (0 would be for an instant combination)
        public GameObject resultPrefab; //e.g., Sushi
    }

    public List<Recipe> recipes = new List<Recipe>();
}
