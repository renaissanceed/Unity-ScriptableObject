using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel2 : MonoBehaviour
{
    public Toggle togMusic;
    public Toggle togSound;

    public Slider sliderMusic;
    public Slider sliderSound;

    MusicData musicData;
    void Start()
    {
        musicData = ScriptableObject.CreateInstance<MusicData>();
        InitInfo();

        togMusic.onValueChanged.AddListener((t) =>
        {
            musicData.isMusic = t;
        });

        togSound.onValueChanged.AddListener((t) =>
        {
            musicData.isSound = t;
        });

        sliderMusic.onValueChanged.AddListener((s) =>
        {
            musicData.musicValue = s;
        });

        sliderSound.onValueChanged.AddListener((s) =>
        {
            musicData.soundValue = s;
        });
    }

    private void InitInfo()
    {
        sliderMusic.value = musicData.musicValue;
        sliderSound.value = musicData.soundValue;

        togMusic.isOn = musicData.isMusic;
        togSound.isOn = musicData.isSound;
    }
}
