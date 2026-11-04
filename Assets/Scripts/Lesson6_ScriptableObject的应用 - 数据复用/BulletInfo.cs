using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletInfo",menuName = "子弹数据")]
public class BulletInfo : ScriptableObject
{
    public float speed;
    public int atk;
}
