using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionRange = 1;

    Coroutine disableRoutine;
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

    public Func<object> Action { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.Input_Interact += Interact;
        InputManager.Instance.Input_SwapSound += SwapSound;
        InputManager.Instance.Input_HearOwnSound += HearOwnSound;
        disableRoutine = StartCoroutine(DisableCoroutine());


    }
    private void Update()
    {
        PlayerManager.Instance.InteractionMarker(Raycast());
    }
    private void HearOwnSound()
    {
        SoundManager.Instance.PlaySoundOneShot(SoundManager.Instance.UI_PlayStop);

        if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
            return;
        }
        PlaySound(PlayerManager.Instance.CurrentPlayerAudioClip);
        StartCoroutine(DisableCoroutine());
    }


    private IEnumerator DisableCoroutine()
    {
        interactionRange = 0;
        while (AudioSource.isPlaying)
        {
            yield return null;
        }
        interactionRange = 1;
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
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Interactable");
        if (Physics.Raycast(ray, out hit, interactionRange, layerMask))
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
