using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject[] roomsPrefabs;
    public int quantityCenterRooms;
    public int quantityAlternativeRooms;
    public float roomWidth;

    //if the initial rooms is in scene before starts, roomsInScene = 1, if it is generate in start roomsInScene = 0
    private int totalRoomsInScene = 0;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        totalRoomsInScene++;
        int rightRooms = 0;
        int leftRooms = 0;

        GameObject initialPoint = Instantiate(initialRoom, Vector3.zero, Quaternion.identity);

        for (int i = 0; i < quantityCenterRooms; i++)
        {
            GameObject temp = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)], new Vector3(0, initialPoint.transform.position.y + roomWidth * totalRoomsInScene, 0), Quaternion.identity);

            for (int a = 0; a < quantityAlternativeRooms; i++)
            {
                int rand = Random.Range(0, 100);

                if (rand < 99) //right
                {
                    SpawnPrefab(new Vector3(temp.transform.position.x + roomWidth * rightRooms, temp.transform.position.y, temp.transform.position.z));

                    rightRooms++;
                }
            }

            totalRoomsInScene++;
        }
    }

    void SpawnPrefab(Vector3 position)
    {
        Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)], position, Quaternion.identity);
    }
}
