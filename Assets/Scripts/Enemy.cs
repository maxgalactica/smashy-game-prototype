using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    public float Health { get; set; }

    float _currentHealth;
    PlaneSlicer slicePlane;
    MeshRenderer renderer;

    public bool isSliceable;

    private void Awake()
    {
        _currentHealth = Health;
        slicePlane = gameObject.GetComponentInChildren<PlaneSlicer>();
        renderer = slicePlane.GetComponent<MeshRenderer>();
    }

    public void Die()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        if ((_currentHealth -= dmg) <= 0)
            Die();
        else _currentHealth -= dmg;

    }
}
