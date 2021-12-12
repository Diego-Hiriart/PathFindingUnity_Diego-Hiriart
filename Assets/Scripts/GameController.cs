using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private bool gameOver;
    [SerializeField] GameObject snake;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] GameObject fruitSpawner;

    [SerializeField] float mapWidth = 29;
    [SerializeField] float spawnLimit = 1f;//How far from the map end is a valid area for a pickup
    private int oldFruitCount = 0;
    private int currentFruitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;//Resume game
        this.oldFruitCount = this.currentFruitCount = snake.GetComponent<SnakeEater>().GetEatenFruits();
        RandomPickUps(true);
    }

    // Update is called once per frame
    void Update()
    {
        HandleScore();
        HandleGameOver();
        this.currentFruitCount = snake.GetComponent<SnakeEater>().GetEatenFruits();//Check fruit count to use it in RandonPickUps method
        RandomPickUps(false);
    }

    private void HandleScore()
    {
        points.text = "Points: " + snake.GetComponent<SnakeEater>().GetEatenFruits();
    }

    private void HandleGameOver()
    {
        gameOver = snake.GetComponent<SnakeEater>().IsDead();
        if (gameOver)//End the game
        {
            Time.timeScale = 0;//Pause game
            UI.GetComponent<UIController>().ToggleMenu(true); //Show menu to quit or restart game
        }
    }

    private void RandomPickUps(bool first)
    {
        //Create a new fruit only if the snake has eaten the one that existed or if it is the first fruit of the game
        if (currentFruitCount > oldFruitCount || first)
        {
            float randX = Random.Range(0 + spawnLimit, mapWidth - spawnLimit);
            float randZ = Random.Range(0 + spawnLimit, mapWidth - spawnLimit);
            GameObject fruit = Instantiate(fruitPrefab, new Vector3(randX, 0.5f, randZ), 
                fruitPrefab.transform.rotation, fruitSpawner.transform);//Create a new fruit with default prefab rotation
            fruit.SetActive(true);//Activate the fruit (prefab isnt active)
            this.oldFruitCount = currentFruitCount;//Update old count
        }       
    }
}
