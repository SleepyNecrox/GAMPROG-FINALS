using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private Renderer targetRenderer;
    private Color originalColor;

    // Movement variables
    public float minMoveSpeed = 0.5f;  // Minimum speed of movement
    public float maxMoveSpeed = 2f;    // Maximum speed of movement
    public float minMoveRange = 1f;    // Minimum range of movement
    public float maxMoveRange = 5f;    // Maximum range of movement
    private float moveSpeed;           // Speed of movement
    private float moveRange;           // Range of movement
    private Vector3 startPosition;     // Starting position of the target

    // Respawn variables
    public float respawnTime = 5f;     // Time to respawn

    void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
        originalColor = targetRenderer.material.color;  // Initialize the original color
        SetRandomMovementValues();  // Set initial random movement values
    }

    void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        // Move the target up and down
        float newY = startPosition.y + Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        
        // Move the target left and right
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveRange) - (moveRange / 2);
        
        // Update the target's position
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
        targetRenderer.material.color = originalColor;  // Reset to original color
    }

    void SetRandomMovementValues()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        moveRange = Random.Range(minMoveRange, maxMoveRange);
        startPosition = transform.position;
    }
}
