using UnityEngine;
using System;

// Event link: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/

public class GlobalTimer2 : MonoBehaviour
{
    public static GlobalTimer2 Instance { get; private set; }

    // Time in seconds after which the exit option becomes available
    public float timeThreshold = 180f; // 3 minutes

    public bool TimeReached { get; private set; } = false;

    // An event that other scripts can link to
    public event Action OnTimeReached;

    private float elapsedTime = 0f;
    private bool eventFired = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Allows for the gameObject to persist through scene changes
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeReached) return; // Stops counting once the time threshold has been reached

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= timeThreshold && !eventFired)
        {
            TimeReached = true;
            eventFired = true;

            // Notify all linked
            OnTimeReached?.Invoke();

            Debug.Log("Global timer threshold has been reached");
        }
    }

    public void ResetTime()
    {
        elapsedTime = 0f;
        TimeReached = false;
        eventFired = false;
    }
}
