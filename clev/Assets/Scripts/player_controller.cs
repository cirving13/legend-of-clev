using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{
    public static int health = 10;
    public static int damage = 1;
    public static int experience;
    public static int gold;
    private Rigidbody2D rb;
    private float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        Move();
        CheckDead();
        CheckPause();
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("demo");
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            pause.isPause = false;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pause.isPause = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Wizard":
                health -= wizard_controller.damage;
                sound_manager.PlaySlap();
                break;
            case "Orc":
                health -= orc_controller.damage;
                sound_manager.PlaySlap();
                break;
            case "Health":
                health = 10;
                break;
            case "Gold":
                gold += 10;
                break;
        }
    }
    void CheckDead()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            game_over.isPlayerDead = true;
        }
    }
}
