using UnityEngine;

public class Pickable : MonoBehaviour {

    public PlacementZone currentZone;

    public void Pickup() {

        //If this object is in a zone, then its updating the zone that there's no longer any obj's in it
        //currentZone?.ClearPlacedObject();
        //currentZone = null;
        if(currentZone != null) {
            currentZone.ClearPlacedObject();
            currentZone = null;
        }

        if(HeldObjectManager.Instance != null) {
            HeldObjectManager.Instance.PickUpObject(gameObject);
        }
        //HeldObjectManager.Instance?.PickUpObject(gameObject);
    }

    public void OnDestroy() {
        //Whenever the pickable obj is destroyed, then it'll clear the zone references
        currentZone?.ClearPlacedObject(); 
    }
}
