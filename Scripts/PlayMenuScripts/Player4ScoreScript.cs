using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player4ScoreScript : MonoBehaviour
{
    private UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.GetInstance();
        uiManager.GetPlayerScore()[3] = GetComponent<Text>();
    }
}
