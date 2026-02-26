using UnityEngine;

public class HeldObjectManager : MonoBehaviour {

    public static HeldObjectManager Instance; //singleton for easy access

    public Transform holdPosition; //Empty child of camera where held object goes
    public float holdDistance = 1.5f;
    public float smoothSpeed = 10f;

    private GameObject heldObject;
    private Rigidbody heldRigidbody;

    void Awake() {
        if (Instance != null && Instance != this) {
        Debug.LogError("Multiple HeldObjectManager instances detected! Destroying the new one.");
        Destroy(gameObject);
        return;
        }
        Instance = this;
        Debug.Log("HeldObjectManager initialized: " + gameObject.name);
    }

    // Update is called once per frame
    void Update() {
        if( heldObject != null) {
            //If not using holdPosition, calculate spot in front of camera
            Vector3 targetPos;
            Quaternion targetRot;

            if(holdPosition != null) {
                //Use assigned transform
                targetPos = holdPosition.position;
                targetRot = holdPosition.rotation;
            }
            else {
                targetPos = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
                targetRot = Quaternion.LookRotation(Camera.main.transform.forward);
            }

            //Smoothly move held object
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPos, Time.deltaTime * smoothSpeed);
            heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
        }
        //Debug.Log($"Current held obj: {heldObject} ");
    }

    public void PickUpObject(GameObject obj) {
        if( heldObject != null ) {
            DropObject();   //Drops any held objects if there's currently any
        } 

        Debug.Log($"PickUpObject called on instance {GetInstanceID()}");

        heldObject = obj;
        heldRigidbody = obj.GetComponent<Rigidbody>();

        if (heldRigidbody != null){
            heldRigidbody.isKinematic = true;   //Disable physics while held
        }

        //Disable collider so it doens't block raycasts
        Collider col = obj.GetComponent<Collider>();
        if( col != null) {
            col.enabled = false;
        }

        //Optional disabling gaze interaction while held
        GazeOverEvent gaze = obj.GetComponent<GazeOverEvent>();
        if (gaze != null) {
            gaze.enabled = false;
        }
        
        Debug.Log($"Current Object being held: {heldObject} ");
    }

    public void DropObject() {
        if ( heldObject != null ) {
            
            //heldRigidbody?.isKinematic = false;
            if (heldRigidbody != null) {
                heldRigidbody.isKinematic = false;
            }

            //Re-enable collisions
            Collider col = heldObject.GetComponent<Collider>();
            if (col != null) {
                col.enabled = true;
            }

            GazeOverEvent gaze = heldObject.GetComponent<GazeOverEvent>();

            if ( gaze != null) {
                gaze.enabled = true;
            }

            heldObject = null;
            heldRigidbody = null;
        } 
    }

    public void ClearHeldObject() {
        heldObject = null;
        heldRigidbody = null;
    }

    public GameObject GetHeldObject() {
        Debug.Log($"This is the current held object value being returned: {heldObject}");
        return heldObject;
    }
}
