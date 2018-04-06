using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    private PlayerManager playerManager;
    private BoardManager boardManager;

    private static UIManager instance = null;

    private Text[] playerScore;

	public static UIManager GetInstance()
    {
        if (instance == null) instance = FindObjectOfType<UIManager>();
        return instance;
    }

    void Awake()
    {
        playerManager = PlayerManager.GetInstance();
        boardManager = BoardManager.GetInstance();
        playerScore = new Text[4];
    }

    public Text[] GetPlayerScore()
    {
        return playerScore;
    }

    public void ClickMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
