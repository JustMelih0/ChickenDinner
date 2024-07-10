using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int difficultyIndex = 1;
    [SerializeField]private float levelUpTimer;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]public bool scoreIncrease = true ;
    public int myScore {get; private set;} 
    [Range(1,5)][SerializeField]private int maxLevel=1;

    [SerializeField]private GameObject endScreen;
    [SerializeField]private GameObject pauseScreen;

    private void Awake() {
        if (Instance==null)
        {
            Instance=this;
        }
    }
    private void Start() 
    {
        InvokeRepeating(nameof(LevelUp),levelUpTimer,levelUpTimer);
    }
    private void LevelUp()
    {
        difficultyIndex++;
        difficultyIndex = Mathf.Clamp(difficultyIndex,1,maxLevel);

        if (difficultyIndex == maxLevel)
        {
            CancelInvoke(nameof(LevelUp));
            return;
        }
    }
    public void SetScore(int addScore)
    {
        if (!scoreIncrease)
        {
            return;
        }
        myScore+=addScore*difficultyIndex;
        scoreText.text=myScore.ToString();
        HighScoreControl();
    }
    private void HighScoreControl()
    {
        if(PlayerPrefs.GetInt("highScore") < myScore)
        {
            PlayerPrefs.SetInt("highScore", myScore);
        }
    }
    public void GameOver()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseTheGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        Time.timeScale = 0;
    }
    public void ResumeTheGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        Time.timeScale = 1;
    }

    
}
