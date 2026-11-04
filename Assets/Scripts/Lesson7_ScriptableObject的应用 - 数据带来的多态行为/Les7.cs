using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Les7 : MonoBehaviour
{
    public AudioBase audioBase;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioBase.Play(this.GetComponent<AudioSource>());
        }
    }
}
