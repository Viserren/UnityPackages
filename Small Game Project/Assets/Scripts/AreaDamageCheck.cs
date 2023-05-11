using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageCheck : MonoBehaviour
{
    private float _radius = 0.2f;
    public LayerMask lights;
    public float damageInterval = 1f;
    public float healInterval = 1f;
    private float _timeTillNextCheck;

    private void Start()
    {
        _timeTillNextCheck = damageInterval;
    }

    private void Update()
    {
        Collider[] overlapingLights = Physics.OverlapSphere(transform.position, _radius, lights);

        if (overlapingLights.Length <= 0)
        {
            if (gameObject.TryGetComponent<Health>(out Health health))
            {
                if (health.currentHealth != 0)
                {
                    _timeTillNextCheck -= damageInterval * Time.deltaTime;
                    if (_timeTillNextCheck <= 0)
                    {
                        _timeTillNextCheck = damageInterval;
                        health.DecreaseHealth(0.5f);
                    }
                }
            }
        }
        else
        {
            if (gameObject.TryGetComponent<Health>(out Health health))
            {
                if (health.currentHealth != health.maxhealth)
                {
                    _timeTillNextCheck -= healInterval * Time.deltaTime;
                    if (_timeTillNextCheck <= 0)
                    {
                        _timeTillNextCheck = healInterval;
                        health.IncreaseHealth(0.5f);
                    }
                }
            }

        }

    }
}
