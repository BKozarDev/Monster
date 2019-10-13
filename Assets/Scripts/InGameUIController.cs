using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour
{
    public static bool isHuntering;

    [Range(0, 3)]
    public int score = 0;
    public int maxScore = 3;
    //public bool isHuntering;
    public bool InPause
    {
        get
        {
            return pauseLayout.activeSelf;
        }
    }

    public GameObject eye;
    public Text scoreText;
    public GameObject pauseLayout;

    private int prevScore;
    private bool isHunteringPrev;

    void Start()
    {
        isHuntering = false;
        scoreText.text = $"{score}/{maxScore}";
    }

    void Update()
    {
        if (score != prevScore)
        {
            scoreText.text = $"{score}/{maxScore}";
            prevScore = score;
        }
        
        if (isHuntering != isHunteringPrev)
        {
            if (isHuntering)
                eye.SetActive(true);
            else
                eye.SetActive(false);
            isHunteringPrev = isHuntering;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseLayout.SetActive(!pauseLayout.activeSelf);
        }
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
