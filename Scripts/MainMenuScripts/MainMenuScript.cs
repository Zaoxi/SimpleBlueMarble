using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    // 몇인용으로 플레이 할 것인지 로드하는 메소드
    public void LoadSelectPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void ExitGame()
    {
        // 게임을 종료하는 메소드
        Application.Quit();
    }
}
