using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> ConnectedRooms;
    private void Awake()
    {
        if (!ConnectedRooms.Contains(this))
            ConnectedRooms.Add(this);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals(PlayerManager.Instance.Player))
        {
            RoomManager.Instance.UpdateRooms(this);
        }
        
    }
  
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

