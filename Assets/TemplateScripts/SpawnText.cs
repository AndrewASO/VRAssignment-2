using UnityEngine;

public class SpawnText : MonoBehaviour {

    public GameObject testText;
    //public TextMeshProUGUI testText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        testText.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        //Api.UpdateScreenParams();

        if (XRInputManager.IsButtonDown() ) {
            testText.SetActive(true);
        }
    }
}
