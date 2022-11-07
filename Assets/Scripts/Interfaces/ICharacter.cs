using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    float Health { get; set; }
    void TakeDamage(float dmg);

    void Die();
}