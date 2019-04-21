using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public abstract class Interactable : MonoBehaviour
{
    protected float SwapCooldown = 1.0f;
    [HideInInspector]
    public bool interactable = true;
    private AudioSource audioSource;
    protected AudioSource AudioSource
    {
        get
        {
            if (audioSource != gameObject.GetComponent<AudioSource>())
                audioSource = gameObject.GetComponent<AudioSource>();
            return audioSource;
        }
    }
    public AudioClip Sound;
    
    public virtual void Awake()
    {
        if (!gameObject.layer.Equals(LayerMask.NameToLayer("Interactable")))
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }
        print("My layer is: " + LayerMask.LayerToName(gameObject.layer));
    }

    public virtual void SwappingAnimation(float duration)
    {
        var scale = transform.localScale;
        transform.DOShakePosition(duration, 0.015f, 10, 90, false, false);
        transform.DOScale(scale * 0.9f, duration / 2).OnComplete(() => transform.DOScale(scale, duration / 2));
    }

    public virtual void Interact()
    {
        if (!interactable) return;
        SetAudioSettings(SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.continuous));
        PlaySound();
    }
    public virtual void SwapSound()
    {
        if (!interactable) return;
        PlayStopSound(false);
        if (Sound == null && PlayerManager.Instance.CurrentPlayerAudioClip == null)
        {
            PlaySound(true);
            return;
        }
        AudioClip temp = PlayerManager.Instance.CurrentPlayerAudioClip;
        PlayerManager.Instance.CurrentPlayerAudioClip = Sound;
        Sound = temp;
        AudioSource.PlayOneShot(SoundManager.Instance.UI_Swap);
        SwappingAnimation(SwapCooldown);
        SetUninteractableForSeconds(SwapCooldown);
    }
    public virtual void SetUninteractableForSeconds(float time)
    {
        interactable = false;
        new Thread(() =>
        {
            var milliseconds = time * 1000f;
            Thread.Sleep((int)milliseconds);
            interactable = true;
        }).Start();
    }
    protected void PlayStopSound(bool play)
    {
        AudioSource.clip = Sound;
        if (play) AudioSource.Play();
        else AudioSource.Stop();
       
    }

    protected void PlaySound(bool vibrate = true)
    {
        if (AudioSource.isPlaying) return;
        if (Sound == null)
        {
            if (vibrate) transform.DOShakePosition(0.25f, 0.025f, 50, 90, false, false);
            SetUninteractableForSeconds(0.25f);
            AudioSource.PlayOneShot(SoundManager.Instance.UI_Error);
            return;
        }
        else if(SoundObjectManager.Instance.CheckCategoryForSound(Sound,SoundObjectCategory.continuous))
        {
            PlayStopSound(!AudioSource.isPlaying);
            if (vibrate) transform.DOShakePosition(0.25f, 0.025f, 50, 90, false, false);
            SetUninteractableForSeconds(0.25f);
            return;
        }
        AudioSource.PlayOneShot(Sound);
        if(vibrate) transform.DOShakePosition(Sound.length, 0.025f, 25, 90, false, false);
        SetUninteractableForSeconds(0.25f);
    }
    protected virtual void SetAudioSettings(bool continous)
    {
        AudioSource.loop = continous;
    }
}
