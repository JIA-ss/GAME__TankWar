using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Tank
{
    public float MoveFreq = 1;
    private float Enemy_AttackFreq = 0;
    private float Enmey_MoveFreq = 0;
    private float Enemy_H_move = 0;
    private float Enemy_V_move = 0;
    public override void Move()
    {
        if (Enmey_MoveFreq <= 0) 
        {
            Enmey_MoveFreq = MoveFreq;
            Enemy_H_move = Random.Range(-1,3);
            if (Enemy_H_move == 2) Enemy_H_move = 0;
            Enemy_V_move = Random.Range(-1,3);
            if (Enemy_V_move == 2) Enemy_V_move = 0;
        }
        transform.Translate(Vector3.right * Enemy_H_move * MoveSpeed * Time.deltaTime, Space.World);
        //MoveByMultiSprite(H_move,0);
        MoveByRoate(Enemy_H_move,0);
        if (Enemy_H_move != 0) return;
        transform.Translate(Vector3.up * Enemy_V_move * MoveSpeed * Time.deltaTime, Space.World);
        //MoveByMultiSprite(0,V_move);
        MoveByRoate(0,Enemy_V_move);
    }

    public override bool AttackControl()
    {
        if (Enemy_AttackFreq <= 0)
        {
            Enemy_AttackFreq = AttackFreq;
            return true;
        }
        return false;
    }
    public override void TimeRecord()
    {
        Enemy_AttackFreq -= Time.deltaTime;
        Enmey_MoveFreq -= Time.deltaTime;
    }

}