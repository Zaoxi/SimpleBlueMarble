using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Script : MonoBehaviour {
    private const int GAIN = 30;
    private PlayerManager playerManager;
    public Sprite[] player3;

    // 플레이어 스프라이트가 넘어가는 시간 간격
    const float TIME_CHANGE = 0.1f;

    // 스프라이트 렌더러
    private SpriteRenderer render;

    // Use this for initialization
    void Start()
    {
        playerManager = PlayerManager.GetInstance();
        render = GetComponent<SpriteRenderer>();
        StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어 애니메이션을 재생하는 코루틴 메서드
    public IEnumerator StartAnimation()
    {
        // 플레이어가 씬에 존재하는 한 계속해서 재생
        while (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < player3.Length; i++)
            {
                render.sprite = player3[i];
                yield return new WaitForSeconds(TIME_CHANGE);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject[] players;
        int i;
        if (playerManager.IsStop() && playerManager.Turn != 2 && other.CompareTag("Player"))
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
                    playerManager.UpdatePlayer3Score(GAIN);
                    playerManager.UpdatePlayer1Score(-GAIN);
                    break;
                case 1:
                    playerManager.UpdatePlayer3Score(GAIN);
                    playerManager.UpdatePlayer2Score(-GAIN);
                    break;
                case 3:
                    playerManager.UpdatePlayer3Score(GAIN);
                    playerManager.UpdatePlayer4Score(-GAIN);
                    break;
            }
        }
    }
}
