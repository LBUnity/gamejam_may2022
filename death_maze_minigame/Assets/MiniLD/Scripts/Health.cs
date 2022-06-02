using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    MainLevelManager levelManager;

    public int GetHealth()
    {
        return health;
    }

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<MainLevelManager>();
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        { 
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.AddToScore(score); 
        }
        else
        {
            
            //Stop the audio source
            audioPlayer.gameObject.GetComponent<AudioSource>().Stop();
            levelManager.LoadMainTilevaniaLevel1();//Return to tilevania level 1 for now. 

        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            //take damage
            TakeDamage(damageDealer.GetDamage());
            ShakeCamera();
            PlayHitEffect();
            if (audioPlayer != null)
            {
                audioPlayer.PlayDamageClip();
            }

            //tell damage dealer it hit something and destroy itself
            damageDealer.Hit();
        }
    }

    private void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

}
