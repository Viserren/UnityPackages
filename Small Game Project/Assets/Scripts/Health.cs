using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; }
    public float maxhealth { get; private set; }
    [SerializeField] private float _maxhealth;

    private void Start()
    {
        maxhealth = _maxhealth;
        currentHealth = maxhealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Die");
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxhealth)
        {
            currentHealth = maxhealth;
        }
    }
}
