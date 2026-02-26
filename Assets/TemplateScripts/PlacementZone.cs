/**
Documentation:
Coroutine: https://docs.unity3d.com/Manual/Coroutines.html
Countdown Timer: https://subscription.packtpub.com/book/game-development/9781784391362/1/ch01lvl1sec12/displaying-a-countdown-timer-graphically-with-a-ui-slider
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlacementZone : MonoBehaviour {

    public Transform snapPoint;             //The snap point to where objects will go when placed 
    public bool makeKinematic = true;       //Placed objects won't move anymore
    public RecipeDatabase recipeDatabase;   //Assigns the database containing all of the recipes
    private GameObject placedObject;        //Tracks whats placed here

    public GameObject cookingUIPrefab;     //UI prefab for cooking timer
    private Coroutine cookingCoroutine;     //Coroutine was used as it wouldn't interrupt the rest of the game and wouldn't need to be tracked with updates()
    private GameObject activeCookingUI;


    public void TryPlaceHeldObject() {
        HeldObjectManager manager = HeldObjectManager.Instance;
        if( manager == null) return;
        //Debug.Log($"Current thing held in HeldObjectManager: {manager.GetHeldObject() }");
        Debug.Log($"Manager instance ID: {manager.GetInstanceID()}, heldObject = {manager.GetHeldObject()}");
        
        GameObject heldObj = manager.GetHeldObject();
        if (heldObj == null) {
            Debug.Log("Placement Zone: Nothing is held in players hand");
            return;
        }

        //If there's an obj in the zone, then attempt combination and if it isn't possible, then place held obj
        if (placedObject != null) {
            TryCombine(heldObj, placedObject);
            return;
        }

        //Check if the single object can be cooked
        RecipeDatabase.Recipe? cookingRecipe = FindCookingRecipe(heldObj);
        //Only cooked ingredients will have a cookTime greater than 0, as 0 will be instant combinations
        //This is a bad implementation and will fail if combinations are ever decided to be given a timer 
        //or cook/combination time greater than 0
        if (cookingRecipe != null && cookingRecipe.Value.cookTime > 0) {
            StartCooking(heldObj, cookingRecipe.Value);
            return;
        }

        // Place held obj
        PlaceObject(heldObj);

        //Maybe create a Debug.Log here later saying whatever heldObj.name was palced on gameObj.name
    }

    private void TryCombine(GameObject objA, GameObject objB) {
        if( recipeDatabase == null) {
            Debug.LogWarning("No recipe database has been assigned to this zone");
            return;
        }

        RecipeDatabase.Recipe? matchingRecipe = FindRecipe(objA, objB);

        if(matchingRecipe == null) {
            Debug.Log("These objects cannot be combined");
            return;
        }

        //Clear zone references from both obj's along with destroying
        Pickable pickableA = objA.GetComponent<Pickable>();
        if (pickableA != null) pickableA.currentZone = null;

        Pickable pickableB = objB.GetComponent<Pickable>();
        if (pickableB != null) pickableB.currentZone = null;

        //Destroy both ingredients 
        Destroy(objA);
        Destroy(objB);

        //Spawn the result at the designated sanp point
        GameObject result = Instantiate(matchingRecipe.Value.resultPrefab, snapPoint.position, snapPoint.rotation );

        //Configure the result
        ConfigurePlacedObject(result);

        //Designate this as new placed obj
        placedObject = result;

        //Clear held obj from manager since it was destroyed
        HeldObjectManager.Instance.ClearHeldObject();
        
        Debug.Log($"Combined into {result.name} " );
    }

    private void StartCooking(GameObject rawObj, RecipeDatabase.Recipe recipe) {
        //Snap the object into place
        rawObj.transform.position = snapPoint.position;
        rawObj.transform.rotation = snapPoint.rotation;

        //Disable its interactions
        DisableInteractions(rawObj);

        placedObject = rawObj;

        //Clear held obj from manager
        HeldObjectManager.Instance.ClearHeldObject();

        //Start cooking coroutine
        if(cookingCoroutine != null) StopCoroutine(cookingCoroutine);
        cookingCoroutine = StartCoroutine(CookingProcess(rawObj, recipe) );
    }

    private IEnumerator CookingProcess(GameObject rawObj, RecipeDatabase.Recipe recipe) {
        float timer = recipe.cookTime;
        
        //Creating and positioning the cooking UI prefab
        if(cookingUIPrefab != null) {
            activeCookingUI = Instantiate(cookingUIPrefab, snapPoint.position + Vector3.up * 0.3f, Quaternion.identity, transform);
        }

        //Getting the UI Elements
        Text countdownText = null;
        if( activeCookingUI != null) {
            countdownText = activeCookingUI.GetComponentInChildren<Text>();
        }

        //Cooking loop
        while(timer > 0) {
            timer -= Time.deltaTime;
            if(countdownText != null) {
                countdownText.text = Mathf.Ceil(timer).ToString();
            }

            yield return null;  //Wait for 1 frame before proceeding (Waiting for next frame)
        }

        //Cooking finished so destroy cooking UI & raw obj
        Destroy(activeCookingUI);
        Destroy(rawObj);

        //Spawning cooked result
        GameObject cooked = Instantiate(recipe.resultPrefab, snapPoint.position, snapPoint.rotation);
        ConfigurePlacedObject(cooked);
        placedObject = cooked;

        cookingCoroutine = null;
    }

    private RecipeDatabase.Recipe? FindRecipe(GameObject obj1, GameObject obj2) {

        //Checks both possible combinations for recipes
        foreach( var recipe in recipeDatabase.recipes) {
            bool match = (obj1.CompareTag(recipe.ingredient1Tag) && obj2.CompareTag(recipe.ingredient2Tag) ) 
            || (obj1.CompareTag(recipe.ingredient2Tag) && obj2.CompareTag(recipe.ingredient1Tag) );

            if (match) return recipe;
        }
        return null;
    }

    //What have I done to myself, can combine these two into 1 possibly later ? No maybe not ? Eh, I'll have to 
    //figure out a way to see if I could substitute in a dummy value for obj2 possibly and if its null then I'd go
    // to a cooked recipe
    private RecipeDatabase.Recipe? FindCookingRecipe(GameObject obj) {
        if(recipeDatabase == null) return null;
        foreach(var recipe in recipeDatabase.recipes) {
            //Cooking recipe: ingredientTag2 will be empty as its only 1 obj being cooked
            if(string.IsNullOrEmpty(recipe.ingredient2Tag) && obj.CompareTag(recipe.ingredient1Tag) ) return recipe;
        }
        return null;
    }

    private void PlaceObject(GameObject heldObj) {
        
        //Snapping the held object onto the designated snap point
        heldObj.transform.position = snapPoint.position;
        heldObj.transform.rotation = snapPoint.rotation;

        //Apply placement configs
        ConfigurePlacedObject(heldObj);

        //Remember obj is now held here
        placedObject = heldObj;

        //Tell the held manager that the obj is no longer being held
        HeldObjectManager.Instance.ClearHeldObject();
    }

    //Helper method to setup an object that's sitting inside the zone 
    public void ConfigurePlacedObject(GameObject obj) {

        //Making it kinematic so it isn't affected anymore 
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if ( rb != null && makeKinematic ) {
            rb.isKinematic = true;
        }

        //Re-enabling collision
        Collider col = obj.GetComponent<Collider>();
        if (col != null) {
            col.enabled = true;
        }

        //Re-enabling gaze so it can be picked up later
        GazeOverEvent gaze = obj.GetComponent<GazeOverEvent>();
        if( gaze != null ) {
            gaze.enabled = true;
        }

        //Telling the heldObj which zone it belongs to
        Pickable pickable = obj.GetComponent<Pickable>();
        if (pickable != null) {
            pickable.currentZone = this;
        }
    }

    //Disable objects so they can't be picked up during cooking process
    private void DisableInteractions(GameObject obj) {
        GazeOverEvent gaze = obj.GetComponent<GazeOverEvent>();
        if(gaze != null) gaze.enabled = false;
        Collider col = obj.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    //Method to clear place object later on if the incorrect ones are placed if trash can isn't implemented
    public void ClearPlacedObject() {
        placedObject = null;
    }
}
