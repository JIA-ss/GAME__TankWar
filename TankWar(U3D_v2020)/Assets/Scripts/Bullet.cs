using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool isPlayerBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.x >= 10 || transform.position.y >= 8 ||
            transform.position.x <= -10 || transform.position.y <= -8) {
                Destroy(gameObject);
            }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)    //避免玩家互相之间误伤
                {
                    collision.SendMessage("PlayerDie");
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (isPlayerBullet)    //避免敌方坦克互相之间误伤
                {
                    collision.SendMessage("EnemyDie");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("HeartDie");
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barriar":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
