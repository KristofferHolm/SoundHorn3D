using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioClip CurrentPlayerAudioClip;

    private void Awake()
    {
        Screen.lockCursor = true;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = !Screen.lockCursor;
    }
}
