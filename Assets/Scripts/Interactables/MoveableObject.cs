using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveableObject : Interactable
{
    public Transform EndPoint;
    public bool moved = false;
    public override void Interact()
    {
        if (!interactable) return;
        if (SoundObjectManager.Instance.CheckCategoryForSound(Sound, SoundObjectCategory.friction))
        {
            MoveAction();
            PlaySound(false);
        }
        else
            base.Interact();
    }

    private void MoveAction()
    {
        var startPos = transform.position;
        transform.DOMove(EndPoint.position, Sound.length).OnComplete(()=>
        {
            moved = !moved;
            EndPoint.position = startPos;
        });
    }
}
