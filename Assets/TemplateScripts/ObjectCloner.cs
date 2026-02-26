using UnityEngine;

public class ObjectCloner : MonoBehaviour {
    public GameObject prefabToSpawn;
    public float spawnOffset = 1.0f;

    public bool isSource = false;

    private bool CanSpawn() {
        if( !isSource ){
            Debug.Log("This object is not a source - cannot spawn.");
            return false;
        }
        return true;
    }

    public void SpawnAtObjectPosition() {
        if( !CanSpawn() ) {
            return;
        }
        //Spawn at the object's position (Where the player is looking)
        Debug.Log("test Worked");
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }

    public void SpawnInFrontOfCamera() {
        if( !CanSpawn() ){
            return;
        }
        //Spawn 1 meter in front of the camera
        Debug.Log("Test 2 worked");
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnOffset;
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    /**
    public void SpawnAndHold() {
        //Calculate spawn position in front of camera
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnOffset;

        //Create new object
        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        //Telling the held object manager to hold the newly created game obj
        if (HeldObjectManager.Instance != null) {
            HeldObjectManager.Instance.PickUpObject(newObject);
        }
        else {
            Debug.LogError("HeldObjectManager isn't found in scene");
        }
    }
    */

    public void SpawnAndHold() {
        if (!CanSpawn() ) {
            return;
        }
        // Only spawn if no object is held
        if (HeldObjectManager.Instance.GetHeldObject() != null)
        {
            Debug.Log("Already holding something – cannot spawn a new object.");
            return;
        }

        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnOffset;
        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        ObjectCloner cloneCloner = newObject.GetComponent<ObjectCloner>();
        if(cloneCloner != null) {
            cloneCloner.isSource = false; //Clone is not the source
        }
        HeldObjectManager.Instance.PickUpObject(newObject);
    }
}
