using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [Header("Enemy Stats")]
  [SerializeField] int health = 100;
  [SerializeField] int scoreValue = 150;

  [Header("Shooting")]
  [SerializeField] float shotCounter;
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] float bulletSpeed = 5f;

  [Header("Sound Effects")]
  [SerializeField] GameObject laserPrefab;
  [SerializeField] GameObject explosionPrefab;
  [SerializeField] AudioClip explosionSound;
  [SerializeField] [Range(0, 1)] float explosionSoundVolume = 0.5f;
  [SerializeField] AudioClip shootSound;
  [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.5f;


  private void Start()
  {
    shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
  }

  private void Update()
  {
    CountDownAndShoot();
  }

  private void CountDownAndShoot()
  {
    shotCounter -= Time.deltaTime;

    if (shotCounter <= 0)
    {
      Fire();
      shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
  }

  private void Fire()
  {
    GameObject laser = Instantiate(
        laserPrefab,
        transform.position,
        Quaternion.identity) as GameObject;
    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (health <= 0)
    {
      Destroy(this.gameObject);

      DeathSequence();
    }
  }

  private void DeathSequence()
  {
    GameObject explosion = Instantiate(
      explosionPrefab,
      transform.position,
      Quaternion.identity) as GameObject;

    FindObjectOfType<GameSession>().AddToScore(scoreValue);
    Destroy(explosion, 0.2f);
    AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (!damageDealer)
    {
      return;
    }
    ProcessHit(damageDealer);
  }
}
