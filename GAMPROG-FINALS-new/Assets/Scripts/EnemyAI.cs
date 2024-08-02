using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    private Transform player;
    private Rigidbody rb;
    private Renderer targetRenderer;
    private Color originalColor;

    private EnemySpawner spawner;

    private PlayerMovement playerMovement;

    private Timer timer;

    public float Gold;


    private void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;
        spawner = FindObjectOfType<EnemySpawner>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        FollowPlayer();


        if(timer.currentTime <= 0)
        {
            Die();
        }


    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }

   internal void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.EnemyHit);
            spawner.EnemyDied();
            playerMovement.AddGold();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = Color.red;
        }
        
        yield return new WaitForSeconds(0.3f);

        if (targetRenderer != null)
        {
            targetRenderer.material.color = originalColor; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

     public void SetStats(int waveNumber)
    {
        health = 30 + (waveNumber * 10);
        damage = 20 + (waveNumber * 2);
        moveSpeed = 2 + (waveNumber * 0.5f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}


