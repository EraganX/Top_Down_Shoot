using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Vector2 _target;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject _PlayerDestroyFX, _EnemyDestroyFX;
    private PlayerScript _PlayerScript;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _target = playerObject.transform.position;
            _PlayerScript = playerObject.GetComponent<PlayerScript>();
        }
    }

    private void Update()
    {
        if (_PlayerScript != null && _target != null && !_PlayerScript.isDead)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _target) < 0.1f)
            {
                Instantiate(_EnemyDestroyFX, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _PlayerScript != null && !_PlayerScript.isDead)
        {
            _PlayerScript.HealthUpdate(-1);
            Instantiate(_PlayerDestroyFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
