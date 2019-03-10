using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }
            return instance;
        }
    }

    public readonly string Interact = "Fire1";
    public readonly string SwapSound = "Fire2";
    public readonly string HearOwnSound = "Fire3";
    public readonly string Duck = "Duck";

    public Action Input_Interact;
    public Action Input_SwapSound;
    public Action Input_HearOwnSound;
    public Action Input_Duck;

    private void Awake()
    {
        Input_Duck += () => Debug.Log("Duck");
        Input_HearOwnSound += () => Debug.Log("HearOwnSound");
        Input_Interact += () => Debug.Log("Interact");
        Input_SwapSound += () => Debug.Log("SwapSound");
    }
    public Vector3 TankMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var vec = new Vector2(x, y).normalized;
        return new Vector3(vec.x, 0, vec.y);
    }
    public float CameraMovementX
    {
        get
        {

            return Input.GetAxis("MouseX");
        }
    }
    public float CameraMovementY
    {
        get
        {
            return Input.GetAxis("MouseY");
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown(Interact))
            Input_Interact?.Invoke();
        if (Input.GetButtonDown(SwapSound))
            Input_SwapSound?.Invoke();
        if (Input.GetButtonDown(HearOwnSound))
            Input_HearOwnSound?.Invoke();
        if (Input.GetButtonDown(Duck))
            Input_Duck?.Invoke();
    }


}
