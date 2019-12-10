using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc_controller : MonoBehaviour
{
    public int health = 2;
    public static int damage = 1;
    public int experienceDrop = 2;
    public float speed = 2f;
    private Rigidbody2D rb;
    public Transform player;
    private Transform orc;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        orc = transform;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
    }

    void Update()
    {
        Move();
        CheckDead();
    }

    void Move()
    {
        transform.position += (player.position - orc.position) * speed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            health -= player_controller.damage;
        }
    }
    void CheckDead()
    {
        System.Random r = new System.Random();
        if (health <= 0) 
        {
            Destroy(this.gameObject);
            player_controller.experience += experienceDrop; //gives exp on kill
            player_controller.gold += r.Next(1,6); //gives gold on kill
        }
    }
}
