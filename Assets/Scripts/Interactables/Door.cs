using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Door : Interactable
{
    public bool Open;
    public Transform DoorObj;
    public override void Interact()
    {
        if (!interactable) return;
        if (SoundObjectManager.Instance.CheckCategory(Sound).Equals(SoundObjectCategory.open))
            OpenAction();
        else if (SoundObjectManager.Instance.CheckCategory(Sound).Equals(SoundObjectCategory.close))
            CloseAction();
        base.Interact();
    }

    private void OpenAction()
    {
        if (Open) return;
        var col = DoorObj.GetComponent<Collider>();
        col.enabled = false;
        DoorObj.DORotate(Vector3.right, Sound.length).OnComplete(() =>
        {
            col.enabled = true;
            Open = true;
        });
    }
    private void CloseAction()
    {
        if (!Open) return;
        var col = DoorObj.GetComponent<Collider>();
        col.enabled = false;
        DoorObj.DORotate(Vector3.zero, Sound.length).OnComplete(() =>
        {
            col.enabled = true;
            Open = false;
        });
    }
}
