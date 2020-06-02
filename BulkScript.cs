using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulkBehaviour
{
    NORMAL,
    TRIGERRED
}

public class BulkScript : EnemyMovement
{
    public BulkBehaviour behaviour;

    public override void WhatToDoWithBullet(Bullet bullet)
    {
        bullet.velBullet = -bullet.velBullet / 2;
        bullet.behaviour = BulletBehaviour.KILLER;

        if (life < 3)
        {
            behaviour = BulkBehaviour.TRIGERRED;
        }
    }


    protected override IEnumerator Movement()
    { 
        while (transform.position.y > -4)
        {
            if(behaviour == BulkBehaviour.NORMAL)
            {
                yield return new WaitForSeconds(0.7f);
                if (enemiesManager.bateuParede != bateuParede)
                {
                    transform.position += new Vector3(0, -1.5f, 0);
                    bateuParede = enemiesManager.bateuParede;
                }
                else
                {
                    transform.position += enemiesManager.mov;
                }
            }
            else if (behaviour == BulkBehaviour.TRIGERRED)
            {
                yield return new WaitForSeconds(0.7f);
                transform.position += new Vector3(0, -1.5f, 0);
                bateuParede = enemiesManager.bateuParede;
            }

        }

        Debug.Log("saiu do loop, está fora da tela");

        playerMovement.Damage(this.gameObject);
    }
}

