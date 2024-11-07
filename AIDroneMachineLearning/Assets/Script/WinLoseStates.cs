using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinLoseStates : MonoBehaviour
{
    [Header("Drone components")]
    public DroneCollissionDetection respawn;

    [Header("Win/Lose States")]
    public WinState winState;
    public LoseState loseState;

    // Start is called before the first frame update
    void Start()
    {
        // Commented out for future coding
        //thisTransform = transform;
        //this.transform.position = originalPosition;
    }

    // Commented out for future coding
    void Update()
    {
       if (winState.gameObject.activeSelf == true)
        {
            //respawn.Invoke("Respawn", 5);
            Invoke("ReloadScene", 3);
            
        }

        
        if (Input.anyKey && loseState.gameObject.activeSelf == true)
        {
            
            loseState.gameObject.SetActive(false);
            respawn.Invoke("Respawn", 0);
        }
    }

    // Commented out for future coding
    //public void Restart()
    //{
        //if (Input.GetButtonUp("Fire1"))
        //{
            //winState.restartButton.clicked += winState.OnRestartButtonClicked;
        //}
    //}

    // Commented out for future coding
    //public void QuitGame()
    //{
        //if (Input.GetButtonUp("Fire1"))
        //{
            //winState.quitButton.clicked += winState.OnQuitButtonClicked;
        //}
    //}

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Win()
    {
        winState.gameObject.SetActive(true);

        winState.restartButton = winState.ui.Q<Button>("RestartButton");
        //Restart();         

        winState.quitButton = winState.ui.Q<Button>("QuitButton");
        //QuitGame();
    }
    public void Lose()
    {
        
        loseState.gameObject.SetActive(true);
    }
}
