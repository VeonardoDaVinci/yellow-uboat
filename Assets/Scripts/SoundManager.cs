using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    public AudioClip MusicClip;
    public AudioClip ExplosionClip;
    public AudioSource MusicSource;
    public AudioSource SfxSource;

    void Awake()
    {
        if (!I)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
