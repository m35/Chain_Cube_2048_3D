using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTap : MonoBehaviour
{    
    [SerializeField] private GameObject exit;

    public void Exit()
    {
        PlayerPrefs.SetInt("LoadS", 0);
        Application.Quit();
    }
}