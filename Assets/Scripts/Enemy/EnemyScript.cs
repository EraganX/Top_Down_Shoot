using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private float _fireRate = 1f;
    private float _lastShotTime;
    private PlayerScript _script;
    private Score _score;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>(); // Add null check with "?"
        _script = player?.GetComponent<PlayerScript>();
        _score = FindAnyObjectByType<Score>();
        _lastShotTime = Time.time;
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, _moveSpeed * Time.deltaTime);
            Shoot();

            //learn more
            //************************************************************
            float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            //************************************************************
        }
    }

    private void Shoot()
    {
        if (player != null && Time.time > _lastShotTime + _fireRate)
        {
            Instantiate(_bullet, transform.position, Quaternion.identity);
            _lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Collide");
         
            _script?.HealthUpdate(-1); 
            _score.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
