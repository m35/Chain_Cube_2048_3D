using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingRank : MonoBehaviour
{
    [HideInInspector] public int rank;
    private TextMeshPro text;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = "+" + rank;
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        if (timeBtwShots < 0f)
        {
            OnAnimOver();
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void OnAnimOver()
    {
        Destroy(gameObject);
    }
}