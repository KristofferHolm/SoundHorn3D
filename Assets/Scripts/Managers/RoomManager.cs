using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private static RoomManager instance;
    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoomManager>();
            }
            return instance;
        }
    }

    public List<Room> rooms = new List<Room>();
    public void Awake()
    {
        rooms.AddRange(FindObjectsOfType<Room>()); 
    }


    public void UpdateRooms(Room currentRoom)
    {
        List<Room> activeRooms = currentRoom.ConnectedRooms;    
        foreach (var room in rooms)
        {
            if (activeRooms.Contains(room))
                room.Activate();
            else
                room.Deactivate();
        }

    }


}
