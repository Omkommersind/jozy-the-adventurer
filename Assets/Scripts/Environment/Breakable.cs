using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Rigidbody2D))]
public class Breakable : MonoBehaviour
{
    public float MagnitudeThreshold = 15f;

    private ParticleSystem _particles;
    private Rigidbody2D _body;
    private float _prevMagnitude = 0f;

    void Start()
    {
        _particles = GetComponent<ParticleSystem>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var gForce = Mathf.Abs(_body.velocity.magnitude - _prevMagnitude);
        if (gForce > MagnitudeThreshold)
        {
            _particles.Play();
            Destroy(gameObject, _particles.main.duration);
        }
        _prevMagnitude = _body.velocity.magnitude;
        Debug.Log(gForce);
    }
}
