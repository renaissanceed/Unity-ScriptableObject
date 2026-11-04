using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="MusicData",menuName = "Music")]
public class MusicData : ScriptableObject
{
    public bool isMusic;
    public bool isSound;
    public float musicValue;
    public float soundValue;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/settingInfo.json"))
        {
            string str = File.ReadAllText(Application.persistentDataPath + "/settingInfo.json");
            JsonUtility.FromJsonOverwrite(str,this);
        }
    }

    public void SaveData()
    {
        string str = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + "/settingInfo.json", str);
    }
}
