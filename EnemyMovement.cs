using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int life = 1;

    [SerializeField]
    int pointsToAdd;

    public EnemiesManager enemiesManager;
    public GameManager gameManager;
    public PlayerMovement playerMovement;

    [SerializeField]
    AudioClip shield;
    [SerializeField]
    AudioClip destroy;

    public int dir = 1;

    public bool bateuParede = false;

    AudioSource audioSource;

    public void Setup(GameManager gameManager, EnemiesManager enemiesManager, PlayerMovement playerMovement)
    {
        this.enemiesManager = enemiesManager;
        this.gameManager = gameManager;
        this.playerMovement = playerMovement;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("Movement");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Parede" && dir == enemiesManager.dir)
        {
            enemiesManager.BateuParede();
        }
    }

    public void Damage(int damage, Bullet bullet)
    {
        life -= damage;
        
        if (life < 1)
        {
            gameManager.AddPoints(pointsToAdd);
            //verificar se foi o último inimigo a ser retirado
            enemiesManager.CheckGameOver(gameObject);
            audioSource.PlayOneShot(destroy, 1);
            Destroy(gameObject);
        }
        else
        {
            audioSource.PlayOneShot(shield, 1);
        }

        WhatToDoWithBullet(bullet);
    }

    public virtual void WhatToDoWithBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    protected virtual IEnumerator Movement()
    {
        while (transform.position.y > -4)
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

        Debug.Log("saiu do loop, está fora da tela");

        playerMovement.Damage(this.gameObject);
    }
}
