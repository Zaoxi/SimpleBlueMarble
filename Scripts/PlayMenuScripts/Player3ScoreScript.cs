using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3ScoreScript : MonoBehaviour
{
    private UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.GetInstance();
        uiManager.GetPlayerScore()[2] = GetComponent<Text>();
    }
}
