using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard_controller : MonoBehaviour
{
    public int health = 1;
    public static int damage = 2;
    public int experienceDrop = 4;
    public float speed = 1f;
    private Rigidbody2D rb;
    public Transform player;
    private Transform wizard;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wizard = transform;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckDead();
        Move();
    }
    void Move()
    {
        transform.position += (player.position - wizard.position) * speed * Time.deltaTime;
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
            player_controller.experience += experienceDrop;
            player_controller.gold += r.Next(1, 6);
        }
    }
}
