using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeChangeColliderChecker : MonoBehaviour
{
    Action<bool> TriggerEvent;
    public void Trigger(Action<bool> callback)
    {
        TriggerEvent = callback;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            TriggerEvent.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerEvent.Invoke(false);
        }
    }
}
