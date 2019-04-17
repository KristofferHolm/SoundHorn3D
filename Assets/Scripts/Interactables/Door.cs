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
        if (SoundObjectManager.Instance.GetCategory(Sound).Equals(SoundObjectCategory.open))
            OpenAction();
        else if (SoundObjectManager.Instance.GetCategory(Sound).Equals(SoundObjectCategory.close))
            CloseAction();
        base.Interact();
    }

    private void OpenAction()
    {
        if (Open) return;
        DoorObj.transform.DOLocalRotate(Vector3.up * -90, Sound.length).OnComplete(() =>
        {
            Open = true;
        });
    }
    private void CloseAction()
    {
        if (!Open) return;
        DoorObj.transform.DOLocalRotate(Vector3.up * 90, Sound.length).OnComplete(() =>
        {
            Open = false;
        });
    }
}
