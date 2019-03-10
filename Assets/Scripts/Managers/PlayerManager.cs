using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }
            return instance;
        }
    }
    private Camera _camera;
    public Camera Cam
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            return _camera;
        }
    }
    private GameObject player;
    public GameObject Player
    {
        get
        {
            if(player == null)
                player = Cam.transform.root.gameObject;
            return player;
        }
    }

    public Image Marker;
    public AudioClip CurrentPlayerAudioClip;

    public void InteractionMarker(Interactable interactableInRange)
    {
        if (interactableInRange == null) UpdateGraphic(0);
        else if (interactableInRange.interactable) UpdateGraphic(1);
        else UpdateGraphic(0);
    }

    private void UpdateGraphic(int graphic)
    {
        switch (graphic)
        {
            case 0:
                Marker.gameObject.SetActive(false);
                break;
            case 1:
                Marker.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        Marker.gameObject.SetActive(false);
        Screen.lockCursor = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = !Screen.lockCursor;
    }
}