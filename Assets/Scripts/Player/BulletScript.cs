using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Vector2 _target;
    [SerializeField] private float _speed = 15.0f;
    [SerializeField] GameObject _PlayerDestroyFX, _EnemyDestroyFX;
    Score scoreScript;
    Shake _shake;

    private void Start()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        scoreScript = FindObjectOfType<Score>();
        _shake = FindAnyObjectByType<Shake>();
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,_target, _speed *Time.deltaTime);

        if (Vector2.Distance(transform.position, _target) < 0.1f)
        {
            Instantiate(_PlayerDestroyFX, transform.position, Quaternion.identity);
            _shake.MissFire();
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (scoreScript != null)
            {
                scoreScript.AddScore(1);
            }
            _shake.CameraShake();
            Instantiate(_EnemyDestroyFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

        }
    }
}
