using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSmoke : MonoBehaviour
{
    public TestPlayer belongsTo;

    public SphereCollider sphereCollider;

    public TestHitbox hitSphere;

    public float damage = -20f;

    public bool isStatusEffect;
    public float statusEffectDuration = 5f;
    public StatusEffectHitbox statusEffectHitbox;

    //THIS WILL GIVE STATUS EFFECT
    //THE STATUS EFFECT LAST LONGER THE FURTHER IN THE HITBOX YOU ARE
    //WILL ALSO HAVE INSTANT HEALING, DAMAGE, SUPER METER
    //the further in the hitbox (more like hitsphere) the more healing/damage it does if its an instant effect

    private void OnEnable()
    {
        if (this.hitSphere != null)
        {
            //this.hitSphere.gameObject.SetActive(true);
            this.hitSphere.damage = this.damage;
        }

        if (this.statusEffectHitbox != null)
        {
            //this.hitSphere.gameObject.SetActive(true);
            this.statusEffectHitbox.duration = this.statusEffectDuration;
        }

        this.sphereCollider = this.GetComponent<SphereCollider>();
        if (this.isStatusEffect)
            this.StartCoroutine(this.GrowStatusEffectHitbox());
        else
            this.StartCoroutine(this.TestGrowHitbox());
    }

    private void OnDisable()
    {
        if (this.hitSphere != null)
        {
            this.hitSphere.gameObject.SetActive(false);
            this.hitSphere.transform.localScale = new Vector3(1f, 1f, 1f);
            this.hitSphere.damage = this.damage;
        }

        if (this.statusEffectHitbox != null)
        {
            this.statusEffectHitbox.gameObject.SetActive(false);
            this.statusEffectHitbox.transform.localScale = new Vector3(1f, 1f, 1f);
            this.statusEffectHitbox.duration = this.statusEffectDuration;
        }

    }


    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            /*if (this.hitbox != null)
                this.hitbox.belongsTo = player;*/
        }
    }
    /*private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, 0f), 1.5f, transform.up, out hit, 10))
        {
            Debug.Log(hit.distance);
        }
    }*/


    private IEnumerator TestGrowHitbox()
    {
        float startScale = 1.5f;


        this.hitSphere.transform.localScale = new Vector3(startScale, startScale, startScale);
        this.hitSphere.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.001f);

        //yield return new WaitForSeconds(0.1f);

        //this.hitSphere.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        float currentTime = 0f;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetScale = 4f;
        //float startScale = 1.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.hitSphere != null)
            {
                this.hitSphere.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration));
                this.hitSphere.damage = Mathf.Round(Mathf.Lerp(this.damage, this.damage * 0.25f, currentTime / duration));
                //Debug.Log(this.hitSphere.damage);
            }
            yield return null;
        }

        currentTime = 0f;
        duration = 0.3f;
        //float targetVolume = 0.1f;
        targetScale = 6f;
        startScale = 4f;

        float minDamage = 1f;
        if (this.damage < 0f)
            minDamage = -1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.hitSphere != null)
            {
                this.hitSphere.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration));
                this.hitSphere.damage = Mathf.Round(Mathf.Lerp(this.damage * 0.25f, minDamage, currentTime / duration));
                //Debug.Log(this.hitSphere.damage);
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);
        this.hitSphere.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

    }

    private IEnumerator GrowStatusEffectHitbox()
    {
        float startScale = 1.5f;


        this.statusEffectHitbox.transform.localScale = new Vector3(startScale, startScale, startScale);
        this.statusEffectHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        //yield return new WaitForSeconds(0.001f);

        //yield return new WaitForSeconds(0.1f);

        //this.statusEffectHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        float currentTime = 0f;
        float duration = 0.2f;
        //float targetVolume = 0.1f;
        float targetScale = 4f;
        //float startScale = 1.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.statusEffectHitbox != null)
            {
                this.statusEffectHitbox.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration));
                //this.hitSphere.damage = Mathf.Round(Mathf.Lerp(this.damage, this.damage * 0.25f, currentTime / duration));

                //this.statusEffectHitbox.duration = Mathf.Lerp(this.statusEffectDuration, this.statusEffectDuration * 0.25f, currentTime / duration);
                this.statusEffectHitbox.duration = Mathf.Round(Mathf.Lerp(this.statusEffectDuration, this.statusEffectDuration * 0.25f, currentTime / duration) * 10000) * 0.0001f;
                //Debug.Log(this.hitSphere.damage);
            }
            yield return null;
        }

        currentTime = 0f;
        duration = 0.3f;
        //float targetVolume = 0.1f;
        targetScale = 6f;
        startScale = 4f;

        float minDuration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.statusEffectHitbox != null)
            {
                this.statusEffectHitbox.transform.localScale = new Vector3(Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration), Mathf.Lerp(startScale, targetScale, currentTime / duration));
                //this.hitSphere.damage = Mathf.Round(Mathf.Lerp(this.damage * 0.25f, minDuration, currentTime / duration));

                //this.statusEffectHitbox.duration = Mathf.Lerp(this.statusEffectDuration * 0.25f, minDuration, currentTime / duration);
                this.statusEffectHitbox.duration = Mathf.Round(Mathf.Lerp(this.statusEffectDuration * 0.25f, minDuration, currentTime / duration) * 10000) * 0.0001f;
                //Debug.Log(this.hitSphere.damage);
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);
        this.statusEffectHitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

    }

    /*private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        //RaycastHit hit;
        if (player != null)
        {
            *//*if (Physics.SphereCast(new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, 0f), 1.5f, transform.up, out hit, 10))
            {
                Debug.Log(hit.distance);
            }*//*


            
            float distanceX = Vector3.Distance(new Vector3(player.transform.position.x, 0f, 0f), new Vector3(this.transform.position.x, 0f, 0f));
            //float distanceY = Vector3.Distance(new Vector3(0f, player.transform.position.y + player.animations.defaultYPos, 0f), new Vector3(0f, this.transform.position.y, 0f));
            float distanceY = Vector3.Distance(new Vector3(0f, player.transform.position.y, 0f), new Vector3(0f, this.transform.position.y, 0f));

            float distance = Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y + player.animations.defaultYPos, 0f), this.transform.position);

            //float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (player.collision != null)
            {
                float collisionRadious = player.collision.GetComponent<CapsuleCollider>().radius*//* * 2f*//*;
                float collisionHeight = player.collision.GetComponent<CapsuleCollider>().height*//* * 2f*//*;
                float percent = Mathf.InverseLerp((2.5f * collisionRadious) + collisionRadious, 1, distance);

                float percentX = Mathf.InverseLerp((2.5f * collisionRadious) + collisionRadious, 1f, distanceX);
                //float percentY = Mathf.InverseLerp((2.5f * collisionRadious) + collisionRadious, 1f, distanceY);
                float percentY = Mathf.InverseLerp((2.5f * collisionHeight) + collisionHeight, 1f, distanceY);

                float percentTotal = percentX + percentY;

                *//*Debug.Log(percent);
                Debug.Log(distance);*/

    /*Debug.Log("percentX: " + percentX);
    Debug.Log("percentY: " + percentY);
    Debug.Log("percentTotal: " + percentTotal);*//*

    //float percent2 = Mathf.InverseLerp(Mathf.PI* 2.5f, 1f, distance);
    //float percent2 = Mathf.Lerp((2.5f / distance) - distance, 1f, distance);
    //float percent2 = Mathf.Lerp((2.5f / distance) - distance, 1f, distance);
    float percent2 = Mathf.InverseLerp(5f, 0f, distance);

    Debug.Log("distance: " + distance);
    Debug.Log("percent2: " + percent2);
    Debug.Log("percentTotal: " + percentTotal);

}
}
}*/
}
