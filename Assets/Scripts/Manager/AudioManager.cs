using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip chooseNumber;
    public AudioClip pairClear;
    public AudioClip pop2;
    public AudioClip rowClear;

    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClipOneShot(AudioClip _audioClip)
    {
        audioSource.PlayOneShot(_audioClip);
    }

    public void PlayPop2OneShot()
    {
        audioSource.PlayOneShot(pop2);
    }
}
