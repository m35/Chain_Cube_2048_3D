using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private TextMeshPro score;
    private int sumRank;

    private void Start()
    {
        score = GetComponent<TextMeshPro>();
        sumRank = 0;
        score.text = sumRank.ToString();
    }

    public void AddRank(int rank)
    {
        sumRank += rank;
        score.text = sumRank.ToString();
    }
}
