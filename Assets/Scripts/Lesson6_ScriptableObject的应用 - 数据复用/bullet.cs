using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public BulletInfo info;

    void Update()
    {
        transform.Translate(Vector3.forward * info.speed);
    }
}
