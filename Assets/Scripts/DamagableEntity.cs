using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : MonoBehaviour
{
    public float health = 10f;
    public float maxHealth = 10f;
    public bool dead = false;

    void Update()
    {
        
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void TakeDamage(Vector3 position, float amount = 1f, float stun = 0f, float horizontalKnockback = 0f, float verticalKnockback = 0f, bool changeDir = false)
    {
        this.health -= amount;

        if (this.health < 0f)
            this.health = 0f;

        if (amount < 0)
        {
            if (this.health >= this.maxHealth)
                this.health = this.maxHealth;
        }

        if (this.health <= 0f)
        {
            this.Die(position);
        }

        
    }

    public virtual void Die(Vector3 position)
    {
        if (!this.dead)
        {
            this.dead = true;
            this.health = 0f;
        }
        
    }
}
