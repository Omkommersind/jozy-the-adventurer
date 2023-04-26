using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private Rigidbody2D _hitbox;

    void Start()
    {
        _hitbox = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var healthController = col.gameObject.GetComponent<HealthController>();
        if (healthController != null) {
            healthController.TakeDamage(10, transform.position);  // Todo
        }
    }
}
