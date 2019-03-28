using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
public class PitchColor : Interactable
{
    PitchColorPuzzle pitchColorPuzzle;
    public bool isPlaying;
    Material mat;
    private void Start()
    {
        pitchColorPuzzle = GetComponentInParent<PitchColorPuzzle>();
        mat = GetComponent<MeshRenderer>().material;
    }
    public override void Interact()
    {
        if (!interactable) return;
        if (SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.pitch))
            SetColor();
        SetAudioSettings(SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.continuous));
        PlaySound(false);
    }

    private void SetColor()
    {
        print("Setting color");
        var soundLength = Sound.length;
        SetIsPlaying(soundLength);
        pitchColorPuzzle.CheckIfPuzzleIsDone();
        var color = pitchColorPuzzle.GetColorOfPitch(this,AudioSource.clip);
        //mat.DORestart(false);
        mat.DOColor(color, soundLength / 2.0f).SetEase(Ease.InExpo).OnComplete(() => mat.DORewind(true));
    }
    void SetIsPlaying(float soundLength)
    {
        int wait = (int)(soundLength * 1000);
        isPlaying = true;
       new Thread(() =>
       {
           Thread.Sleep(wait);
           isPlaying = false;
       }).Start();
    }
}
