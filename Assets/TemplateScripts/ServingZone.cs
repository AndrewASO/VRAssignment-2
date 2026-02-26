using UnityEngine;

public class ServingZone : MonoBehaviour {

    public Transform snapPoint;

    private OrderManager orderManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        orderManager = FindFirstObjectByType<OrderManager>();
        if(orderManager == null) {
            Debug.LogError("No OrderManager foudn in scene");
        }
    }

    public void TryServeHeldObject() {
        HeldObjectManager manager = HeldObjectManager.Instance;
        if(manager == null) return;

        GameObject heldObj = manager.GetHeldObject();
        if(heldObj == null) {
            Debug.Log("Nothing held");
            return;
        }

        //Makes sure orderManager isn't null and if the dish is correct with the right tag
        if(orderManager != null && orderManager.IsCorrectDish(heldObj.tag)) {
            if(snapPoint != null) {
                heldObj.transform.position = snapPoint.position;
                heldObj.transform.rotation = snapPoint.rotation;
            }
            
            //Destroys dish
            Destroy(heldObj);
            manager.ClearHeldObject();

            //Updating order & score
            orderManager.DishServed();

            Debug.Log("Correct Dish, +1 score");
        }
        else {
            Debug.Log("Wrong dish served");
        }
    }
}
