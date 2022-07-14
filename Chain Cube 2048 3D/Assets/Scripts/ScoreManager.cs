using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI score;
    private int sumRank;

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        sumRank = 0;
        score.text = sumRank.ToString();
    }

    public void AddRank(int rank)
    {
        sumRank += rank;
        score.text = sumRank.ToString();
    }
}
