using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI score;
    private string rec;
    [SerializeField] private TextMeshProUGUI bestScore;
    private int sumRank;
    private int best;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            best = PlayerPrefs.GetInt("SaveScore");
        }
        else
        {
            best = 0;
        }
    }

    public void SetRest(string res)
    {
        rec = res;
        ShowBest();
    }

    private void Start()
    {
        Best();
        ShowBest();
        score = GetComponent<TextMeshProUGUI>();
        sumRank = 0;
        score.text = sumRank.ToString();
    }

    public void AddRank(int rank)
    {
        sumRank += rank;
        score.text = sumRank.ToString();
        Best();

    }

    private void Best()
    {
        if (best < sumRank)
        {
            best = sumRank;
            PlayerPrefs.SetInt("SaveScore", best);
            ShowBest();
        }
    }

    private void ShowBest()
    {
        bestScore.text = rec + ": " + best.ToString();
    }
}