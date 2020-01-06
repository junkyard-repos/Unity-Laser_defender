using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float padding = 1f;
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float bulletSpeed = 10f;
  [SerializeField] float projectileFiringPeriod = 0.1f;

  Coroutine fireCoroutine;

  float xMin;
  float xMax;
  float yMin;
  float yMax;
  float deltaX;
  float deltaY;
  float newXPos;
  float newYPos;

  void Start()
  {
    SetupMoveBoundaries();
    newXPos = 0;
    newYPos = yMin;
  }

  void Update()
  {
    // Move();
    UpdatePlayerPos();
    Fire();
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void UpdatePlayerPos()
  {
    deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
  }

  private void SetupMoveBoundaries()
  {
    Camera gameCamera = Camera.main;

    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  }

  private void Move()
  {
    transform.position = new Vector2(newXPos, newYPos);
  }

  private void Fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      fireCoroutine = StartCoroutine(FireContinuosly());
    }

    if (Input.GetButtonUp("Fire1"))
    {
      StopCoroutine(fireCoroutine);
    }
  }

  private IEnumerator FireContinuosly()
  {
    while (true)
    {
      GameObject laser = Instantiate(
        laserPrefab,
        transform.position,
        Quaternion.identity) as GameObject;
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

      yield return StartCoroutine(FireCoroutine());
    }

  }

  private IEnumerator FireCoroutine()
  {
    yield return new WaitForSeconds(projectileFiringPeriod);
  }
}
