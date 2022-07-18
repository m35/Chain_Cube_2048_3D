using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJSON
{
    /*
    [Header("ItemJSON")]
    [SerializeField] private string Pl;
    [SerializeField] private string Res;
    [SerializeField] private string Rec;

    [Header("FileJSON")]
    [SerializeField] private string path;
    [SerializeField] private string fileName = "UA.json";
    */

    private string path;
    private string fileName = "EN.json";

    public void SaveToFile()
    {
        Debug.Log("SaveToFile");
        Item item = new Item();
        item.Pl = "Play";
        item.Res = "Again?";
        item.Rec = "Record";

        string json = JsonUtility.ToJson(item, prettyPrint: true);

        File.WriteAllText(path, contents: json);
    }

    public void ChangeLang(string lang)
    {
        Debug.Log("ChangeLangJSON");
        fileName = lang + ".json";
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, fileName);
#else
        path = Path.Combine(Application.dataPath, fileName);
#endif
        Debug.Log(path);
    }

    public Item LoadFromFile()
    {
        Debug.Log("LoadFromFile");
        if (!File.Exists(path))
        {
            Debug.Log("Error read");
            Item iteml = new Item();
            iteml.Pl = "file";
            iteml.Res = "file";
            iteml.Rec = "no";
            return iteml;
        }

        string json = File.ReadAllText(path);
        Debug.Log(json);
        Item item = JsonUtility.FromJson<Item>(json);

        //this.Pl = item.Pl;
        //this.Res = item.Res;
        //this.Rec = item.Rec;

        Debug.Log("Success");

        return item;
    }

    private void Awake()
    {
        Debug.Log("AwakeJSON");
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, fileName);
#else
        path = Path.Combine(Application.dataPath, fileName);
#endif
    }

    public LoadJSON()
    {
        Debug.Log("LoadJSON");
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, fileName);
#else
        path = Path.Combine(Application.dataPath, fileName);
#endif
        //SaveToFile();
    }
}