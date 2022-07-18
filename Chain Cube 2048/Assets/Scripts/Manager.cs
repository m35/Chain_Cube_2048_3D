using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Manager : MonoBehaviour
{
    private Item item;
    private string lang;
    [SerializeField] private GameObject play;
    private string rest;
    private bool isAct;
    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private GameObject player;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject[] buttonsLan;

    private void Awake()
    {
        lang = "UA";
        if (PlayerPrefs.HasKey("SaveLang"))
        {
            lang = PlayerPrefs.GetString("SaveLang");
        }
        PlayerMovement.isActiveForRestart = true;
        StartCoroutine(ReloadFile());
    }

    private IEnumerator ReloadFile()
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/"+ lang +".json");

        yield return data;

        item = JsonUtility.FromJson<Item>(data.text);
        ChangeLang();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("LoadS", 0);
            Application.Quit();
        }
    }

    public void ChangeLang()
    {
        if (item != null)
        {
            playText.text = item.Pl;
            rest = item.Res;
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
        else
        {
            playText.text = "";
            rest = "";
            scoreManager.SetRest("");
        }        
    }
    
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        /*
        if (PlayerPrefs.HasKey("SaveLang"))
        {
            lang = PlayerPrefs.GetString("SaveLang");
            ChangeLang();
        }
        else
        {
            lang = "EN";
            ChangeLang();
        }
        */
        int a = 0;
        if (PlayerPrefs.HasKey("LoadS"))
        {
            a = PlayerPrefs.GetInt("LoadS");
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
        if (playerMovement != null)
        {
            Debug.Log("not_null");
            if (playerMovement.isActiveAndEnabled)
            {
                Debug.Log("not_off");
                playerMovement.OffCube();
            }
            else
            {
                Debug.Log("off");
            }
        }
        else
        {
            Debug.Log("null");
        }
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
