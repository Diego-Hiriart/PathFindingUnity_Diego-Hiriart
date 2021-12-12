using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("START GAME");
        SceneManager.LoadScene(1); // Load the game scene
    }

    public void OpenRepository()
    {
        Application.OpenURL("https://github.com/LuisCorales/snake3d-unity");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
