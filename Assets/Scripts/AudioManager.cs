using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource dashAudioSource;
    [SerializeField] private AudioSource shurikenAudioSource;
    [SerializeField] private AudioSource ekkoUtiAudioSource;
    [SerializeField] private AudioSource dieAudioSource;
    [SerializeField] private AudioSource enemyShootAudioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayDashSound() { dashAudioSource.Play(); }

    public void PlayShurikenSound() { shurikenAudioSource.Play(); }

    public void PlayEkkoSound() { ekkoUtiAudioSource.Play(); }

    public void PlayDieSound() { dieAudioSource.Play(); }
    public void PlayEnemyShootSound() { enemyShootAudioSource.Play(); }



}