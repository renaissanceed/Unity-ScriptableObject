using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="zhang",menuName ="data数据",order = 0)]
public class MyData : ScriptableObject
{
    public int i;
    public float h;
    public AudioClip clip;
    public GameObject obj;
}
