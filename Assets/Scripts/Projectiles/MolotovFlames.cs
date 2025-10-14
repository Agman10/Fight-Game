using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFlames : MonoBehaviour
{
    public TestPlayer owner;
    public ParticleSystem flameEffect;
    public TestHitbox hitbox;
    public AudioSource fireSfx;

    private float startVolume;

    private void Start()
    {
        if (this.fireSfx != null)
        {
            this.startVolume = this.fireSfx.volume;
        }
    }
    private void OnEnable()
    {
        this.StartCoroutine(this.DespawnCoroutine());
        if (this.hitbox != null)
        {
            this.hitbox.gameObject.SetActive(true);
            this.hitbox.damage = 1f;
        }

        if (this.fireSfx != null)
        {
            this.fireSfx.Play();
            //this.startVolume = this.fireSfx.volume;
        }
            
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.fireSfx != null)
        {
            this.fireSfx.volume = this.startVolume;
        }
    }

    private IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(5f);
        if (this.flameEffect != null)
            this.flameEffect.Stop();

        /*if (this.fireSfx != null)
            this.fireSfx.Stop();*/

        this.StartCoroutine(this.FadeOutFireSfxCoroutine());

        /*yield return new WaitForSeconds(0.5f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/


        yield return this.WeakenFlameHitboxCoroutine();

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public virtual void SetOwner(TestPlayer user)
    {
        if (user != null)
        {
            this.owner = user;

            if (this.hitbox != null)
                this.hitbox.belongsTo = user;
        }
    }


    private IEnumerator WeakenFlameHitboxCoroutine()
    {
        if (this.hitbox != null)
        {
            yield return new WaitForSeconds(0.1f);
            this.hitbox.damage = 0.8f;
            yield return new WaitForSeconds(0.1f);
            this.hitbox.damage = 0.6f;
            yield return new WaitForSeconds(0.1f);
            this.hitbox.damage = 0.4f;
            yield return new WaitForSeconds(0.1f);
            this.hitbox.damage = 0.2f;
            yield return new WaitForSeconds(0.1f);
            this.hitbox.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    private IEnumerator FadeOutFireSfxCoroutine()
    {
        

        if (this.fireSfx != null)
        {
            float currentTime = 0;
            float duration = 0.25f;
            //float startVolmue = this.fireSfx.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                //this.fireSfx.volume = Mathf.Lerp(startVolmue, 0f, currentTime / duration);
                this.fireSfx.volume = Mathf.Lerp(this.startVolume, 0f, currentTime / duration);
                yield return null;
            }
            this.fireSfx.Stop();
            this.fireSfx.volume = this.startVolume;
            //this.fireSfx.volume = startVolmue;
        }

    }
}
