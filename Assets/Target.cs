using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private Renderer targetRenderer;
    private Color originalColor;

    public float minMoveSpeed = 0.5f;
    public float maxMoveSpeed = 2f; 
    public float minMoveRange = 1f; 
    public float maxMoveRange = 5f; 
    private float moveSpeed; 
    private float moveRange; 
    private Vector3 startPosition;

    // Respawn variables
    public float respawnTime = 5f;

    void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;
        SetRandomMovementValues();
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
            Die();
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

    void Die()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        health = 50f;
        SetRandomMovementValues();
        transform.position = startPosition;
        gameObject.SetActive(true);
        targetRenderer.material.color = originalColor;
    }

    void SetRandomMovementValues()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        moveRange = Random.Range(minMoveRange, maxMoveRange);
        startPosition = transform.position;
    }
}
