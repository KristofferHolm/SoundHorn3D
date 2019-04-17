using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PitchColorPuzzle : MonoBehaviour
{
    public PitchColor Small, Medium, Big;
    public AudioClip A_Small, A_Medium, A_Big;

    public float GetColorOfPitch(PitchColor pitch, AudioClip a)
    {
        var str = 0f; 

        if (a == A_Small)
            str = 1;
        else if (a == A_Medium)
            str = 2;
        else if (a == A_Big)
            str = 4;

        if (pitch == Small)
            str /= 1;
        else if (pitch == Medium)
            str /= 2;
        else if (pitch == Big)
            str /= 4;

        return str;
    }

    public bool CheckIfPuzzleIsDone()
    {
        if (Small.Sound == A_Small &&
            Medium.Sound == A_Medium &&
            Big.Sound == A_Big)
        {
            if (Small.isPlaying &&
                Medium.isPlaying &&
                Big.isPlaying)
            {
                CompletePuzzle();
                return true;
            }

        }
        return false;
    }

    private void CompletePuzzle()
    {
        var vector = transform.position + Vector3.up * 1.25f;
        transform.DOJump(vector, .5f, 1, A_Small.length);
    }
}
