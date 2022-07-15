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
            Debug.Log(1);
            lang = language.text;
            item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Localization/" + lang + ".json"));
            Debug.Log(item.Pl);
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        else
        {
            Debug.Log(3);
            gameManager = FindObjectOfType<GameManager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            item = null;
        }
    }

    public void ChangeLang()
    {
        if (language != null)
        {
            Debug.Log(2);
            text.text = item.Pl;
            gameManager.SetRest(item.Res);
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
    }

    public void Push()
    {
        gameManager.Push();
    }

    [System.Serializable]
    public class Item
    {
        public string Pl;
        public string Res;
        public string Rec;
    }
}