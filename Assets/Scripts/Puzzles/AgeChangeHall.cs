using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeChangeHall : MonoBehaviour
{
    public Transform Start, End;
    public Collider TriggerCollider;
    bool inside;

    float distBetween;
    Vector3 dirBetween;
    Vector3 dirNorm;
    private void Awake()
    {
        dirBetween = End.position - Start.position;
        distBetween = dirBetween.magnitude;
        dirNorm = dirBetween / distBetween;
        TriggerCollider.gameObject.AddComponent<AgeChangeColliderChecker>().Trigger(TriggerChecker);
        
    }
    void TriggerChecker(bool enter)
    {
        if (enter)
            Age(PlayerManager.Instance.Player);
        else
            ResetPlayer();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Age(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        print("exit and now what??");
    }

    void Age(GameObject player)
    {
        var percentage = GetPercentage(player.transform);
        player.transform.localScale = Vector3.one - (Vector3.one * 0.5f * percentage);
    }
    float GetPercentage(Transform player)
    {
        var playerPos = player.position - Start.position;
        print("% " + playerPos.magnitude / distBetween);
        return playerPos.magnitude / distBetween;

    }

}
