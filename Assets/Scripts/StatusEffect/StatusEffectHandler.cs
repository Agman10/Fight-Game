using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public TestPlayer user;
    public StatusEffect poison;
    public StatusEffect regeneration;
    public StatusEffect superRegeneration;
    public StatusEffect superDrain;
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

        if (this.regeneration != null)
        {
            this.regeneration.RemoveStatusEffect();
        }

        if (this.superRegeneration != null)
        {
            this.superRegeneration.RemoveStatusEffect();
        }

        if (this.superDrain != null)
        {
            this.superDrain.RemoveStatusEffect();
        }
    }

    public void GiveStatusEffect(int effectId = 0, float time = 1)
    {
        if (effectId == 0)
            this.GivePoison(time);
        else if (effectId == 1)
            this.GiveRegeneration(time);
        else if (effectId == 2)
            this.GiveSuperRegeneration(time);
        else if (effectId == 3)
            this.GiveSuperDrain(time);
    }

    
    public void GivePoison(float time = 1f)
    {
        if (this.poison != null)
            this.poison.GiveStatusEffect(time);

        //Debug.Log(time);
    }

    public void GiveRegeneration(float time = 1f)
    {
        if (this.regeneration != null)
            this.regeneration.GiveStatusEffect(time);
    }

    public void GiveSuperRegeneration(float time = 1f)
    {
        if (this.superRegeneration != null)
            this.superRegeneration.GiveStatusEffect(time);
    }

    public void GiveSuperDrain(float time = 1f)
    {
        if (this.superDrain != null)
            this.superDrain.GiveStatusEffect(time);
    }

    [ContextMenu("TestPoison")]
    public void TestGivePoison()
    {
        float time = 5f;
        if (this.poison != null)
            this.poison.GiveStatusEffect(time);
    }
}
