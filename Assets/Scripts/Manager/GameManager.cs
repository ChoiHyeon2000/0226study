using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // static 변수로 자기자신의 타입을 담을 공간을 만든다.
    // 이 변수는 메모리에 딱 1개만 존재하게 된다.
    public static GameManager instance;

    // 게임 데이터를 관리할 변수들.
    public int totalScore = 0;
    private bool isGameOver = false;

    public float limitTime = 30.0f;
    private float limitTimer = 0.5f;

    public TMP_Text scoreText;

    public GameObject gameOver;

    public AudioClip audioRestart;

    private void Awake()
    {
        // 나 자신(GameManager)을 집어넣어서 초기화 한다.
        instance = this;

        limitTimer = 0.0f;

        UpdateUI();

        gameOver.SetActive(false);
    }

    private void Update()
    {
        // 1초에
        limitTimer += Time.deltaTime;
        Debug.Log("경과 시간: " + limitTimer);
        if (limitTimer >= limitTime)
        {
            SetGameOver(true);
        }
    }


    /// <summary>
    /// 점수 추가.
    /// </summary>
    /// <param name="amount">추가할 점수</param>
    public void AddScore(int amount)
    {
        if (isGameOver == true)
        {
            return;
        }

        //totalScore = totalScore + amount;
        totalScore += amount;
        Debug.Log("현재 점수 : " + totalScore);

        UpdateUI();
    }

    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;

        this.gameOver.SetActive(true);

        // 유니티의 시간을 멈추게 한다.
        Time.timeScale = 0.0f;

        Debug.Log("isGameOver = " + isGameOver);
    }

    void UpdateUI() 
    {
        scoreText.text = "Score : " + totalScore.ToString();
    }

    public void Restart() 
    {
        Time.timeScale = 1.0f;

        AudioManager.instance.PlaySFX(audioRestart);

        //SceneManager.Load

        // 현재 열려있는 씬의 이름을 다시 로딩.
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene() 
    {
        yield return new WaitForSeconds(1.0f);

        // 현재 열려있는 씬의 이름을 가져와서 다시 로딩
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

