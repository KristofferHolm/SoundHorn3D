using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LookCameraControls : MonoBehaviour
{
    public float LookSpeed = 1.0f;
    readonly float YMin = -0.666f;
    readonly float YMax = 0.666f;

    float currentLookSpeed;
    bool ducking = false;
    private void Start()
    {

        InputManager.Instance.Input_Duck += Duck;
    }
    void Duck()
    {
        ducking = !ducking;
        if (ducking)
            transform.DOLocalMove(Vector3.down * 0.25f, 0.5f).SetEase(Ease.OutCubic);
        else
            transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.OutCubic);
    }
    void Update()
    {
        currentLookSpeed = LookSpeed * Time.deltaTime;
        transform.localRotation = LockRotation(transform.localRotation* Quaternion.AngleAxis(currentLookSpeed * -InputManager.Instance.CameraMovementY, Vector3.right));
       
    }

    Quaternion LockRotation(Quaternion rot)
    {
        var temp = rot;
        temp.x = Mathf.Clamp(temp.x, YMin, YMax);
        return temp;
    }

}
