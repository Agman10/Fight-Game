using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float explosionDuration = 0.1f;
    public TestHitbox explosionHitbox;
    public TestHitbox explosionHitbox2;
    public TestPlayer belongsTo;

    public CharacterSoundEffect explosionSfx;
    public bool preventExplosionSound = false;

    [Space]
    public float explosionSize = 1f;
    public Transform sizeTransform;
    public bool damageOwner = true;
    public bool explosionKnockback = true;

    [Space]
    public float innerDamage = 15f;
    public float innerKnockbackX = 900f;
    public float innerKnockbackY = 900f;
    public float innerSuperCharge = 3.75f;
    public float innerStun = 0.2f;
    public bool changePlayerDir = false;
    [Space]
    public float outerDamage = 5f;
    public float outerKnockbackX = 300f;
    public float outerKnockbackY = 300f;
    public float outerSuperCharge = 1.25f;
    public float outerStun = 0.2f;
    private void OnEnable()
    {
        this.SetDamage(this.innerDamage, this.outerDamage, this.innerKnockbackX, this.innerKnockbackY, this.outerKnockbackX, this.outerKnockbackY, this.innerSuperCharge, this.outerSuperCharge, this.innerStun, this.outerStun, this.changePlayerDir, this.damageOwner, this.explosionKnockback);
        this.SetSize(this.explosionSize);
        this.SetOwner(this.belongsTo);

        this.StartCoroutine(this.ExplosionDuration());
        /*if (!this.preventExplosionSound)
            this.explosionSfx.PlaySound();*/
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.explosionHitbox != null)
            this.explosionHitbox.gameObject.SetActive(true);
    }

    IEnumerator ExplosionDuration()
    {
        yield return new WaitForSeconds(this.explosionDuration);
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

    public void SetDamage(float damageInner = 15f, float damageOuter = 5f, float knockbackInnerX = 900f, float knockbackInnerY = 900f, float knockbackOuterX = 300f, float knockbackOuterY = 300f, float superChargeInner = 3.75f, float superChargeOuter = 1.25f, float stunInner = 0.2f, float stunOuter = 0.2f, bool playerDirChange = false, bool damageSelf = true, bool explosionKnockbackk = true)
    {
        if (this.explosionHitbox != null)
        {
            this.explosionHitbox.damage = damageInner;
            this.explosionHitbox.horizontalKnockback = knockbackInnerX;
            this.explosionHitbox.verticalKnockback = knockbackInnerY;
            this.explosionHitbox.superChargeAmount = superChargeInner;
            this.explosionHitbox.stun = stunInner;

            this.explosionHitbox.changeTargetDir = playerDirChange;

            this.innerDamage = damageInner;
            this.innerKnockbackX = knockbackInnerX;
            this.innerKnockbackY = knockbackInnerY;
            this.innerSuperCharge = superChargeInner;
            this.innerStun = stunInner;

            this.changePlayerDir = playerDirChange;

            this.explosionHitbox.damageOwner = damageSelf;

            this.explosionHitbox.explosionKnockback = explosionKnockbackk;
        }

        if (this.explosionHitbox2 != null)
        {
            this.explosionHitbox2.damage = damageOuter;
            this.explosionHitbox2.horizontalKnockback = knockbackOuterX;
            this.explosionHitbox2.verticalKnockback = knockbackOuterY;
            this.explosionHitbox2.superChargeAmount = superChargeOuter;
            this.explosionHitbox2.stun = stunOuter;

            this.outerDamage = damageOuter;
            this.outerKnockbackX = knockbackOuterX;
            this.outerKnockbackY = knockbackOuterY;
            this.outerSuperCharge = superChargeOuter;
            this.outerStun = stunOuter;

            this.explosionHitbox2.damageOwner = damageSelf;

            this.explosionHitbox2.explosionKnockback = explosionKnockbackk;
        }
    }

    public void SetSize(float size = 1)
    {
        if (this.sizeTransform != null)
        {
            this.sizeTransform.localScale = Vector3.one * size;
            this.explosionSize = size;
        }
    }
}
