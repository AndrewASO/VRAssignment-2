using UnityEngine;

public class GameQuit2 : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

        // Unity Editor Test
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
