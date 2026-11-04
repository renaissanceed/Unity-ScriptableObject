using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletInfo",menuName = "子弹数据")]
public class BulletInfo1 : SingScriptableObject<RoleInfo>
{
    public float speed;
    public int atk;
}
