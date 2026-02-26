using Google.XR.Cardboard;
using UnityEngine;

/// <summary>
/// A simple class that spawns a donut or donuts where the player is looking when the trigger button is pressed.
/// </summary>
public class TriggerDonutSpawn : MonoBehaviour
{
    public GameObject donutPrefab; // a reference to the donut prefab object that should be spawned
    public bool spawnOnlyOneDonut; // bool to determine if one donut should be spawned or unlimited donuts

    private bool donutSpawned; // for keeping track of if a donut has spawned if only one donut should be spawned
    private Camera cam; // the player's camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main; // set the camera to be the player's camera
        donutSpawned = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Api.UpdateScreenParams();

        // if the trigger button is pressed, spawn a donut
        if (XRInputManager.IsButtonDown())
        {
            // if spawnOnlyOneDonut is true, update donutSpawned to be true after the first donut is spawned so no other donuts spawn
            if (!donutSpawned)
            {
                SpawnDonut(cam.transform.forward, donutPrefab.transform.rotation); // spawns the donut where the player is looking, with the donuts standard rotation
                if (spawnOnlyOneDonut) donutSpawned = true;
            }
        }
    }

    /// <summary>
    /// Instantiates a donut prefab object into the scene.
    /// </summary>
    /// <param name="pos">The position the donut should take when spawned</param>
    /// <param name="rot">The rotation the donut should have when spawned</param>
    public void SpawnDonut(Vector3 pos, Quaternion rot)
    {
        //Debug.Log("Spawning donut");
        Instantiate(donutPrefab, pos, rot);
    }
}
