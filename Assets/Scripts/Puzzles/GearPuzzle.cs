using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GearPuzzle : MonoBehaviour
{

    public Gear SmallGear, MedGear, BigGear;
    public AudioClip Slow, Normal, Fast;
    public GameObject Door;
    public bool CheckIfPuzzleIsDone()
    {
        if(SmallGear.Sound == Fast &&
            MedGear.Sound == Normal &&
            BigGear.Sound == Slow)
        {
            if(SmallGear.isPlaying &&
                MedGear.isPlaying &&
                BigGear.isPlaying)
            {
                CompletePuzzle();
                return true;
            }

        }
        return false;
    }

    private void CompletePuzzle()
    {
        SmallGear.ResetSound(2.75f);
        MedGear.ResetSound(2.75f);
        BigGear.ResetSound(2.75f);
        SmallGear.interactable = false;
        MedGear.interactable = false;
        BigGear.interactable = false;
        SmallGear.transform.DOLocalMoveZ(0, 2.75f).SetEase(Ease.InOutExpo);
        BigGear.transform.DOLocalMoveZ(0, 2.75f).SetEase(Ease.InOutExpo);
        MedGear.transform.DOLocalMoveZ(0, 2.75f).SetEase(Ease.InOutExpo).OnComplete(()=>
        {
            Door.transform.DOLocalRotate(Vector3.up * 90, 3);
            Door.transform.DOMove(transform.position - transform.right*0.75f, 3);
        });
        
    }
}
