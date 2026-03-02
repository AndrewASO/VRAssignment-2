using UnityEngine;

public class ExitObjectSpawner2 : MonoBehaviour
{
    public GameObject exitObjectPrefab;
    public Transform spawnLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalTimer2.Instance != null)
        {
            GlobalTimer2.Instance.OnTimeReached += SpawnExitObject;
        }
    }

    private void OnDisable()
    {
        if (GlobalTimer2.Instance != null)
        {
            GlobalTimer2.Instance.OnTimeReached -= SpawnExitObject;
        }
    }

    void SpawnExitObject()
    {
        GameObject obj = Instantiate(exitObjectPrefab, spawnLocation.position, spawnLocation.rotation);
    }
}
