using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRoller : MonoBehaviour
{
    public TestPlayer belongsTo;
    public TestPlayer victim;

    public GameObject model;

    public GameObject collisions;

    public TestHitbox hitbox;

    public GameObject landingEffect;

    public GameObject explosion;

    public CharacterSkinTest skin;

    public AudioSource explosionSfx;

    public bool exploded = false;

    private void OnEnable()
    {
        if (this.hitbox != null)
        {
            //this.hitbox.gameObject.SetActive(true);
            this.hitbox.OnPlayerCollision += this.HitPlayer;
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();

        if (this.hitbox != null)
        {
            this.hitbox.gameObject.SetActive(true);
            this.hitbox.OnPlayerCollision -= this.HitPlayer;
        }

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.landingEffect != null)
            this.landingEffect.SetActive(false);

        if (this.victim != null)
            this.StopRoadRoller(this.victim);

        if (this.explosion != null)
            this.explosion.SetActive(false);

        if (this.collisions != null)
            this.collisions.gameObject.SetActive(true);

        
    }

    public void HitPlayer(TestPlayer player)
    {
        if (this.victim == null && player != null && player != this.belongsTo)
        {
            if (!player.knockbackInvounrability)
            {
                player.attackStuns.Add(this.gameObject);
                player.OnHit?.Invoke();
                player.rb.isKinematic = true;
                player.knockbackInvounrability = true;
                this.victim = player;
                player.preventDeath = true;

                /*if (this.collisions != null)
                    this.collisions.gameObject.SetActive(false);*/


                if (player.animations != null)
                {
                    player.animations.SetDefaultPose();
                    //player.animations.body.localEulerAngles = new Vector3(0f, yRot, 0f);
                }


                if (this.hitbox != null)
                    this.hitbox.gameObject.SetActive(false);

                this.StartCoroutine(this.HitPlayerCoroutine(player));
            }
            else
            {
                player.TakeDamage(this.transform.position, 40f, 0f, 0f, 0f, true, true, false, false, true, false, true);
                player.OnHitFromPlayer?.Invoke(this.belongsTo);
            }
        }
    }

    private IEnumerator HitPlayerCoroutine(TestPlayer player)
    {
        if (player.animations != null)
            player.animations.LayDown();


        float currentTime = 0;
        float duration = 0.05f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        if (player.transform.position.y > 0f)
        {
            /*while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
                yield return null;
            }*/

            while (this.transform.position.y > 0f)
            {
                //currentTime += Time.deltaTime;
                player.transform.position = new Vector3(player.transform.position.x, this.transform.position.y - 1f, 0);

                /*if (player.animations != null)
                    player.animations.LayDown();*/
                yield return null;
            }
        }
        player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);

        /*if (player.collision != null)
            player.collision.enabled = false;*/

        player.TakeDamage(this.transform.position, 20f);

        if (player.animations != null)
            player.animations.LayDown();
    }

    public void Explode()
    {
        if (!this.exploded)
        {
            this.exploded = true;

            if (this.explosion != null)
                this.explosion.SetActive(true);

            if (this.model != null)
                this.model.gameObject.SetActive(false);

            if (this.collisions != null)
                this.collisions.gameObject.SetActive(false);

            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            if (this.victim != null)
            {
                TestPlayer victimm = this.victim;

                this.StopRoadRoller(this.victim);



                victimm.TakeDamage(new Vector3(this.transform.position.x, this.transform.position.y - 1f, 0f), 15f, 1.35f, this.transform.forward.z * 1000f, 1200f, true, true, true, false, true, false, true);

                if (!victimm.dead)
                    victimm.animations.SetDefaultPose();
            }

            if (this.explosionSfx != null)
            {
                //this.explosionSfx.time = 0.01f;
                this.explosionSfx.Play();
            }

            this.StartCoroutine(this.DisableCoroutine());
        }
        
    }

    public void LandEffect()
    {
        if (this.landingEffect != null)
            this.landingEffect.SetActive(true);
    }

    public void DisableHitbox()
    {
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
    }


    public void StopRoadRoller(TestPlayer player)
    {
        player.knockbackInvounrability = false;

        player.attackStuns.Remove(this.gameObject);
        player.preventDeath = false;

        if (player.hitboxes != null)
            player.hitboxes.gameObject.SetActive(true);

        //player.animations.EnableBloodPuddle(false);

        if (!player.dead)
        {
            player.rb.isKinematic = false;
            /*if (player.collision != null)
                player.collision.enabled = true;*/

            //player.OnHit?.Invoke();

            //Debug.Log("hello");
            /*if (player.animations != null)
                player.animations.SetDefaultPose();*/
        }

        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        this.victim = null;
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.hitbox != null)
                this.hitbox.belongsTo = player;

            if (this.skin != null && player.skin != null && player.skin.skin != null)
                this.skin.SetSkin(player.skin.skin);
        }
    }
}
