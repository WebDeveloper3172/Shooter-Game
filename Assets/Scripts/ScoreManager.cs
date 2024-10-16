using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI actualScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    public int score = 0;
    public int highscore = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore" , 0);
        bestScoreText.text = "BEST COINS SCORE : " + highscore;
    }
    public void Save(int count)
    {
        score = count;
        bestScoreText.text = "BEST COINS SCORE : " + highscore;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }
}
