using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door_controller : MonoBehaviour
{
    [SerializeField]
    string direction;
    void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player")
		{
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length == 0)
            {
                GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
                dungeon_generator dungeonGenerator = dungeon.GetComponent<dungeon_generator>();
                Room room = dungeonGenerator.CurrentRoom();
                dungeonGenerator.MoveToRoom(room.Neighbor(this.direction));
                SceneManager.LoadScene("demo");
            }
		}
	}
}
