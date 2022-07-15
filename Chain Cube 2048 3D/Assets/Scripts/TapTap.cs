using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTap : MonoBehaviour
{    
    [SerializeField] private GameObject exit;

    public void Exit()
    {
        Debug.Log(000);
        PlayerPrefs.SetInt("LoadS", 0);
        Application.Quit();
    }
}