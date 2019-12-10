using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class dungeon_generator : MonoBehaviour
{
    [SerializeField]
    public int numRooms;
    private Room[,] rooms;
    private Room currentRoom;
    [SerializeField]
    private GameObject[] possibleEnemies;
    [SerializeField]
    private int numberOfEnemies;
    private static dungeon_generator instance;
    [SerializeField]
    private GameObject theEnd;
    [SerializeField]
    private GameObject[] possibleDrops;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            this.currentRoom = GenerateDungeon();
        }
        else
        {
            string roomName = instance.currentRoom.PrefabName();
            GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomName));
            Tilemap tilemap = roomObject.GetComponentInChildren<Tilemap>();
            instance.currentRoom.AddPopulationToTilemap();
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        string roomName = this.currentRoom.PrefabName();
        GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomName));
        Tilemap tilemap = roomObject.GetComponentInChildren<Tilemap>();
        this.currentRoom.AddPopulationToTilemap();
    }

    private Room GenerateDungeon()
    {
        int gridSize = 3 * numRooms;
        rooms = new Room[gridSize, gridSize];
        Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);
        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
        List<Room> createdRooms = new List<Room>();

        while (roomsToCreate.Count > 0 && createdRooms.Count < numRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();
            this.rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }
        int maximumDistanceToSpawn = 0;
        Room finalRoom = null;
        foreach (Room room in createdRooms)
        {
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
            foreach (Vector2Int coordinate in neighborCoordinates)
            {
                Room neighbor = this.rooms[coordinate.x,  coordinate.y];
                if (neighbor != null)
                {
                    room.Connect(neighbor);
                }
            }
            room.PopulatePrefabs(this.numberOfEnemies, this.possibleEnemies);
            room.PopulatePrefabs(1, this.possibleDrops);
            int distanceToSpawn = Mathf.Abs(room.roomCoordinate.x - initialRoomCoordinate.x) + Mathf.Abs(room.roomCoordinate.y - initialRoomCoordinate.y);
            if (distanceToSpawn > maximumDistanceToSpawn)
            {
                maximumDistanceToSpawn = distanceToSpawn;
                finalRoom = room;
            }
        }
        GameObject[] theEnd = { this.theEnd };
        finalRoom.PopulatePrefabs(1, theEnd);
        
        return this.rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();
        foreach (Vector2Int coordinate in neighborCoordinates)
        {
            if (this.rooms[coordinate.x, coordinate.y] == null)
            {
                availableNeighbors.Add(coordinate);
            }
        }
        int numberOfNeighbors = (int)Random.Range(1, availableNeighbors.Count);

        for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++)
        {
            float randomNumber = Random.value;
            float roomFrac = 1f / (float)availableNeighbors.Count;
            Vector2Int chosenNeighbor = new Vector2Int(0, 0);
            foreach (Vector2Int coordinate in availableNeighbors)
            {
                if (randomNumber < roomFrac)
                {
                    chosenNeighbor = coordinate;
                    break;
                }
                else
                {
                    roomFrac += 1f / (float)availableNeighbors.Count;
                }
            }
            roomsToCreate.Enqueue(new Room(chosenNeighbor));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }
    public void MoveToRoom(Room room)
    {
        this.currentRoom = room;
    }

    public Room CurrentRoom()
    {
        return this.currentRoom;
    }
}




