using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    //属性值: 移动速度、攻击频率、无敌时间
    public float MoveSpeed = 3;
    public float AttackFreq = 0.4f;
    public float InvincibleTime = 2;

    //引用: 坦克的Sprite，子弹样式，出生特效，死亡特效，
    public Sprite[] tankSprite; //上右下左
    public GameObject bulletPrefab;
    public GameObject BornEffectPrefab;
    public GameObject DieEffectPrefab;

    //自身属性
    public SpriteRenderer SelfRenderer;
    void Start()
    {
        SelfRenderer = GetComponent<SpriteRenderer>();
        ShowEffect();
    }
    private void ShowEffect()
    {
        Transform born = transform.Find("Born");
        Transform shield = transform.Find("Shield");
        born.gameObject.SetActive(true);
        Invoke("OffBornEffect",0.5f);
        if(shield) {
            shield.gameObject.SetActive(true);
            Invoke("OffShieldEffect",InvincibleTime);
        }
    }
    private void OffBornEffect()
    {
        Transform born = transform.Find("Born");
        born.gameObject.SetActive(false);
    }
    private void OffShieldEffect()
    {
        Transform shield = transform.Find("Shield");
        shield.gameObject.SetActive(false);
    }
    void Update()
    {
        Attack();
        TimeRecord();
    }

    private void FixedUpdate() 
    {
        Move();
    }
    public virtual void Move() {}

    private void Die()
    {
        if (InvincibleTime > 0) return;
        Instantiate(DieEffectPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    private void Born()
    {
        Instantiate(BornEffectPrefab,transform.position,Quaternion.identity);
    }

    public virtual bool AttackControl() { return true; }

    public void Attack() 
    {
        if (AttackControl())
        {
            Instantiate(bulletPrefab,transform.position,transform.rotation);
        }
    }
    public virtual void TimeRecord() {}

    public void MoveByMultiSprite(float H_move, float V_move)
    {
        if (H_move > 0) { SelfRenderer.sprite = tankSprite[1]; return; }
        if (H_move < 0) { SelfRenderer.sprite = tankSprite[3]; return; }
        if (V_move > 0) { SelfRenderer.sprite = tankSprite[0]; return; }
        if (V_move < 0) { SelfRenderer.sprite = tankSprite[2]; return; }
    }
    public void MoveByRoate(float H_move, float V_move)
    {
        Vector3 TankAngle = new Vector3(0,0,0);
        if(H_move > 0) { TankAngle.z = -90; transform.rotation = Quaternion.Euler(TankAngle); return;} 
        if(H_move < 0) { TankAngle.z = 90; transform.rotation = Quaternion.Euler(TankAngle); return;} 
        if(V_move > 0) { TankAngle.z = 0; transform.rotation = Quaternion.Euler(TankAngle); return;} 
        if(V_move < 0) { TankAngle.z = 180; transform.rotation = Quaternion.Euler(TankAngle); return;} 
    }
    

}
   