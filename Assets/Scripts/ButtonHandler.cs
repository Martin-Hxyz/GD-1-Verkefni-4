using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void ExitButtonPressed()
    {
        // slokkva a leik
        Application.Quit();
    }

    public void ReplayButtonPressed()
    {
        // loada MainScene senunni
        SceneManager.LoadScene(0);
    }
}
