using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchColorPuzzle : MonoBehaviour
{
    public Color[] Colors = {
        new Color(0.25f,0,0),
        new Color(0.5f,0,0),
        new Color(1,0,0)
        };
    public PitchColor Small, Medium, Big;
    public AudioClip A_Small, A_Medium, A_Big;

    public Color GetColorOfPitch(PitchColor pitch, AudioClip a)
    {
        if(pitch == Big)
        {
            if (a == A_Small)
                return Colors[0];
            else if (a == A_Medium)
                return Colors[1];
        }
        if(pitch == Medium)
        {
            if (a == A_Small)
                return Colors[1];
        }
        return Colors[2];
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
        print("GG");
    }
}
