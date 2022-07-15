using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Localization : MonoBehaviour
{
    private Item item;
    private LangText langText;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI language;
    private string lang;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveLang"))
        {
            lang = PlayerPrefs.GetString("SaveLang");
        }
        else
        {
            lang = "EN";
        }
    }

    private void Start()
    {
        if (language != null)
        {
            Debug.Log(1);
            lang = language.text;
            item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Localization/" + lang + ".json"));
            //langText.SetData(item.Play, item.Restart, item.Record);
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            ChangeLang();
        }
        else
        {
            Debug.Log(2);
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            item = null;
        }
    }

    public void ChangeLang()
    {
        if (language != null)
        {
            lang = language.text;
            
            Debug.Log(item.Play);
            text.text = item.Play;
            gameManager.SetRest(item.Restart);
            scoreManager.SetRest(item.Record);
            
            /*
            Debug.Log(langText.play);
            text.text = langText.play;
            gameManager.SetRest(langText.restart);
            scoreManager.SetRest(langText.record);
            */
            PlayerPrefs.SetString("SaveLang", lang);
        }
    }
    /*
    public void ChangeLang(string lang)
    {
        text.text = item.Play;
        gameManager.SetRest(item.Restart);
        scoreManager.SetRest(item.Record);
        PlayerPrefs.SetString("SaveLang", lang);
    }
    */
    public void Push()
    {
        gameManager.Push();
    }

    [System.Serializable]
    public class Item
    {
        public string Play;
        public string Restart;
        public string Record;
    }

    public class LangText
    {
        public string play;
        public string restart;
        public string record;

        public void SetData(string Play, string Restart, string Record)
        {
            play = Play;
            restart = Restart;
            record = Record;
        }
    }
}
