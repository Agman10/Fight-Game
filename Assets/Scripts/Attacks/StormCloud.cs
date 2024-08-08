using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : MonoBehaviour
{
    /*public float lightinigTime;
    public float lightinigDuration;*/

    public GameObject model;
    public GameObject lightning;

    public TestHitbox hitbox;
    public TestPlayer belongsTo;

    public AudioSource lightningSfx;

    [Space]
    public float lightningDelay = 0.4f;
    private void OnEnable()
    {
        //this.StartCoroutine(this.LightningCloudCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.lightning != null)
        this.lightning.SetActive(false);
    }

    private IEnumerator LightningCloudCoroutine(float delay = 0.4f, float dissapearDuration = 0.2f)
    {
        //yield return new WaitForSeconds(0.4f);
        //yield return new WaitForSeconds(this.lightningDelay);
        yield return new WaitForSeconds(delay);


        if (this.lightning != null)
            this.lightning.SetActive(true);

        if (this.lightningSfx != null)
        {
            this.lightningSfx.Play();
        }

        yield return new WaitForSeconds(0.2f);

        if (this.lightning != null)
            this.lightning.SetActive(false);

        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(dissapearDuration);

        if (this.model != null)
            this.model.gameObject.SetActive(false);


        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void DoLightning(float delay = 0.4f, float dissapearDuration = 0.2f)
    {
        this.StartCoroutine(this.LightningCloudCoroutine(delay, dissapearDuration));
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.hitbox != null)
                this.hitbox.belongsTo = player;
        }
    }
}
