using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloud : MonoBehaviour
{
    /*public float lightinigTime;
    public float lightinigDuration;*/

    public GameObject lightning;

    public TestHitbox hitbox;
    public TestPlayer belongsTo;
    private void OnEnable()
    {
        this.StartCoroutine(this.LightningCloudCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.lightning != null)
        this.lightning.SetActive(false);
    }

    private IEnumerator LightningCloudCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        if (this.lightning != null)
            this.lightning.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        if (this.lightning != null)
            this.lightning.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
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
