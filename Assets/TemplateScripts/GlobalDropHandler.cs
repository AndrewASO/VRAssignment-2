using UnityEngine;

public class GlobalDropHandler : MonoBehaviour {

    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //Check for trigger pressed
        if (XRInputManager.IsButtonDown()) {
            DropObject();
        }
    }

    public void DropObject() {
        //Ray cast from the center of camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        //The raycast hit an object
        if (Physics.Raycast(ray, out hit)) {

            Debug.Log($"GlobalDropHandler hit: {hit.collider.name}");
            //Check if the hit object has GazeOverEvent
            GazeOverEvent gaze = hit.collider.GetComponent<GazeOverEvent>();
            if (gaze != null && gaze.enabled) {
                
                //Verify that object is in hover angle
                Vector3 toHitPoint = hit.point - cam.transform.position;
                float angle = Vector3.Angle(cam.transform.forward, toHitPoint);
                Debug.Log($"Angle; {angle}, max allowed: {gaze.maximumAngleForEvent}");
                
                if(angle <= gaze.maximumAngleForEvent) {
                    //Its a valid interactive object, so let it run its own functions
                    Debug.Log("Valid hover - letting object handle trigger");
                    return;
                }
                else {
                    Debug.Log("Angle too large - treating as drop");
                }
            }
        }

        //The raycast didn't hit an object
        if( HeldObjectManager.Instance != null && HeldObjectManager.Instance.GetHeldObject() != null ) {
            //HeldObjectManager.Instance.DropObject();
        }
    }
}
