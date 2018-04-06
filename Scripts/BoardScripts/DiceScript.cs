using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    const float DICE_SPEED = 0.2f;
    const float WAIT_ROLL = 0.1f;
    const float WAIT_SHOW = 3f;

    public Sprite[] dice;

    private BoardManager boardManager;

    private SpriteRenderer render;

    private bool rolling = false;

    private int diceNumber = -1;

    public bool Rolling
    {
        get { return rolling; }
    }

    public int Number
    {
        get { return diceNumber; }
    }

    // Use this for initialization
    void Start()
    {
        boardManager = BoardManager.GetInstance();
        render = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollDice()
    {
        diceNumber = -1;

        gameObject.SetActive(true);

        StartCoroutine(RollingDice());
    }

    private IEnumerator RollingDice()
    {
        rolling = true;

        int number = 0;

        for (int i = 0; i < 20; i++)
        {
            number = (int)Random.Range(0.0f, 6.0f);

            render.sprite = dice[number];

            int direction = (int)Random.Range(0.0f, 4.0f);
            switch (direction)
            {
                case 0:
                    transform.Translate(Vector3.right * DICE_SPEED);
                    break;
                case 1:
                    transform.Translate(Vector3.left * DICE_SPEED);
                    break;
                case 2:
                    transform.Translate(Vector3.up * DICE_SPEED);
                    break;
                case 3:
                    transform.Translate(Vector3.down * DICE_SPEED);
                    break;
            }

            yield return new WaitForSeconds(WAIT_ROLL);
        }

        diceNumber = number;

        StartCoroutine(ResetDice());

        rolling = false;
    }

    private IEnumerator ResetDice()
    {
        yield return new WaitForSeconds(WAIT_SHOW);

        transform.position = new Vector3(4.5f, 4.5f, -2f);

        boardManager.GetPlayerManager().ChangeTurn();

        gameObject.SetActive(false);
    }
}
