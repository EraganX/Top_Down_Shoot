using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _bullet, DestroyFX;
    private int _health = 4;
    public bool isDead = false;

    private Rigidbody2D _rigidbody;
    private Vector2 _MoveVelocity;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float minX = -5, maxX = 5, minY, maxY;

    private float _lastShotTime;

    Vector3 _mousePosition;

    [SerializeField] private RawImage[] image;
    [SerializeField] private AudioClip _fireClip;
    [SerializeField] private AudioSource source;

    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastShotTime = Time.time;
        _health = 4;
        isDead = false;
    }

    private void Update()
    {
        if (_health>=0)
        {
            FaceDirection();
            MoveInput();
            Shoot();
            Boundary();
            ShowHealth();
        }
        else
        {
            image[0].enabled = false;
            isDead = true;
            Instantiate(DestroyFX,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }

    private void ShowHealth()
    {
        switch (_health)
        {
            case 4:
                image[0].enabled = true;
                image[1].enabled = true;
                image[2].enabled = true;
                image[3].enabled = true;
                image[4].enabled = true;
                break;

            case 3:
                image[0].enabled = true;
                image[1].enabled = true;
                image[2].enabled = true;
                image[3].enabled = true;
                image[4].enabled = false;
                break;
            case 2:
                image[0].enabled = true;
                image[1].enabled = true;
                image[2].enabled = true;
                image[3].enabled = false;
                image[4].enabled = false;
                break;
            case 1:
                image[0].enabled = true;
                image[1].enabled = true;
                image[2].enabled = false;
                image[3].enabled = false;
                image[4].enabled = false;
                break;
            case 0:
                image[0].enabled = true;
                image[1].enabled = false;
                image[2].enabled = false;
                image[3].enabled = false;
                image[4].enabled = false;
                break;

        }
    }

    private void Boundary()
    {
        Vector3 position = transform.position;
        if (position.x<minX || position.x>maxX || position.y<minY || position.y>minY)
        {
            position.y = Mathf.Clamp(transform.position.y, minY, maxY);
            position.x = Mathf.Clamp(transform.position.x, minX, maxX);

            transform.position = position;
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time>_lastShotTime+_fireRate)
        {
            source.PlayOneShot(_fireClip);
            Instantiate(_bullet, transform.position, Quaternion.identity);
            _lastShotTime = Time.time;
        }
    }

    private void MoveInput()
    {
        Vector2 _moveInpout = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _MoveVelocity = _moveInpout.normalized * _speed;
    }

    public void HealthUpdate(int life)
    {
        _health += life;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _MoveVelocity * Time.fixedDeltaTime);
    }

    private void FaceDirection()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //learn more
        //************************************************************
        // Calculate the difference between the object's current rotation and the rotation needed to face the mouse position.
        float angle = Mathf.Atan2(_mousePosition.y - transform.position.y, _mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        // Rotate the object towards the mouse position.
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        //************************************************************

    }
}
