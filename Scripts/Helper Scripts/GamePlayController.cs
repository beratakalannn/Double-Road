using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public float moveSpeed, distance_Factor = 1f;
    private float distanceMove;
    private bool gameJustStarted;

    public GameObject obstacle_Obj;
    public GameObject[] obstacle_List;
  

    [HideInInspector]
    public bool obstacleIsActive;

    private string Coroutine_Name = "SpawnObstacles";

    private Text scoreText;
    private Text starScoreText;

    private int starScoreCount, scoreCount;

    public GameObject pausePanel;
    public Animator pauseAnim;

    public GameObject gameoverPanel;
    public Animator gameoverAnim;
    public Text finalScoreText, bestScoreText, finalStarScoreText;

    public GameObject RewardPanel;



    private void Awake()
    {
        instance = this;
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        starScoreText = GameObject.Find("Star Text").GetComponent<Text>();

    }

    void Start()
    {
        gameJustStarted = true;
        GetObstacles();
        StartCoroutine(Coroutine_Name);
    }

    void Update()
    {
        MoveCamera();
       
    }


    void MoveCamera()
    {
        if (gameJustStarted)
        {
            if (!PlayerController.instance.playerDied)
            {
                if (moveSpeed < 12f)
                {
                    moveSpeed += Time.deltaTime * 5.0f;
                }
                else
                {
                    moveSpeed = 12;
                    gameJustStarted = false;
                }
            }
        }
        if (!PlayerController.instance.playerDied)
        { 
            Camera.main.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            UpdateDistance();
        }
       
    }

    void UpdateDistance()
    {
        distanceMove += Time.deltaTime * distance_Factor;
        float round = Mathf.Round(distanceMove);

        scoreCount = (int)round;
        scoreText.text = round.ToString();

        if (round >= 30f && round < 60f)
        {
            moveSpeed = 14f;
        }
        else if (round >= 60)
        {
            moveSpeed = 16f;
        }
    }



    void GetObstacles()
    {
        obstacle_List = new GameObject[obstacle_Obj.transform.childCount];

        for (int i = 0; i < obstacle_List.Length; i++)
        {
            obstacle_List[i] = obstacle_Obj.GetComponentsInChildren<ObstacleHolder>(true)[i].gameObject;
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (!PlayerController.instance.playerDied)
            {
                if (!obstacleIsActive)
                {
                    if (Random.value <= 0.85f)
                    {
                        int randomIndex = 0;

                        do
                        {
                            randomIndex = Random.Range(0, obstacle_List.Length);
                        } while (obstacle_List[randomIndex].activeInHierarchy);
                        obstacle_List[randomIndex].SetActive(true);
                        obstacleIsActive = true; 
                    }
                }
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void UpdateStarScore()
    {
        starScoreCount++;
        starScoreText.text = starScoreCount.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseAnim.Play("slide");
    }

    public void ResumeGame()
    {
        pauseAnim.Play("out");

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


    public void GameOverPanel()
    {
        Time.timeScale = 0;
        gameoverPanel.SetActive(true);
        gameoverAnim.Play("slide");

        finalScoreText.text = scoreCount.ToString();
        finalStarScoreText.text = starScoreCount.ToString();

        if (GameManager.instance.scoreCount < scoreCount)
        {
            GameManager.instance.scoreCount = scoreCount;
        }

        bestScoreText.text = GameManager.instance.scoreCount.ToString();
        GameManager.instance.starScore += starScoreCount;
        GameManager.instance.SaveGameData();
    }

    public void ContinueGame()
    {
        if (starScoreCount >= 0)
        {
            starScoreCount -= 0;
            PlayerController.instance.playerDied = false;
            Time.timeScale = 1;
            moveSpeed = 10;
            PlayerController.instance.explosion.SetActive(false);
            PlayerController.instance.player.SetActive(true);
            gameoverPanel.SetActive(false);
            SoundManager.instance.Bg();
            PlayerController.instance.playerJump = false;
        }
    }

    public void RecieceReward()
    {
        starScoreCount += 15;
        RewardPanel.SetActive(false);
    }
}







