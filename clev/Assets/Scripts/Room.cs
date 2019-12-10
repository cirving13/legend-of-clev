using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbors;
    private string[,] population;
    public Dictionary<string, GameObject> enemyPrefab;
    public Dictionary<string, GameObject> dropPrefab;

    public Room(int xCoordinate, int yCoordinate)
    {
        this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        this.neighbors = new Dictionary<string, Room>();
        this.population = new string[18, 10];
        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                this.population[x, y] = "";
            }
        }
        this.population[8, 5] = "Player";
        this.enemyPrefab = new Dictionary<string, GameObject>();
    }

    public Room(Vector2Int roomCoordinate)
    {
        this.roomCoordinate = roomCoordinate;
        this.neighbors = new Dictionary<string, Room>();
        this.population = new string[18, 10];
        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                this.population[x, y] = "";
            }
        }
        this.population[8, 5] = "Player";
        this.enemyPrefab = new Dictionary<string, GameObject>();
    }

    private bool IsFree(List<Vector2Int> region)
    {
        foreach (Vector2Int tile in region)
        {
            if (this.population[tile.x, tile.y] != "")
            {
                return false;
            }
        }
        return true;
    }

    private List<Vector2Int> FindFreeRegion(Vector2Int sizeInTiles)
    {
        List<Vector2Int> region = new List<Vector2Int>();
        do
        {
            region.Clear();
            Vector2Int centerTile = new Vector2Int(UnityEngine.Random.Range(2, 18 - 3), UnityEngine.Random.Range(2, 10 - 3));
            region.Add(centerTile);
            int initialXCoordinate = (centerTile.x - (int)Mathf.Floor(sizeInTiles.x / 2));
            int initialYCoordinate = (centerTile.y - (int)Mathf.Floor(sizeInTiles.y / 2));
            for (int xCoordinate = initialXCoordinate; xCoordinate < initialXCoordinate + sizeInTiles.x; xCoordinate += 1)
            {
                for (int yCoordinate = initialYCoordinate; yCoordinate < initialYCoordinate + sizeInTiles.y; yCoordinate += 1)
                {
                    region.Add(new Vector2Int(xCoordinate, yCoordinate));
                }
            }
        } while (!IsFree(region));
        return region;
    }

    public void PopulatePrefabs(int numberOfPrefabs, GameObject[] possibleEnemies)
    {
        for (int prefabIndex = 0; prefabIndex < numberOfPrefabs; prefabIndex++)
        {
            int choiceIndex = UnityEngine.Random.Range(0, possibleEnemies.Length);
            GameObject enemy = possibleEnemies[choiceIndex];
            List<Vector2Int> region = FindFreeRegion(new Vector2Int(1, 1));

            this.population[region[0].x, region[0].y] = enemy.name;
            this.enemyPrefab[enemy.name] = enemy;
        }
    } 
    
    public List<Vector2Int> NeighborCoordinates()
    {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));

        return neighborCoordinates;
    }

    public void Connect(Room neighbor)
    {
        string direction = "";
        if (neighbor.roomCoordinate.y < this.roomCoordinate.y)
        {
            direction = "N";
        }
        if (neighbor.roomCoordinate.x > this.roomCoordinate.x)
        {
            direction = "E";
        }
        if (neighbor.roomCoordinate.y > this.roomCoordinate.y)
        {
            direction = "S";
        }
        if (neighbor.roomCoordinate.x < this.roomCoordinate.x)
        {
            direction = "W";
        }
        this.neighbors.Add(direction, neighbor);
    }

    public string PrefabName()
    {
        string name = "Room_";
        foreach (KeyValuePair<string, Room> neighborPair in neighbors)
        {
            name += neighborPair.Key;
        }
        return name;
    }

    public Room Neighbor(string direction)
    {
        return this.neighbors[direction];
    }
    public void AddPopulationToTilemap()
    {
        for (int x = 0; x < 18; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (this.population[x, y] != "" && this.population[x, y] != "Player")
                {
                    GameObject enemy = GameObject.Instantiate(this.enemyPrefab[this.population[x, y]]);
                    enemy.transform.position = new Vector2(x - 9 + .5f, y - 5 + .5f);
                }
            }
        }
    }

}

