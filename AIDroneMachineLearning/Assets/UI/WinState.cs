
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinState: MonoBehaviour
{
    public VisualElement ui;

    public Button restartButton;
    public Button quitButton;

    public void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        gameObject.SetActive(false);
        restartButton = ui.Q<Button>("RestartButton");
        restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);
    }

    public void Update()
    {
        
    }

    public void OnRestartButtonClicked(ClickEvent evt)
    {
        string SceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneName);
        gameObject.SetActive(false);  
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

}
