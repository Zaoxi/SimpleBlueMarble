using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float MOVE_TIME = 0.01f;
    private const int BASIC_SCORE = 100;

    private static PlayerManager instance = null;

    private BoardManager boardManager;
    private UIManager uiManager;

    public GameObject[] playerPrefabs;

    private GameObject[] players;

    // 플레이어의 턴
    private int turn = 0;
    public int Turn
    {
        get { return turn; }
    }
    // 플레이어가 움직이고 있는 중인지 나타내는 상태
    private bool moving = false;
    // 주사위가 굴려진 후 플레이어가 전진한 횟수
    private int count = 0;
    // 주사위의 눈금의 합
    private int diceNumber = -1;
    // 주사위를 굴릴 수 있는가?
    private bool rollable = true;
    // DiceScript에서 ChangeTurn을 몇번호출했는지
    private int diceCount = 0;
    private int[] playerScore;


    public static PlayerManager GetInstance()
    {
        if (instance == null) instance = FindObjectOfType<PlayerManager>();
        return instance;
    }

    // Use this for initialization
    void Awake()
    {
        int n = 4;
        boardManager = BoardManager.GetInstance();
        uiManager = UIManager.GetInstance();
        IntantiatePlayer(n);
        diceNumber = 3;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDiceRoll();
    }

    // 점수 UI를 업데이트하는 메소드
    public void UpdateScoreText()
    {
        uiManager.GetPlayerScore()[0].text = playerScore[0].ToString();
        uiManager.GetPlayerScore()[1].text = playerScore[1].ToString();
        uiManager.GetPlayerScore()[2].text = playerScore[2].ToString();
        uiManager.GetPlayerScore()[3].text = playerScore[3].ToString();
    }

    // R버튼을 눌렀을때, 주사위를 던질 수 있는 상황이면 던진다.
    private void CheckDiceRoll()
    {
        if(rollable && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("roll");
            rollable = false;
            boardManager.RollDice();
        }
        else if(!rollable && boardManager.CheckDice() != -1)
        {
            diceNumber = boardManager.CheckDice();
            PlayerMove();
        }
    }

    // n명 만큼 플레이어를 소환한다.
    private void IntantiatePlayer(int n)
    {
        players = new GameObject[n];
        playerScore = new int[n];

        players[0] = Instantiate(playerPrefabs[0], transform);
        players[0].transform.localPosition = new Vector3(0f, 0f, 0f);
        playerScore[0] = BASIC_SCORE;

        players[1] = Instantiate(playerPrefabs[1], transform);
        players[1].transform.localPosition = new Vector3(9f, 9f, 0f);
        playerScore[1] = BASIC_SCORE;

        players[2] = Instantiate(playerPrefabs[2]);
        players[2].transform.parent = transform;
        players[2].transform.localPosition = new Vector3(0f, 9f, 0f);
        playerScore[2] = BASIC_SCORE;

        players[3] = Instantiate(playerPrefabs[3]);
        players[3].transform.parent = transform;
        players[3].transform.localPosition = new Vector3(9f, 0f, 0f);
        playerScore[3] = BASIC_SCORE;

    }

    // 주사위 눈금 수 만큼 움직이는 메소드
    public void PlayerMove()
    {
        if (!moving && count < diceNumber)
        {
            // 오른쪽으로 진행해야 하는 경우
            if (players[turn].transform.position.x < 8f && players[turn].transform.position.y < 1f)
            {
                Debug.Log(players[turn].transform.position);
                StartCoroutine(Move(new Vector3(players[turn].transform.position.x + 1,
                    players[turn].transform.position.y, 0f)));
            } // 위로 진행해야 하는 경우
            else if (players[turn].transform.position.y < 8f && players[turn].transform.position.x > 8f)
            {
                StartCoroutine(Move(new Vector3(players[turn].transform.position.x,
                    players[turn].transform.position.y + 1, 0f)));
            } // 왼쪽으로 진행해야 하는 경우
            else if (players[turn].transform.position.x > 1f && players[turn].transform.position.y > 8f)
            {
                StartCoroutine(Move(new Vector3(players[turn].transform.position.x - 1,
                    players[turn].transform.position.y, 0f)));
            } // 아래로 진행해야 하는 경우
            else if (players[turn].transform.position.y > 1f && players[turn].transform.position.x < 1f)
            {
                StartCoroutine(Move(new Vector3(players[turn].transform.position.x,
                    players[turn].transform.position.y - 1, 0f)));
            }
        }
    }
    private IEnumerator Move(Vector3 destination)
    {
        moving = true;
        if (destination.x != players[turn].transform.localPosition.x)
        {
            float x = destination.x - players[turn].transform.localPosition.x;
            x /= Mathf.Abs(x);
            for (int i = 0; i < 10; i++)
            {
                if (GameObject.ReferenceEquals(players[turn], null))
                    break;
                players[turn].transform.Translate(x * 0.1f, 0f, 0f);
                yield return new WaitForSeconds(MOVE_TIME);
            }
        }
        else if (destination.y != players[turn].transform.localPosition.y)
        {
            float y = destination.y - players[turn].transform.localPosition.y;
            y /= Mathf.Abs(y);
            for (int i = 0; i < 10; i++)
            {
                if (GameObject.ReferenceEquals(players[turn], null))
                    break;
                players[turn].transform.Translate(0f, y * 0.1f, 0f);
                yield return new WaitForSeconds(MOVE_TIME);
            }
        }
        count++;
        moving = false;
    }

    // 턴을 바꾸는 메소드, DiceScript에서 사용한다.
    public void ChangeTurn()
    {
        diceCount++;
        if (diceCount == 2)
        {
            diceCount = 0;
            count = 0;
            diceNumber = -1;
            rollable = true;
            turn = (turn + 1) % players.Length;
            // 플레이어 사망시 작동
            while(GameObject.ReferenceEquals(players[turn], null))
            {
                turn = (turn + 1) % players.Length;
            }
        }
    }
    // 플레이어 1의 점수를 업데이트하는 메소드
    public void UpdatePlayer1Score(int score)
    {
        playerScore[0] += score;
        if(playerScore[0] <= 0)
        {
            // 플레이어가 죽는 이펙트

            GameObject.Destroy(players[0]);
            players[0] = null;
            playerScore[0] = 0;
        }

        UpdateScoreText();
    }
    // 플레이어 2의 점수를 업데이트하는 메소드
    public void UpdatePlayer2Score(int score)
    {
        playerScore[1] += score;

        if (playerScore[1] <= 0)
        {
            // 플레이어가 죽는 이펙트

            GameObject.Destroy(players[1]);
            players[1] = null;
            playerScore[1] = 0;
        }

        UpdateScoreText();
    }
    // 플레이어 3의 점수를 업데이트하는 메소드
    public void UpdatePlayer3Score(int score)
    {
        playerScore[2] += score;

        if (playerScore[2] <= 0)
        {
            // 플레이어가 죽는 이펙트

            GameObject.Destroy(players[2]);
            players[2] = null;
            playerScore[2] = 0;
        }

        UpdateScoreText();
    }
    // 플레이어 4의 점수를 업데이트하는 메소드
    public void UpdatePlayer4Score(int score)
    {
        playerScore[3] += score;
        if (playerScore[3] <= 0)
        {
            // 플레이어가 죽는 이펙트

            GameObject.Destroy(players[3]);
            players[3] = null;
            playerScore[3] = 0;
        }

        UpdateScoreText();
    }
    // 플레이어 오브젝트들을 반환하는 메소드
    public GameObject[] GetPlayers()
    {
        return players;
    }
    // 플레이어 오브젝트들이 다음칸에서 멈추는가를 반환하는 메소드
    public bool IsStop()
    {
        if (count == diceNumber - 1)
            return true;
        return false;
    }
}
