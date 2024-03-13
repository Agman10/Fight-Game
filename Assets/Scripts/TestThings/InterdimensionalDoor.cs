using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterdimensionalDoor : MonoBehaviour
{
    public Transform doorLid;

    public TestPlayer belongsTo;

    public TestHitbox hitbox;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

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

    public void ActivateHitbox()
    {
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.StartCoroutine(this.DisableHitboxCoroutine());
    }

    private IEnumerator DisableHitboxCoroutine()
    {

        yield return new WaitForSeconds(0.05f);

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }
}
