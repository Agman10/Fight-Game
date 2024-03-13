using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public TestPlayer user;

    public virtual void OnEnable()
    {
        if (this.user != null)
        {
            this.user.OnHit += OnHit;
            this.user.OnDeath += OnDeath;
            this.user.OnReset += OnReset;
        }
    }

    public virtual void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.user != null)
        {
            this.user.OnHit -= OnHit;
            this.user.OnDeath -= OnDeath;
            this.user.OnReset -= OnReset;
        }

    }
    public virtual void OnHit()
    {

    }
    public virtual void OnDeath()
    {
        this.Stop();
    }
    public virtual void OnReset()
    {
        this.Stop();
    }

    public virtual void Initiate()
    {

    }

    public virtual void Stop()
    {
        this.StopAllCoroutines();
    }
}
