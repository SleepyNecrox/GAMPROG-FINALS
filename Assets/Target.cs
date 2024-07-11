using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private Renderer targetRenderer;
    private Color originalColor;

    void Awake()
    {
        targetRenderer = GetComponent<Renderer>();
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
        Destroy(gameObject);
    }
}
