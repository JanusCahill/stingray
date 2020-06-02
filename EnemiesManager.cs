using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerMovement playerMovement;

    public int dir = 1;
    public bool bateuParede;
    public Vector3 mov = new Vector3(1, 0, 0);
    public List<GameObject> enemies;
    public List<EnemyMovement> movementEnemies;

    public void Setup()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.Setup(this, gameManager);

        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        

        for (int i = 0; i < enemies.Count; i++)
        {
            movementEnemies.Add(enemies[i].GetComponent<EnemyMovement>());
            movementEnemies[i].dir = this.dir;
            movementEnemies[i].Setup(gameManager, this, playerMovement);
        }
    }

    public void BateuParede()
    {
        bateuParede = !bateuParede;
        dir = -dir;
        mov.x = -mov.x;
        foreach(EnemyMovement enemy in movementEnemies)
        {
            enemy.dir = this.dir;
        }
    }

    //Quero tirar o inimigo da minha lista de inimigos ativos
    //Depois verifico se ele foi o último, e portanto, o jogo acabou
    public void CheckGameOver(GameObject hitEnemy)
    {

        enemies.Remove(hitEnemy);
        movementEnemies.Remove(hitEnemy.GetComponent<EnemyMovement>());

        if(enemies.Count == 0)
        {
            gameManager.GameOver(enemies.Count);
            Debug.Log("Enemies Manager ativou fim do jogo");
        }
    }

}
