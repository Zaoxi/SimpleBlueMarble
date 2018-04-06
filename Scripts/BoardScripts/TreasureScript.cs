using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour {

    private BoardManager boardManager;
    private PlayerManager playerManager;
    private const int GAIN = 10;

    void Start()
    {
        boardManager = BoardManager.GetInstance();
        playerManager = PlayerManager.GetInstance();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject[] players;
        int i;
        if (playerManager.IsStop() && other.CompareTag("Player"))
        {
            players = playerManager.GetPlayers();

            for (i = 0; i < 4; i++)
            {
                if (GameObject.ReferenceEquals(other.gameObject, players[i]))
                {
                    break;
                }
            }

            switch (i)
            {
                case 0:
                    playerManager.UpdatePlayer1Score(GAIN);
                    break;
                case 1:
                    playerManager.UpdatePlayer2Score(GAIN);
                    break;
                case 2:
                    playerManager.UpdatePlayer3Score(GAIN);
                    break;
                case 3:
                    playerManager.UpdatePlayer4Score(GAIN);
                    break;
            }
        }
    }
}
