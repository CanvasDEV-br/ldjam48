using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject bossRoom;
    public GameObject[] roomsPrefabs;
    public int quantityCenterRooms;
    public int quantityAlternativeRooms;
    public float roomWidth;

    [Range((int)0, (int)100)]
    public int chancesToNull = 20; //what's the chance to NO spawn?
    [Range((int)0, (int)100)]
    public int chancesToRight = 50; //what's the chance to spawn for right?
    [Range((int)0, (int)100)]
    public int bossSpawnInTopCenter = 50; //what's the chance to spawn the room of boss in top center in finish row?

    //if the initial rooms is in scene before starts, roomsInScene = 1, if it is generate in start roomsInScene = 0
    private int totalCenterRoomsInScene = 0;
    private GameObject currentRoom; //current Alternative room
    private bool isRight; //if the last currentRoom is Right

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        GameObject temp = new GameObject();

        int rightRooms = 1;
        int leftRooms = 1;
        totalCenterRoomsInScene++;

        GameObject initialPoint = Instantiate(initialRoom, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < quantityCenterRooms; i++)
        {
            temp = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)], new Vector3(0, initialPoint.transform.position.y + roomWidth * totalCenterRoomsInScene, 0), Quaternion.identity);

            //---Side Rooms of center rooms---
            for (int j = 0; j < quantityAlternativeRooms; j++)
            {
                int rand = Random.Range(0, 100);

                if(rand >= chancesToNull)
                {
                    rand = Random.Range(0, 100);

                    if (rand <= chancesToRight) //right
                    {
                        SpawnPrefab(new Vector3(temp.transform.position.x + roomWidth * rightRooms, temp.transform.position.y, temp.transform.position.z));

                        rightRooms++;
                        isRight = true;
                    }
                    else //left
                    {
                        SpawnPrefab(new Vector3(temp.transform.position.x + (roomWidth * -leftRooms), temp.transform.position.y, temp.transform.position.z));

                        leftRooms++;
                        isRight = false;
                    }
                }
            }

            rightRooms = 1;
            leftRooms = 1;
            totalCenterRoomsInScene++;
        }

        //bossRoom spawn in center
        if (Random.Range(0, 100) <= bossSpawnInTopCenter)
        {
            Instantiate(bossRoom, new Vector3(0, temp.transform.position.y + roomWidth, temp.transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(bossRoom, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y + roomWidth, currentRoom.transform.position.z), Quaternion.identity);
        }
    }

    void SpawnPrefab(Vector3 position)
    {
       currentRoom = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)], position, Quaternion.identity);
    }
}
