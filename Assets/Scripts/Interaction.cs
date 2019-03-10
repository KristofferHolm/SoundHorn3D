using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource AudioSource
    {
        get
        {
            if (audioSource != gameObject.GetComponent<AudioSource>())
                audioSource = gameObject.GetComponent<AudioSource>();
            return audioSource;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.Input_Interact += Interact;
        InputManager.Instance.Input_SwapSound += SwapSound;
        InputManager.Instance.Input_HearOwnSound += HearOwnSound;
    }

    private void HearOwnSound()
    {
        print("SPIL MIG");
        SoundManager.Instance.PlaySoundOneShot(SoundManager.Instance.UI_PlayStop);
        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
            return;
        }
        PlaySound(PlayerManager.Instance.CurrentPlayerAudioClip);
    }   

    private void SwapSound()
    {
        var hit = Raycast();
        if (hit is Interactable)
            hit.SwapSound();
    }

    void Interact()
    {
        var hit = Raycast();
        if(hit is Interactable)
            hit.Interact();
    }
    Interactable Raycast()
    {
        Ray ray = new Ray(PlayerManager.Instance.Cam.transform.position, PlayerManager.Instance.Cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Interactable");
        if (Physics.Raycast(ray, out hit,layerMask))
        {
            var interactable = hit.transform.GetComponent<Interactable>();
            if (interactable==null) interactable = hit.transform.GetComponentInParent<Interactable>();
            if (interactable ==null) Debug.LogError("den du prøver at klikke er null");
            return interactable;
        }
        return null;
    }
    void PlaySound(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }
}
