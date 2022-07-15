using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Localization : MonoBehaviour
{
    private Item item;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI language;
    private string lang;

    private void Start()
    {
        if (language != null)
        {
            lang = language.text;
            item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Localization/" + lang + ".json"));
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        else
        {
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            item = null;
        }
    }

    public void ChangeLang()
    {
        text.text = "1";
        if (language != null && !scoreManager.isMoreZero())
        {
            lang = language.text;
            text.text = item.Pl;
            gameManager.SetRest(item.Res);
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
        else if (language != null && scoreManager.isMoreZero())
        {
            lang = language.text;
            text.text = item.Res;
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
        else
        {
            text.text = "2";
        }
    }
    
    public void Push()
    {
        gameManager.Push();
    }
}