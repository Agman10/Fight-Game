using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    public TestHitbox explosionHitbox;
    public TestHitbox explosionHitbox2;
    public TestPlayer belongsTo;
    // Start is called before the first frame update
    private void OnEnable()
    {
        this.StartCoroutine(this.ExplosionDuration());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(true);
    }
    IEnumerator ExplosionDuration()
    {
        //yield return new WaitForSeconds(this.explosionDuration);
        yield return new WaitForSeconds(0.8f);
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.explosionHitbox != null)
                this.explosionHitbox.belongsTo = player;

            if (this.explosionHitbox2 != null)
                this.explosionHitbox2.belongsTo = player;
        }
    }
}
