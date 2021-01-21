using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    private float PlayerTank_AttackFreq = 0;

    public override void Move()
    {
        float H_move = Input.GetAxisRaw("Horizontal");
        float V_move = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.right * H_move * MoveSpeed * Time.deltaTime, Space.World);
        //MoveByMultiSprite(H_move,0);
        MoveByRoate(H_move,0);
        if (H_move != 0) return;
        transform.Translate(Vector3.up * V_move * MoveSpeed * Time.deltaTime, Space.World);
        //MoveByMultiSprite(0,V_move);
        MoveByRoate(0,V_move);
    }

    public override bool AttackControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerTank_AttackFreq <= 0)
        {
            PlayerTank_AttackFreq = AttackFreq;
            return true;
        }
        return false;
    }
    public override void TimeRecord()
    {
        if (PlayerTank_AttackFreq >= 0) PlayerTank_AttackFreq -= Time.deltaTime;
    }

}