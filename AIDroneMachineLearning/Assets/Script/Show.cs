using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    private BoxCollider area;
    public  GameObject Player;
    private GameObject PlayerBody;

    private void Start()
    {
        area = GetComponent<BoxCollider>();
        PlayerBody = Player.transform.GetChild(0).GetChild(0).gameObject;
    }

    void Update()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        
        if (area.bounds.Contains(PlayerBody.transform.position))
        {
            Debug.Log(gameObject.transform.GetChild(0).gameObject.name);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
