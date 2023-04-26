using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float InitialHealth = 100f;
    public float ImpulseOnDamage = 20f;

    private Rigidbody2D _rigidbody;
    private float _health;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = InitialHealth;
    }

    void Update()
    {
        
    }

    // Todo: extract
    public void TakeDamage(int amount)
    {
        _health -= amount;
    }

    public void TakeDamage(int amount, Vector3 fromPos)
    {
        Debug.Log("From: " + fromPos.ToString());
        _health -= amount;
        if (fromPos != null && ImpulseOnDamage > 0.0f)
        {
            var direction = fromPos - transform.position;
            Debug.Log("Direction: " + direction.ToString());
            direction.z = 0.0f;
            if (direction.y < 0.0f)
            {
                direction.y = 0.0f;
            }
            direction.Normalize();
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(direction * -1 * ImpulseOnDamage, ForceMode2D.Impulse);
        }
        Debug.Log("Health: " + _health.ToString());
    }
}
