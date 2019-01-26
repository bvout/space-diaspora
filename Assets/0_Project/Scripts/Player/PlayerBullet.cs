﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BulletMovement2D))]
public class PlayerBullet : MonoBehaviour
{
    private BulletMovement2D _movement;
    private CircleCollider2D _collider;
    [SerializeField] private float _lifespan = 3.0f;
    public EventHandler Inert;

    [SerializeField] private float _speed = 200;
    [SerializeField] private int _damage = 10;
    
    public void Initialize()
    {
        _movement = GetComponent<BulletMovement2D>();
        _movement.Initialize();
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Fire(Vector2 dir, Vector3 pos)
    {
        _movement.Stop();
        transform.position = pos;
        _movement.ApplyForce(dir * _speed);
        StartCoroutine(Lifespan());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile")) return;

//        var playerHealth = GetComponent<PlayerHealth>();
//        playerHealth.Damage(_damage);
        DestroyBullet();
    }

    private IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(_lifespan);
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        Inert?.Invoke(this, EventArgs.Empty);
        _movement.Stop();
        gameObject.SetActive(false);
    }
}