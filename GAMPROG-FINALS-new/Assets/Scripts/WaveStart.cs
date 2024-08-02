using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveStart : MonoBehaviour
{
    [SerializeField] private Material[] numberMaterials;

    private Renderer cubeRenderer;
    private Timer timer; 

    private void Awake()
    {
        cubeRenderer = GetComponent<Renderer>();
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        ChangeNumber();
    }

    private void ChangeNumber()
    {
        int waveNumber = timer.waveNumber;

        if (waveNumber > 0 && waveNumber <= numberMaterials.Length)
        {
            cubeRenderer.material = numberMaterials[waveNumber - 1];
        }
    }
}
