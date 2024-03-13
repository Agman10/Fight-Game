using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fire : MonoBehaviour
{
    public TestPlayer belongsTo;
    public TestHitbox hitbox;
    public VisualEffect flame;
    public float lifeTime = 1.5f;
    void OnEnable()
    {
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.StartCoroutine(this.DisableTimerCoroutine());
    }
    void OnDisable()
    {
        this.StopAllCoroutines();
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

    IEnumerator DisableTimerCoroutine()
    {
        yield return new WaitForSeconds(this.lifeTime);
        
        if (this.flame != null)
            this.flame.Stop();


        yield return new WaitForSeconds(0.25f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
        /*if (!this.destroy)
            this.gameObject.SetActive(false);
        else
            Destroy(this.gameObject);*/
    }
}
