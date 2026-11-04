using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio",menuName = "随机音效")]
public class RandoPlayAudio : AudioBase
{
    //随机播放的文件
    public List<AudioClip> clipList;
    public override void Play(AudioSource source)
    {
        if (clipList.Count == 0)
        {
            return;
        }

        source.clip = clipList[Random.Range(0,clipList.Count)]; 
    }
}
