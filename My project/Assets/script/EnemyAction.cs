using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    float Startpos;
    public int limitation;
    public float speed;
    public int[] Dir = new int[2];
    public int i = 0, j = 0;
    public int RandomDir;
    public float startpos, dis, Dis, Position;
    bool Spot_Player;
    public LayerMask player;
    public Transform Visibility;
    public GameObject player_character;
    public float Coefficient;
    public bool spot;
    public Rigidbody2D rbe;
    public float jumpforce;
    public int test_movement;
    public GameObject player_position, enemy_position;
    public float x;
    public int ecurrentHealth;
    public int emaxHealth;
    PlayerAction con;

    // Start is called before the first frame update
    void Start()
    {
        Startpos = transform.position.x;
        rbe = GetComponent<Rigidbody2D>();
        ecurrentHealth = emaxHealth;
        con = GetComponent<PlayerAction>();
    }

    // Update is called once per frame
    void Update()
    {
        Position = transform.position.x;
        Dis = Startpos - Position;
        if (Dis <= 0)
        {
            dis = -1 * Dis;
        }
        else
        {
            dis = Dis;
        }
        if (ecurrentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Move();
    }
    void Move() 
    {
        Spot_Player = Physics2D.OverlapCircle(Visibility.position, 5f, player);
        if (Spot_Player)
        {
            SpotPlayer();
            spot = true;
            test_movement = 1;
        }
        else
        {
            RandomMove();
            spot = false;
            test_movement = 2;
        }
    }
    void RandomMove()
    {
        if (i == 75)
        {
            RandomDir = Random.Range(0, 2);
            i = 0;
            if (RandomDir == 0)
            {
                speed = speed * -1;
            }
        }
        if (dis > limitation && !spot)
        {
            if (speed > 0 && Dis < 0)
            {
                speed = speed * -1;
            }
            if (speed < 0 && Dis > 0)
            {
                speed = speed * -1;
            }
            i = 0;
        }
        transform.Translate(speed, 0, 0);
        i++;
    }
    void SpotPlayer()
    {
        Vector3 Enemy_moving = enemy_position.transform.position - player_position.transform.position;
        if (Enemy_moving.x < 0)
        {
            x = 1 / (Enemy_moving.x * -1);
        }
        else
        {
            x = 1 / (Enemy_moving.x);
        }
        transform.Translate(Enemy_moving.x * x * Coefficient, 0, 0);
        if (enemy_position.transform.position.y < player_position.transform.position.y)
        {
            if (j >= 100)
            {
                rbe.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
                j = 0;
            }
        }
        j++;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerAction player = other.gameObject.GetComponent<PlayerAction>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("hitbox"))
        {
            eChangeHealth(-1);
        }
    }

    public void eChangeHealth(int amount)
    {
        ecurrentHealth = Mathf.Clamp(ecurrentHealth + amount, 0, emaxHealth);
        Debug.Log("Enemy" + ecurrentHealth + "/" + emaxHealth);
    }

}
