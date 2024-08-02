using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] AudioSource SFXSource;

    public AudioClip bgMusic;

    public AudioClip Button;
    public AudioClip PlayerHit;
    public AudioClip EnemyHit;
    public AudioClip Shoot;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        musicSource.clip = bgMusic;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }
  
}
