using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject play;
    private string rest;
    private bool isAct;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private GameObject[] buttonsLan;
    [SerializeField] private GameObject player;

    private void Start()
    {
        int a = 0;
        if (PlayerPrefs.HasKey("LoadS"))
        {
            //a = PlayerPrefs.GetInt("LoadS");
        }

        if (a == 0)
        {
            isAct = true;
            play.SetActive(true);
            foreach (GameObject btn in buttonsLan)
            {
                btn.SetActive(true);
            }
            player.SetActive(false);
        }
        else
        {
            isAct = false;
            play.SetActive(false);
            foreach (GameObject btn in buttonsLan)
            {
                btn.SetActive(false);
            }
            player.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("LoadS", 0);
            Application.Quit();
        }
    }

    public void SetRest(string res)
    {
        rest = res;
    }

    public void Push()
    {
        if (isAct)
        {
            play.SetActive(false);
            foreach (GameObject btn in buttonsLan)
            {
                btn.SetActive(false);
            }
            player.SetActive(true);
        }
        else
        {
            Reload();
        }
    }

    public void Restart()
    {
        playText.text = rest;
        play.SetActive(true);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.OffCube();
        player.SetActive(false);

        isAct = false;
        foreach (GameObject btn in buttonsLan)
        {
            btn.SetActive(true);
        }
        PlayerPrefs.SetInt("LoadS", 1);
    }

    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
