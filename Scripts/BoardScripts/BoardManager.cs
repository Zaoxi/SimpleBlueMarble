using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static BoardManager instance = null;

    private PlayerManager playerManager = null;

    // 프리팹
    public GameObject thief;
    public GameObject treasure;
    public GameObject player1Area;
    public GameObject player2Area;
    public GameObject player3Area;
    public GameObject player4Area;
    public GameObject dicePrefab;

    // 생성된 오브젝트
    private GameObject[] dices;
    private DiceScript[] diceScripts;
    private GameObject[] thieves;
    private GameObject[] treasures;

    private const int numberX = 9;
    private const int numberY = 9;

    public static BoardManager GetInstance()
    {
        if (instance == null) instance = FindObjectOfType<BoardManager>();
        return instance;
    }

    void Awake()
    {
        playerManager = PlayerManager.GetInstance();
        MakeBoardObjects();

    }

    private void InitializeDice()
    {
        dices = new GameObject[2];
        diceScripts = new DiceScript[2];

        dices[0] = Instantiate(dicePrefab, transform);
        dices[1] = Instantiate(dicePrefab, transform);
        diceScripts[0] = dices[0].GetComponent<DiceScript>();
        diceScripts[1] = dices[1].GetComponent<DiceScript>();
    }

    public void RollDice()
    {
        diceScripts[0].RollDice();
        diceScripts[1].RollDice();
    }

    public int CheckDice()
    {
        if (diceScripts[0].Number != -1 && diceScripts[1].Number != -1)
        {
            return diceScripts[0].Number + diceScripts[1].Number + 2;
        }

        return -1;
    }

    public PlayerManager GetPlayerManager()
    {
        return playerManager;
    }

    private void MakeBoardObjects()
    {

        InitializeThieves();
        InitializeTreasures();
        InitializeDice();
    }

    private void InitializeTreasures()
    {
        int treasureX, treasureY;

        treasures = new GameObject[8];

        for (int i = 0; i < 3; i++)
        {
            treasureX = 0;
            treasureY = (int)Random.Range(1f, 9f);

            treasures[0] = Instantiate(treasure, transform);
            treasures[0].transform.position = new Vector3(treasureX, treasureY, -1f);
        }

        for (int i = 0; i < 3; i++)
        {
            treasureX = 9;
            treasureY = (int)Random.Range(1f, 9f);

            treasures[1] = Instantiate(treasure, transform);
            treasures[1].transform.position = new Vector3(treasureX, treasureY, -1f);
        }
        for (int i = 0; i < 3; i++)
        {
            treasureY = 0;
            treasureX = (int)Random.Range(1f, 9f);

            treasures[2] = Instantiate(treasure, transform);
            treasures[2].transform.position = new Vector3(treasureX, treasureY, -1f);
        }
        for (int i = 0; i < 3; i++)
        {
            treasureY = 9;
            treasureX = (int)Random.Range(1f, 9f);

            treasures[3] = Instantiate(treasure, transform);
            treasures[3].transform.position = new Vector3(treasureX, treasureY, -1f);
        }
    }

    private void InitializeThieves()
    {
        int thiefX, thiefY;

        thieves = new GameObject[4];

        thiefX = 0;
        for (int i = 0; i < 2; i++)
        {
            thiefY = (int)Random.Range(1f, 9f);

            thieves[0] = Instantiate(thief, transform);
            thieves[0].transform.position = new Vector3(thiefX, thiefY, -1.5f);
        }

        thiefX = 9;
        for (int i = 0; i < 2; i++)
        {
            thiefY = (int)Random.Range(1f, 9f);

            thieves[1] = Instantiate(thief, transform);
            thieves[1].transform.position = new Vector3(thiefX, thiefY, -1.5f);
        }

        thiefY = 0;
        for (int i = 0; i < 2; i++)
        {
            thiefX = (int)Random.Range(1f, 9f);

            thieves[2] = Instantiate(thief, transform);
            thieves[2].transform.position = new Vector3(thiefX, thiefY, -1.5f);
        }

        thiefY = 9;
        for (int i = 0; i < 2; i++)
        {
            thiefX = (int)Random.Range(1f, 9f);

            thieves[3] = Instantiate(thief, transform);
            thieves[3].transform.position = new Vector3(thiefX, thiefY, -1.5f);
        }
    }
}
