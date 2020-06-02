using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletBehaviour
{
    NORMAL,
    KILLER
}

public class Bullet : MonoBehaviour
{
    public BulletBehaviour behaviour;

    public float speed = 1;
    public EnemiesManager enemiesManager;
    public GameManager gameManager;
    public GameObject particles;
    GameObject instanceParticles;
    public Vector3 velBullet;

    void Start()
    {
        enemiesManager = GameObject.FindObjectOfType<EnemiesManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
        transform.position += new Vector3(0,0, 0.254f);
        velBullet = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position += velBullet;
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyMovement>().Damage(1, this);
            instanceParticles = Instantiate(particles, other.gameObject.transform.position, Quaternion.identity);
            Destroy(instanceParticles, 2);
        }
        else if (other.gameObject.CompareTag("Destroyable"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(behaviour == BulletBehaviour.KILLER && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Damage(null);
            Destroy(gameObject);
        }
    }
}
