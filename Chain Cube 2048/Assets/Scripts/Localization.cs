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
    [SerializeField] private Manager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI language;
    private string lang;

    private void Awake()
    {
        if (language != null)
        {
            lang = language.text;
            StartCoroutine(ReloadFile());
        }
    }

    private IEnumerator ReloadFile()
    {
        WWW data = new WWW(Application.streamingAssetsPath + "/" + lang + ".json");

        yield return data;

        item = JsonUtility.FromJson<Item>(data.text);
    }

    private void Start()
    {
        if (language != null)
        {
            lang = language.text;
            gameManager = FindObjectOfType<Manager>();
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        else if (language == null && gameObject.tag != "Play")
        {
            gameManager = FindObjectOfType<Manager>();
            scoreManager = FindObjectOfType<ScoreManager>();
            item = null;
        }
    }

    public void ChangeLang()
    {
        if (language != null && PlayerMovement.isActiveForRestart)
        {
            lang = language.text;
            text.text = item.Pl;
            gameManager.SetRest(item.Res);
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
        else if (language != null && !PlayerMovement.isActiveForRestart)
        {
            lang = language.text;
            text.text = item.Res;
            scoreManager.SetRest(item.Rec);
            PlayerPrefs.SetString("SaveLang", lang);
        }
    }
    
    public void Push()
    {
        gameManager.Push();
    }
}