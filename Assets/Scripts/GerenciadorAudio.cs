using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorAudio : MonoBehaviour
{
    static AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public static void TocarSom(AudioClip clip)
    {
        Debug.Log("toca");
        audioSrc.clip = clip;
        audioSrc.Play();
    }
}
