using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public TestPlayer user;
    public StatusEffect poison;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (this.user != null)
        {
            //this.user.OnHit += OnHit;
            this.user.OnDeath += this.OnDeath;
            this.user.OnReset += this.OnReset;
        }
    }

    private void OnDisable()
    {
        if (this.user != null)
        {
            //this.user.OnHit += OnHit;
            this.user.OnDeath -= this.OnDeath;
            this.user.OnReset -= this.OnReset;
        }
    }

    public void OnDeath()
    {
        this.RemoveAllEffects();
        //Debug.Log("dead");
    }

    public void OnReset()
    {
        this.RemoveAllEffects();
    }

    public void RemoveAllEffects()
    {
        if (this.poison != null)
        {
            this.poison.RemoveStatusEffect();
        }
    }

    
    public void GivePoison(float time = 5f)
    {
        if (this.poison != null)
            this.poison.GiveStatusEffect(time);
    }

    [ContextMenu("TestPoison")]
    public void TestGivePoison()
    {
        float time = 5f;
        if (this.poison != null)
            this.poison.GiveStatusEffect(time);
    }
}
