using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Lever : Interactable
{
    public Gate GateObj;
    public GameObject PulledLeverGraphic;
    public GameObject NotPulledLeverGraphic;
    public bool pulled = false;


    public AudioClip _2Ticks;
    public AudioClip _3Ticks;
    public override void Interact()
    {
        if (!interactable) return;
        base.Interact();
        if(SoundObjectManager.Instance.CheckCategoryForSound(Sound,SoundObjectCategory.ticks))
        {
            pulled = !pulled;
            PulledLeverGraphic.SetActive(pulled);
            NotPulledLeverGraphic.SetActive(!pulled);

            if (Sound == _2Ticks) TwoTicks();
            else if (Sound == _3Ticks) ThreeTicks();
            else print("fucking wat m8");
        }
    }

    private void TwoTicks()
    {
        StartCoroutine(Tick2Coroutine());
    }

    private IEnumerator Tick2Coroutine()
    {
        var firstTick = 0.05f;
        var secondTick = 0.148f;
        var end = Sound.length;
        yield return new WaitForSeconds(firstTick);
        GateObj.Tick(pulled,secondTick-firstTick);
        yield return new WaitForSeconds(secondTick-firstTick);
        GateObj.Tick(pulled,end-secondTick-0.2f);
    }
    private void ThreeTicks()
    {
        StartCoroutine(Tick3Coroutine());
    }
    private IEnumerator Tick3Coroutine()
    {
        var firstTick = .005f;
        var secondTick = .27f;
        var thirdTick = .4f;
        var end = Sound.length;
        yield return new WaitForSeconds(firstTick);
        GateObj.Tick(pulled, secondTick-firstTick);
        yield return new WaitForSeconds(secondTick-firstTick);
        GateObj.Tick(pulled, thirdTick- secondTick);
        yield return new WaitForSeconds(thirdTick-secondTick);
        GateObj.Tick(pulled,end- thirdTick-0.2f);
    }
}
