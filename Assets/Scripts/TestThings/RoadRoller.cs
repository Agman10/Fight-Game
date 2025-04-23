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
    public GameObject smallExplosionEffect;

    public GameObject electricEffects;
    public Transform modelToScale;

    public CharacterSkinTest skin;

    public AudioSource explosionSfx;

    public SoundEffect landingSfx;

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

        //player.TakeDamage(this.transform.position, 20f);
        player.TakeDamage(this.transform.position, 15f);

        if (player.animations != null)
            player.animations.LayDown();
    }

    public void Explode()
    {
        if (!this.exploded)
        {
            //this.StopAllCoroutines();

            this.exploded = true;

            if (this.explosion != null)
                this.explosion.SetActive(true);

            if (this.model != null)
                this.model.gameObject.SetActive(false);

            if (this.collisions != null)
                this.collisions.gameObject.SetActive(false);

            if (this.hitbox != null)
                this.hitbox.gameObject.SetActive(false);

            this.EnableElectricity(false);

            if (this.victim != null)
            {
                TestPlayer victimm = this.victim;

                this.StopRoadRoller(this.victim);



                /*victimm.TakeDamage(new Vector3(this.transform.position.x, this.transform.position.y - 1f, 0f), 15f, 1.35f, this.transform.forward.z * 1000f, 1200f, true, true, true, false, true, false, true);

                if (!victimm.dead)
                    victimm.animations.SetDefaultPose();*/

                victimm.TakeDamage(new Vector3(this.transform.position.x, this.transform.position.y - 1f, 0f), 15f, 1.35f, this.transform.forward.z * 1000f, 1200f, true, true, true, false, true, false, true, true);
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

        this.landingSfx.PlaySound();
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



    public void EnableElectricity(bool activate = true)
    {
        if(this.electricEffects != null)
        {
            this.electricEffects.SetActive(activate);
        }
    }

    [ContextMenu("StartExploding")]
    public void StartExploding()
    {
        //this.StartCoroutine(this.ExplodingCoroutine());
        //this.StartCoroutine(this.ExplodingLoopCoroutine());
        this.StartCoroutine(this.ExplodingCoroutine2());
    }

    private IEnumerator ExplodingCoroutine2()
    {
        if(this.modelToScale != null)
        {
            float currentTime = 0;
            float duration = 0.05f;
            float targetScale = 0.95f;
            Vector3 startScale = this.modelToScale.localScale;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.modelToScale.localScale = new Vector3(Mathf.Lerp(startScale.x, targetScale, currentTime / duration), Mathf.Lerp(startScale.y, targetScale, currentTime / duration), Mathf.Lerp(startScale.z, targetScale, currentTime / duration));
                yield return null;
            }
        }
        this.Explode();
    }





    private IEnumerator ExplodingLoopCoroutine()
    {
        /*int amount = 10;
        while (amount > 0)
        {
            float minXPos = -2.5f;
            float maxXPos = 2.5f;
            float minYPos = 0.5f;
            float maxYPos = 2.5f;

            this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);

            amount -= 1;

            yield return null;
        }*/

        int amount = 2;
        int animId = 0;
        float forwardZ = this.transform.forward.z;
        while (amount > 0)
        {
            float minXPos = -2.5f;
            float maxXPos = 2.5f;
            float minYPos = 0.5f;
            float maxYPos = 2.5f;

            if (animId == 0)
            {
                minXPos = forwardZ * -0.75f;
                maxXPos = forwardZ * 1.5f;
                maxYPos = 2.5f;

                this.SmallExplosionEffect(new Vector3(this.transform.position.x - (forwardZ * 2.35f) + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));
                animId = 1;
            }
            else
            {
                minXPos = forwardZ * -1.5f;
                maxXPos = forwardZ * 0.75f;
                maxYPos = 3f;

                this.SmallExplosionEffect(new Vector3(this.transform.position.x + (forwardZ * 1.6f) + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));
                animId = 0;
            }


            //this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);

            amount -= 1;

            yield return null;
        }

        //yield return new WaitForSeconds(0.1f);

        this.StartCoroutine(this.ExplodingLoopCoroutine());
    }

    private IEnumerator ExplodingCoroutine()
    {
        //yield return new WaitForSeconds(0.1f);

        /*int amount = 40;
        while (amount > 0)
        {
            float minXPos = -2.5f;
            float maxXPos = 2.5f;
            float minYPos = 0.5f;
            float maxYPos = 2.5f;

            //this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            //minXPos = -0.75f;
            //maxXPos = 0.75f;

            minXPos = -0.75f;
            maxXPos = 1.5f;
            this.SmallExplosionEffect(new Vector3(this.transform.position.x - 2.35f + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);
            minXPos = -1.5f;
            maxXPos = 0.75f;
            this.SmallExplosionEffect(new Vector3(this.transform.position.x + 1.6f + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);
            //this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            amount -= 1;

            yield return null;
        }*/


        /*int amount = 10;
        while (amount > 0)
        {
            float minXPos = -2.5f;
            float maxXPos = 2.5f;
            float minYPos = 0.5f;
            float maxYPos = 2.5f;

            this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);

            amount -= 1;

            yield return null;
        }*/

        int amount = 6;
        int animId = 0;
        float forwardZ = this.transform.forward.z;
        while (amount > 0)
        {
            float minXPos = -2.5f;
            float maxXPos = 2.5f;
            float minYPos = 0.5f;
            float maxYPos = 2.5f;

            if (animId == 0)
            {
                minXPos = forwardZ * -0.75f;
                maxXPos = forwardZ * 1.5f;
                maxYPos = 2.5f;

                this.SmallExplosionEffect(new Vector3(this.transform.position.x - (forwardZ * 2.35f) + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));
                animId = 1;
            }
            else
            {
                minXPos = forwardZ * -1.5f;
                maxXPos = forwardZ * 0.75f;
                maxYPos = 3f;

                this.SmallExplosionEffect(new Vector3(this.transform.position.x + (forwardZ * 1.6f) + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));
                animId = 0;
            }


            //this.SmallExplosionEffect(new Vector3(this.transform.position.x + Random.Range(minXPos, maxXPos), Random.Range(minYPos, maxYPos), -2.5f));

            yield return new WaitForSeconds(0.05f);

            amount -= 1;

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        this.Explode();
    }

    public void SmallExplosionEffect(Vector3 position)
    {
        if (this.smallExplosionEffect != null)
        {
            GameObject explosionEffectPrefab = this.smallExplosionEffect;
            explosionEffectPrefab = Instantiate(explosionEffectPrefab, position, Quaternion.Euler(0, 0, 0));
        }
    }
}
