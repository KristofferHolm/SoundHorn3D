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
        if (!SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.pitch))
        {
            base.Interact();
            return;
        }
        SetColor();
        SetAudioSettings(SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.continuous));
        PlaySound(false);
    }

    private void SetColor()
    {
        var soundLength = Sound.length;
        SetIsPlaying(soundLength);
        pitchColorPuzzle.CheckIfPuzzleIsDone();
        var colorStrength = pitchColorPuzzle.GetColorOfPitch(this,Sound);
        var color = Color.black;
        color.r = colorStrength;
        var time = soundLength / (1 + (colorStrength * 2.0f));
        // Debug.Log($"Time: {time} - Color: {color} - Time2: {soundLength - time}");
        mat.DOColor(color, time).SetEase(Ease.OutExpo).OnComplete(() =>
        mat.DOColor(Color.white, soundLength - time).SetEase(Ease.InExpo));
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
