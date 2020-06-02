using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movMult;
    public GameObject tiro;
    public GameManager gameManager;
    public EnemiesManager enemiesManager;
    float posX;
    int life = 2;
    Vector3 mov;
    AudioSource audioSource;
    Renderer renderer;

    [SerializeField]
    AudioClip soundTiro;
    [SerializeField]
    AudioClip soundLifeLost;

    public void Setup(EnemiesManager enemiesManager, GameManager gameManager)
    {
        this.enemiesManager = enemiesManager;
        this.gameManager = gameManager;
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        posX = Input.GetAxisRaw("Horizontal") * movMult;
        mov = new Vector3(-posX, 0, 0);
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject tiroInstantiate = Instantiate(tiro, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(soundTiro, 1);
        }
    }

    private void FixedUpdate()
    {
        transform.position += mov;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision.gameObject);
        }
    }

    public void Damage(GameObject enemy)
    {
        audioSource.PlayOneShot(soundLifeLost);

        life--;

        if (life < 0)
        {
            Debug.Log("Player Movement ativou fim do jogo");
            gameManager.GameOver(1);
            Destroy(gameObject);
            return;
        }

        if(enemy != null)
        {
            enemiesManager.CheckGameOver(enemy);
            Destroy(enemy);
        }

        gameManager.UpdateLife(life);
        StartCoroutine("Tilt");
    }

    IEnumerator Tilt()
    {
        bool temp = false;

        for (int i = 0; i < 3; i++)
        {
            renderer.enabled = temp;
            temp = !temp;
            yield return new WaitForSeconds(0.2f);
        }

        renderer.enabled = true;
    }
}
