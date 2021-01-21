using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    public float shootFrequency = 1;
    private Vector3 bulletAngles;
    private float timeVal = 0.4f;
    private bool isDefended;
    private float defendTimeVal = 3;
    private float enemyActionTimeVal = 0.4f;
    private float enemyShootTimeVal;
    public bool isEnemy;
    private int enemyMoveValue_h,enemyMoveValue_v;

    //引用
    public Sprite[] tankSprite; //上右下左
    private SpriteRenderer sr;
    public GameObject bullet;
    public GameObject explosion;
    public GameObject defendEffect;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        enemyShootTimeVal = shootFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy) {
            //EnemyAction();
            if (enemyShootTimeVal <= 0) {
                EnemyShootAction();
                enemyShootTimeVal = shootFrequency;
            } else {
                enemyShootTimeVal -= Time.deltaTime;
            }
        } else {
            PlayerShootAction();
        }
    }

    private void FixedUpdate() 
    {
        if (isEnemy) {
            if (enemyActionTimeVal <= 0) {
                int h  = Random.Range(-1, 2);//产生-1到1之间的随机整数
                System.Random ran_v = new System.Random();
                int v = Random.Range(-3,4);
                enemyMoveValue_h = h;
                enemyMoveValue_v = v;
                enemyActionTimeVal = 0.4f;
            } else {
                EnemyMove(enemyMoveValue_h,enemyMoveValue_v);
                enemyActionTimeVal -= Time.deltaTime;
            }
        } else {
            PlayerMove();
        }
    }
    private void PlayerMove() 
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            //向左移动
            sr.sprite = tankSprite[3];
            bulletAngles.z = 90;
        } else if (h > 0) {
            //向右移动
            sr.sprite = tankSprite[1];
            bulletAngles.z = 270;
        }
        if (h != 0) { return ;}  //避免坦克出现斜着走，这里会保证h 和 v 必有一个为0

        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) {
            //向下移动
            sr.sprite = tankSprite[2];
            bulletAngles.z = 180;
        } else if (v > 0) {
            //向上移动
            sr.sprite = tankSprite[0];
            bulletAngles.z = 0;
        }
    }
    private void EnemyMove(int h, int v) 
    {
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            //向左移动
            sr.sprite = tankSprite[3];
            bulletAngles.z = 90;
        } else if (h > 0) {
            //向右移动
            sr.sprite = tankSprite[1];
            bulletAngles.z = 270;
        }
        if (h != 0) { return ;}  //避免坦克出现斜着走，这里会保证h 和 v 必有一个为0

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) {
            //向下移动
            sr.sprite = tankSprite[2];
            bulletAngles.z = 180;
        } else if (v > 0) {
            //向上移动
            sr.sprite = tankSprite[0];
            bulletAngles.z = 0;
        }
    }
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 bulletPos = transform.position;
            float height = GetComponent<Collider2D>().bounds.size.y;
            float width = GetComponent<Collider2D>().bounds.size.x;
            timeVal = 0;

            switch(bulletAngles.z) {
                case 0: 
                    bulletPos.y += height/1.2f;
                    break;
                case 90:
                    bulletPos.x -= height/1.2f;
                    break;
                case 180:
                    bulletPos.y -= height/1.2f;
                    break;
                case 270:
                    bulletPos.x += height/1.2f;
                    break;
            }
            Instantiate(bullet,bulletPos, Quaternion.Euler(bulletAngles));
        }
    }
    private void EnemyAttack()
    {
        Vector3 bulletPos = transform.position;
        float height = GetComponent<Collider2D>().bounds.size.y;
        float width = GetComponent<Collider2D>().bounds.size.x;
        timeVal = 0;

        switch(bulletAngles.z) {
            case 0: 
                bulletPos.y += height/1.2f;
                break;
            case 90:
                bulletPos.x -= height/1.2f;
                break;
            case 180:
                bulletPos.y -= height/1.2f;
                break;
            case 270:
                bulletPos.x += height/1.2f;
                break;
        }
        Instantiate(bullet,bulletPos, Quaternion.Euler(bulletAngles));
    }
    private void PlayerDie()
    {
        if (isDefended) return;
        //产生爆炸特效
        Instantiate(explosion,transform.position,Quaternion.identity);
        //死亡
        Destroy(gameObject);
    }
    private void EnemyDie()
    {
        //产生爆炸特效
        Instantiate(explosion,transform.position,Quaternion.identity);
        //死亡
        Destroy(gameObject);
    }

    private void PlayerShootAction() 
    {
        //无敌状态
        if (isDefended)
        {
            defendEffect.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0) 
            {
                isDefended = false;
                defendEffect.SetActive(false);
            }
        }
        //攻击CD
        if (timeVal >= 0.4) 
        {
            PlayerAttack();
        }
        else 
        {
            timeVal += Time.deltaTime;
        }
    }

    private void EnemyShootAction()
    {
        EnemyAttack();
    }

}
