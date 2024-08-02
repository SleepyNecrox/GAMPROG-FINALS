using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float minMoveSpeed = 0.5f;
    [SerializeField] private float maxMoveSpeed = 2f; 
    [SerializeField] private float minMoveRange = 1f; 
    [SerializeField] private float maxMoveRange = 5f; 
    [SerializeField] private float respawnTime = 5f;
    private float moveSpeed; 
    private float moveRange; 
    private Vector3 startPosition;

    private Renderer targetRenderer;
    private Color originalColor;

    void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;
        RandomMovement();
    }

    void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        float newY = startPosition.y + Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        
        transform.position = new Vector3(newX, newY, startPosition.z);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.EnemyHit);
            gameObject.SetActive(false);
            Invoke("Respawn", respawnTime);
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

    void Respawn()
    {
        health = 50f;
        RandomMovement();
        transform.position = startPosition;
        gameObject.SetActive(true);
        targetRenderer.material.color = originalColor;
    }

    void RandomMovement()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        moveRange = Random.Range(minMoveRange, maxMoveRange);
        startPosition = transform.position;
    }
}
