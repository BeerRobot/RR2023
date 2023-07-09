using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.6f, 1.3f);
        GetComponent<AudioSource>().Play();
    }

}
