using UnityEngine;

public class GameQuit : MonoBehaviour {
    public void QuitGame() {
        Application.Quit();

        //Unity Editor Test
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
