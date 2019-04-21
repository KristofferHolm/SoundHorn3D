using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gear : Interactable
{
    public bool ClockwiseRotation = true;
    [HideInInspector]
    public bool isPlaying;
    float speed;
    GearPuzzle gearPuzzle;

    private void Start()
    {
        gearPuzzle = GetComponentInParent<GearPuzzle>();
        if (SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.tempo))
        {
            SetSpeed();
            SetAudioSettings(true);
            isPlaying = false;
        }
    }
    public void Update()
    {
        if (isPlaying)
        {
            transform.Rotate(ClockwiseRotation ? Vector3.down: Vector3.up, speed * Time.deltaTime, Space.Self);
        }
    }
    public override void Interact()
    {
        if (!interactable) return;
        if(SoundObjectManager.Instance.CheckCategoryForSound(Sound,SoundObjectCategory.tempo))
        {
            isPlaying = !isPlaying;
            SetSpeed();
            SetAudioSettings(true);

            PlayStopSound(isPlaying);
            if (isPlaying)
            {
                if (gearPuzzle.CheckIfPuzzleIsDone())
                    return;
            }
            SetUninteractableForSeconds(0.2f);
        }
        else
        {
            isPlaying = false;
            SetAudioSettings(false);
            base.Interact();
        }
    }
    public override void SwapSound()
    {
        if (SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.tempo))
        {
            PlayStopSound(false);
            isPlaying = false;
        }
        base.SwapSound();
    }
    private void SetSpeed()
    {
        if (Sound == gearPuzzle.Slow) speed = 10;
        if (Sound == gearPuzzle.Normal) speed = 20;
        if (Sound == gearPuzzle.Fast) speed = 30;
    }
    public void ResetSound(float waitFor)
    {
        AudioSource.Stop();
        isPlaying = false;
        StartCoroutine(PlayDelayed(waitFor));
    }
    IEnumerator PlayDelayed(float waitFor)
    {
        yield return new WaitForSeconds(waitFor);
        isPlaying = true;
        AudioSource.Play();
    }
}
