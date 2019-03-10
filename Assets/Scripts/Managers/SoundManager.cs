using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    public void PlaySoundOneShot(AudioClip audio)
    {
        var source = PlayerManager.Instance.Cam.gameObject.AddComponent<AudioSource>();
        source.clip = audio;
        source.Play();
        Destroy(source,audio.length);
    }

    public AudioClip UI_Error;
    public AudioClip UI_Swap;
    public AudioClip UI_PlayStop;
    
}
