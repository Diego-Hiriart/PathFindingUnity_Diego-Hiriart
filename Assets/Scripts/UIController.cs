using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject cameras;
    [SerializeField] GameObject menu;

    void Start() 
    {
        ToggleMenu(false);
    }

    public void ToggleMenu(bool turnOn)
    {
        menu.SetActive(turnOn);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
