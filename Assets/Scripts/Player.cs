using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public Projectile laserPrefab;
    public int playerLifes { get; set; }
    public float speed = 2.0f;
    private bool _laserActive;
    public GameManager gameManager;
    private bool isDead = false;
    private void Start()
    {
        playerLifes = 3;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey((KeyCode.LeftArrow)))
        {
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey((KeyCode.RightArrow)))
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey((KeyCode.UpArrow)))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!_laserActive)
        {
            Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestoyed;
            _laserActive = true;
        }
    }
    private void LaserDestoyed()
    {
        _laserActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Invader")) || other.gameObject.layer.Equals(LayerMask.NameToLayer("Missile")))
        {
            playerLifes--;
            if (playerLifes == 0 && !isDead)
            {
                isDead = true;
                gameObject.SetActive(false);
                gameManager.GameOver();
            }
        }
    }
}