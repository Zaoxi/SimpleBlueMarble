using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4CastleScript : MonoBehaviour {
    private PlayerManager playerManager;
    private GameObject owner;
    private const int ATTACK = -50;

    void Start()
    {
        playerManager = PlayerManager.GetInstance();
        owner = playerManager.GetPlayers()[3];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject[] players;
        int i;
        if (playerManager.IsStop() && !GameObject.ReferenceEquals(owner, other.gameObject) && other.CompareTag("Player"))
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
                    playerManager.UpdatePlayer1Score(ATTACK);
                    break;
                case 1:
                    playerManager.UpdatePlayer2Score(ATTACK);
                    break;
                case 2:
                    playerManager.UpdatePlayer3Score(ATTACK);
                    break;
            }
        }
    }
}
