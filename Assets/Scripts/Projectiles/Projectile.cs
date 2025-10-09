using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TestPlayer owner;
    public bool ignoreOtherProjectiles = false;
    public int priority;

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {
        this.StopAllCoroutines();

    }

    public virtual void SetOwner(TestPlayer user)
    {
        if(user != null)
        {
            this.owner = user;
        }
    }
}
