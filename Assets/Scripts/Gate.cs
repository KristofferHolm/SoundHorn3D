using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Gate : MonoBehaviour
{
 
    public GameObject GateObj;
    public BoxCollider InvisibleBlock;
    //90 / 5 = 18
    private readonly float tickDegree = 22.5f;
    // 0 is up, and 4 is all down.
    public int ticks = 0;
    private bool completed;
    public void Tick(bool down, float duration)
    {
        ticks += down ? 1 : -1;
        completed = (ticks >= 4);
        if(ticks<0)
        {
            ticks = 0;
            return;
        }
        if(ticks > 4)
        {
            ticks = 4;
            return;
        }

        GateObj.transform.DOLocalRotate(Vector3.back * tickDegree*ticks, duration, RotateMode.Fast).SetEase(Ease.OutBounce).OnComplete(()=>
        {
            InvisibleBlock.enabled = !completed;
        });
    }

}
