using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    public TestHitbox hitbox;
    public GameObject model;
    public Transform blood;
    public TestPlayer victim;
    public GameObject bloodEffect;
    public GameObject oilEffect;
    public GameObject landingEffect;

    public TestHitbox growHitbox;

    public TestPlayer belongsTo;

    public GameObject alternateModel;
    //public GameObject breakEffect;

    private void OnEnable()
    {
        if (this.hitbox != null)
        {
            //this.hitbox.gameObject.SetActive(true);
            this.hitbox.OnPlayerCollision += this.HitPlayer;
        }

        int number = Random.Range(1, 101);

        if (number == 1 && this.alternateModel != null)
        {
            this.model.gameObject.SetActive(false);

            this.alternateModel.gameObject.SetActive(true);
            this.model = this.alternateModel;
        }

        this.StartCoroutine(this.FallDownCoroutine());
    }

    private void OnDisable()
    {
        if (this.hitbox != null)
        {
            this.hitbox.gameObject.SetActive(true);
            this.hitbox.OnPlayerCollision -= this.HitPlayer;
        }
        if (this.victim != null)
            this.StopAnvil(this.victim);

        if (this.model != null)
            this.model.gameObject.SetActive(true);

        if (this.bloodEffect != null)
            this.bloodEffect.SetActive(false);

        if (this.oilEffect != null)
            this.oilEffect.SetActive(false);

        if (this.landingEffect != null)
            this.landingEffect.SetActive(false);

        this.StopAllCoroutines();
    }

    public void HitPlayer(TestPlayer player)
    {
        if(this.victim == null && player != null)
        {
            if (!player.knockbackInvounrability)
            {
                float yRot = player.animations.body.localEulerAngles.y;

                player.attackStuns.Add(this.gameObject);
                player.OnHit?.Invoke();
                player.rb.isKinematic = true;
                player.knockbackInvounrability = true;
                this.victim = player;
                player.preventDeath = true;


                if (player.animations != null)
                {
                    player.animations.SetDefaultPose();
                    player.animations.body.localEulerAngles = new Vector3(0f, yRot, 0f);
                }


                if (this.hitbox != null)
                    this.hitbox.gameObject.SetActive(false);

                this.StartCoroutine(this.SquishPlayerCoroutine(player));
            }
            else
            {
                player.TakeDamage(this.transform.position, 32f);
                if (this.belongsTo != null)
                    player.OnHitFromPlayer?.Invoke(this.belongsTo);
            }
        }
        
    }

    private IEnumerator FallDownCoroutine()
    {
        float currentTime = 0;
        float duration = 0.4f;
        //float targetVolume = 0.1f;
        float targetPosition = 0f;
        float start = this.transform.position.y;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);

            yield return null;
        }
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        if (this.landingEffect != null)
            this.landingEffect.SetActive(true);

        if (this.victim != null)
        {
            if (this.victim.characterId == 2)
            {
                if (this.oilEffect != null)
                {
                    this.oilEffect.transform.position = new Vector3(this.victim.transform.position.x, 0f, 0f);
                    this.oilEffect.SetActive(true);
                }
            }
            else
            {
                if (this.bloodEffect != null)
                {
                    this.bloodEffect.transform.position = new Vector3(this.victim.transform.position.x, 0f, 0f);
                    this.bloodEffect.SetActive(true);
                }
            }
        }
            

        yield return new WaitForSeconds(0.5f);
        if (this.model != null)
            this.model.gameObject.SetActive(false);

        /*if (this.breakEffect != null)
        {
            this.breakEffect.SetActive(true);
        }*/

        yield return new WaitForSeconds(0.5f);

        if(this.victim == null)
            this.gameObject.SetActive(false);
    }


    private IEnumerator SquishPlayerCoroutine(TestPlayer player)
    {
        //player.OnHit?.Invoke();
        /*if (player.animations != null)
            player.animations.SetDefaultPose();*/

        /*if (player.animations != null)
            player.animations.AnvilHit();*/

        float currentTime = 0;
        float duration = 0.05f;
        float targetPosition = 0f;
        float start = player.transform.position.y;
        if(player.transform.position.y > 0f)
        {
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                player.transform.position = new Vector3(player.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0);
                yield return null;
            }
        }
        player.transform.position = new Vector3(player.transform.position.x, 0f, 0f);

        if (player.collision != null)
            player.collision.enabled = false;

        player.TakeDamage(this.transform.position, 32f);


        if (player.hitboxes != null)
            player.hitboxes.gameObject.SetActive(false);

        if (player.animations != null)
        {
            currentTime = 0;
            //duration = 0.15f;
            duration = 0.1f;


            float startPosY = player.animations.body.localPosition.y;
            float targetPosY = player.animations.body.localPosition.y * 0.01f;

            float startScaleY = player.animations.body.localScale.y;
            float startScaleX = player.animations.body.localScale.x;
            float startScaleZ = player.animations.body.localScale.z;

            float targetScaleY = 0.01f;
            float targetScaleX = 1.25f;
            float targetScaleZ = player.animations.body.localScale.z * 1.25f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                player.animations.body.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), player.animations.body.localPosition.z);
                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
        }
        //player.TakeDamage(this.transform.position, 32f);

        /*if (this.blood != null)
        {
            this.blood.gameObject.SetActive(true);
            this.blood.transform.position = new Vector3(player.transform.position.x, this.blood.transform.position.y, 0f);

            *//*float startBloodScale = this.blood.localScale.x;
            float targetBloodScale = 3f;
            currentTime = 0;
            duration = 0.3f;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.blood.localScale = new Vector3(Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration), 1f, Mathf.Lerp(startBloodScale, targetBloodScale, currentTime / duration));

                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }*//*

        }*/
        player.animations.EnableBloodPuddle(true);
        /*if (this.bloodEffect != null)
            this.bloodEffect.SetActive(true);*/


        /*if (this.model != null)
            this.model.gameObject.SetActive(false);*/



        if (player.health <= 0f)
        {
            player.Die(player.transform.position, false, true, false);

            player.preventDeath = false;
            player.knockbackInvounrability = false;
            player.attackStuns.Remove(this.gameObject);
            this.victim = null;
            //player.animations.body.localScale = new Vector3(1.25f, 0.01f, player.animations.body.localScale.z * 1.25f);
            //player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, player.animations.body.localPosition.y * 0.01f, player.animations.body.localPosition.z);

            //yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(1f);
            this.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(2f);

            if (this.growHitbox != null)
            {
                this.growHitbox.transform.position = new Vector3(player.transform.position.x, 0.5f, 0f);
                this.growHitbox.gameObject.SetActive(true);
            }
                

            if (player.animations != null && !player.dead)
            {
                /*currentTime = 0;
                duration = 0.3f;


                float startPosY = player.animations.body.localPosition.y;
                float targetPosY = player.animations.body.localPosition.y * 100f;

                float startScaleY = player.animations.body.localScale.y;
                float startScaleX = player.animations.body.localScale.x;
                float startScaleZ = player.animations.body.localScale.z;

                float targetScaleY = 1f;
                float targetScaleX = 1f;
                float targetScaleZ = 1f;
                if (player.animations.body.localScale.z < 0f)
                    targetScaleZ = -1f;


                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    player.animations.body.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                    player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), player.animations.body.localPosition.z);
                    //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                    yield return null;
                }*/


                currentTime = 0;
                duration = 0.2f;


                float startPosY = player.animations.body.localPosition.y;
                float targetPosY = player.animations.body.localPosition.y * 110f;
                //float targetPosY = player.animations.defaultYPos * 1.1f;
                //float targetPosY = player.animations.defaultYPos * 1.2f;

                float startScaleY = player.animations.body.localScale.y;
                float startScaleX = player.animations.body.localScale.x;
                float startScaleZ = player.animations.body.localScale.z;

                float targetScaleY = 1.1f;
                float targetScaleX = 0.9f;
                //float targetScaleX = 0.8f;
                float targetScaleZ = 0.9f;
                if (player.animations.body.localScale.z < 0f)
                    targetScaleZ = -0.9f;


                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    player.animations.body.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                    player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), player.animations.body.localPosition.z);
                    //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                    yield return null;
                }

                currentTime = 0;
                duration = 0.05f;
                //duration = 0.1f;


                startPosY = player.animations.body.localPosition.y;
                //targetPosY = player.animations.body.localPosition.y * 0.9f;
                targetPosY = player.animations.defaultYPos;

                startScaleY = player.animations.body.localScale.y;
                startScaleX = player.animations.body.localScale.x;
                startScaleZ = player.animations.body.localScale.z;

                targetScaleY = 1f;
                targetScaleX = 1f;
                targetScaleZ = 1f;
                if (player.animations.body.localScale.z < 0f)
                    targetScaleZ = -1f;


                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    player.animations.body.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScaleX, currentTime / duration), Mathf.Lerp(startScaleY, targetScaleY, currentTime / duration), Mathf.Lerp(startScaleZ, targetScaleZ, currentTime / duration));
                    player.animations.body.localPosition = new Vector3(player.animations.body.localPosition.x, Mathf.Lerp(startPosY, targetPosY, currentTime / duration), player.animations.body.localPosition.z);
                    //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                    yield return null;
                }
            }

            /*if (player.collision != null)
                player.collision.enabled = true;*/

            if (this.growHitbox != null)
                this.growHitbox.gameObject.SetActive(false);


            if (player.hitboxes != null)
                player.hitboxes.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.1f);
            if (player.animations != null)
                player.animations.SetDefaultPose();

            player.LookAtTarget();

            if (player.collision != null)
                player.collision.enabled = true;

            /*if (this.blood != null)
            {
                this.blood.gameObject.SetActive(false);
                //this.blood.localScale = Vector3.one;

            }*/

            player.animations.EnableBloodPuddle(false);

            player.preventDeath = false;
            player.knockbackInvounrability = false;
            player.rb.isKinematic = false;
            player.attackStuns.Remove(this.gameObject);
            this.victim = null;

            /*if (player.health <= 0f)
                player.Die(player.transform.position, false, true, false);*/

            //player.TakeDamage(this.transform.position, 0f);

            /*yield return new WaitForSeconds(0.2f);
            if (this.model != null)
                this.model.SetActive(false);*/
            yield return new WaitForSeconds(0.2f);
            this.gameObject.SetActive(false);
        }

        
    }

    public void StopAnvil(TestPlayer player)
    {
        player.knockbackInvounrability = false;
        
        player.attackStuns.Remove(this.gameObject);
        player.preventDeath = false;

        if (player.hitboxes != null)
            player.hitboxes.gameObject.SetActive(true);

        player.animations.EnableBloodPuddle(false);

        if (!player.dead)
        {
            player.rb.isKinematic = false;
            if (player.collision != null)
                player.collision.enabled = true;

            if (player.animations != null)
                player.animations.SetDefaultPose();
        }
        

        this.victim = null;
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            /*if (this.hitbox != null)
                this.hitbox.belongsTo = player;

            if (this.skin != null && player.skin != null && player.skin.skin != null)
                this.skin.SetSkin(player.skin.skin);*/
        }
    }
}
