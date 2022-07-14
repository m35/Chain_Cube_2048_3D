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
    }

    private void Update()
    {
        if (timeBtwShots <= 0f)
        {
            OnAnimOver();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            return;
        }
    }

    public void OnAnimOver()
    {
        Destroy(gameObject);
    }
}
