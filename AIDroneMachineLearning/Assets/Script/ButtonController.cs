using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private string changeGameScene = "DroneControlTestLevel";

    public void GameButton()
    {
        SceneManager.LoadScene(changeGameScene);
    }
}
