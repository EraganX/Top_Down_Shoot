using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EnemyScript : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject _bullet,_destroyFX;
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private AudioClip _shootclip;
    [SerializeField] private AudioSource source;
    private float _fireRate = 3f;
    private float _lastShotTime;
    private PlayerScript _script;
    private Score _score;
    private Shake _shake;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>(); // Add null check with "?"
        _script = player?.GetComponent<PlayerScript>();
        _score = FindAnyObjectByType<Score>();
        _shake = FindAnyObjectByType<Shake>();
        _lastShotTime = Time.time;
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, _moveSpeed * Time.deltaTime);
            Shoot();
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
            source.PlayOneShot(_shootclip);
            Instantiate(_bullet, transform.position, Quaternion.identity);
            _lastShotTime = Time.time;
            _fireRate = Random.Range(1,5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _script?.HealthUpdate(-1);
            _shake.CameraShake();
            _score.AddScore(1);
            Instantiate(_destroyFX,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
