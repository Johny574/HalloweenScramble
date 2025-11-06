using System;
using UnityEngine;

public class Health : Singleton<Health>{
    int _hearts = 3;
    bool _canTakeDamage = true;
    float _damageTimer = 0f;
    [SerializeField] float _damageCooldown = 5f;
    public Action<int> HealthChanged;
    public Action Death;
    [SerializeField] AudioSource _hitAudio;
    
    public void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Enemy"))
            return;

        Damage(col.transform.position);
    }

    public void Damage(Vector3 origin)
    {
        _hitAudio.Play();
        if (!_canTakeDamage)
            return;

        _canTakeDamage = false;
        _hearts--;
        HealthChanged.Invoke(_hearts);

        if (_hearts <= 0)
        {
            Die();
            return;
        }

        var dif = (transform.position - origin).normalized;
        transform.position += dif * 3f;
    }

    private void Die()
    {
        Death?.Invoke();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_canTakeDamage)
            return;

        if (_damageTimer < _damageCooldown)
        {
            _damageTimer += Time.deltaTime;
        }
        else
        {
            _damageTimer = 0f;
            _canTakeDamage = true;
        }
    }

#if UNITY_INCLUDE_TESTS
    public int Hearts => _hearts;


    public void TestDamage(int amount)
    {
        _canTakeDamage = false;
        _hearts-= amount;

        if (_hearts <= 0)
        {
            Die();
            return;
        }
    }
    #endif 
}