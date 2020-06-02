using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInstantiate : MonoBehaviour
{
    GameManager gameManager;
    EnemiesManager enemiesManager;

    int lines = 5;
    int columns = 11;

    [SerializeField]
    float horizontalSpace = 1.2f;
    [SerializeField]
    float verticalSpace = 1.5f;

    [SerializeField]
    GameObject[] typeEnemies;

    int[,,] defineType =
    {
        {
            {0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1},
            {2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2}
        },
        {
            {0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1},
            {2,2,2,2,2,2,2,2,2,2,2},
            {2,3,2,3,2,3,2,3,2,3,2}
        },
        {
            {3,1,2,1,3,1,2,1,3,1,2},
            {0,1,0,1,0,1,0,1,0,1,0},
            {2,1,3,1,2,1,3,1,2,1,3},
            {0,1,0,1,0,1,0,1,0,1,0},
            {3,1,2,1,3,1,2,1,3,1,2},
        }
    };




    public List<List<GameObject>> enemies = new List<List<GameObject>>();

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        enemiesManager = GameObject.FindObjectOfType<EnemiesManager>();

        for (int i = 0; i < lines; i++)
        {
            enemies.Add(new List<GameObject>());
            for (int j = 0; j < columns; j++)
            {
                enemies[i].Add(typeEnemies[defineType[gameManager.stage, i, j]]);
                Instantiate(enemies[i][j], transform.position + new Vector3(j * horizontalSpace, i * -verticalSpace, 0), Quaternion.identity, gameObject.transform);
            }
        }

        enemiesManager.Setup();
    }

    public bool TemMaisFases(int stage)
    {
        if(stage < defineType.GetLength(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
