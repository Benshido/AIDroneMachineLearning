using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoseState : MonoBehaviour
{
    public VisualElement ui1;


    public void Start()
    {
        ui1 = GetComponent<UIDocument>().rootVisualElement;
        gameObject.SetActive(false);
    }
}
