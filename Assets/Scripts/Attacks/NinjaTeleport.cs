using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaTeleport : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public Movement movement;
    public GameObject teleportEffect;
    public float teleportEffeftYPos = 0f;
    private float teleportDistance = 7f;
    public TestHitbox hitbox;

    public bool shrink;

    [Space]

    public float stun1 = 0.2f;
    public float stun2 = 0.4f;
    //public Transform cameraTransform;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.NinjaTeleport();

        if (this.user.rb != null)
            this.user.rb.isKinematic = true;

        yield return new WaitForSeconds(this.stun1);

        /*if (this.teleportEffect != null)
        {
            GameObject teleportEffectPrefab = this.teleportEffect;
            teleportEffectPrefab = Instantiate(teleportEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }*/

        float currentTime = 0;
        float duration = 0.05f;
        //float targetVolume = 0.1f;
        float targetScale = 0f;
        //float startScale = 1f;
        float startScaleX = this.animations.body.transform.localScale.x;
        float startScaleZ = this.animations.body.transform.localScale.z;
        if (this.shrink)
        {
            
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                if (this.animations != null)
                    this.animations.body.transform.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScale, currentTime / duration), 1, Mathf.Lerp(startScaleZ, targetScale, currentTime / duration));
                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }
        


        this.Disappear(true);

        if (this.teleportEffect != null)
        {
            GameObject teleportEffectPrefab = this.teleportEffect;
            teleportEffectPrefab = Instantiate(teleportEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.teleportEffeftYPos, 0), Quaternion.Euler(0, 0, 0));
        }

        //yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(this.stun2);
        
        this.Teleport();
        yield return new WaitForSeconds(0.1f);
        //yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        /*yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);*/
        this.user.LookAtTarget();

        this.Disappear(false);
        if (this.teleportEffect != null)
        {
            GameObject teleportEffectPrefab = this.teleportEffect;
            teleportEffectPrefab = Instantiate(teleportEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.teleportEffeftYPos, 0), Quaternion.Euler(0, 0, 0));
        }

        currentTime = 0;
        duration = 0.05f;
        //float targetVolume = 0.1f;
        targetScale = 1f;
        //float startScale = 1f;
        startScaleX = 0f;
        startScaleZ = 0f;
        if (this.shrink)
        {
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                if (this.animations != null)
                    this.animations.body.transform.localScale = new Vector3(Mathf.Lerp(startScaleX, targetScale, currentTime / duration), 1, Mathf.Lerp(startScaleZ, targetScale, currentTime / duration));
                //pose.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(startScale, targetScale, currentTime / duration), 0);
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }
        



        /*if (this.teleportEffect != null)
        {
            GameObject teleportEffectPrefab = this.teleportEffect;
            teleportEffectPrefab = Instantiate(teleportEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.teleportEffeftYPos, 0), Quaternion.Euler(0, 0, 0));
        }*/
        //yield return new WaitForSeconds(0.05f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(this.stun1);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Disappear(bool disappear = true)
    {
        if (this.user.rb != null)
            this.user.rb.isKinematic = disappear;

        if (this.user.collision != null)
            this.user.collision.enabled = !disappear;

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(!disappear);

        if (this.user.hitboxes != null)
            this.user.hitboxes.gameObject.SetActive(!disappear);
    }

    public override void Stop()
    {
        base.Stop();

        if (this.teleportEffect != null && this.onGoing && this.user.dead)
        {
            this.user.LookAtTarget();

            GameObject teleportEffectPrefab = this.teleportEffect;
            teleportEffectPrefab = Instantiate(teleportEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y + this.teleportEffeftYPos, 0), Quaternion.Euler(0, 0, 0));

            if (this.animations != null)
                this.animations.body.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        

        if (this.hitbox != null /*&& this.onGoing*/)
            this.hitbox.gameObject.SetActive(false);

        if (!this.user.dead /*&& this.onGoing*/)
            this.Disappear(false);

        if (this.animations != null /*&& this.onGoing*/)
            this.animations.body.gameObject.SetActive(true);


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Teleport()
    {
        //this.user.transform.position = new Vector3(this.transform.position.x + 5f, this.transform.position.y, 0f);
        if (this.user.movement != null && GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameCamera cameraa = GameManager.Instance.gameCamera;

            /*if (this.movement.playerInput.moveInput.x > 0)
            {
                this.user.transform.position = new Vector3(this.transform.position.x + 5f, this.transform.position.y, 0f);
                if (cameraa != null && this.user.transform.position.x > cameraa.transform.position.x + 7.5f)
                    this.user.transform.position = new Vector3(cameraa.transform.position.x + 7.5f, this.transform.position.y, 0f);
            }
            else if (this.movement.playerInput.moveInput.x < 0)
            {
                this.user.transform.position = new Vector3(this.transform.position.x - 5f, this.transform.position.y, 0f);
                if (cameraa != null && this.user.transform.position.x < cameraa.transform.position.x - 7.5f)
                    this.user.transform.position = new Vector3(cameraa.transform.position.x - 7.5f, this.transform.position.y, 0f);
            }*/
            //Debug.Log(Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)));
            //Debug.Log(Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)));
            if (this.user.movement.playerInput.moveInput.x > 0)
            {
                
                this.user.transform.position = new Vector3(this.transform.position.x + this.teleportDistance, this.transform.position.y, 0f);
                if(this.user.transform.position.x > 14f && Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) <= 15f)
                {
                    this.user.transform.position = new Vector3(14f, this.transform.position.y, 0f);
                }
                else if (this.user.tempOpponent != null && Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) > 15f)
                {
                    this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x + 15f, this.transform.position.y, 0f);
                    if (this.user.transform.position.x > 14f)
                    {
                        this.user.transform.position = new Vector3(14f, this.transform.position.y, 0f);
                    }
                }
                /*else if (this.user.transform.position.x > 14f)
                {
                    this.user.transform.position = new Vector3(14f, this.transform.position.y, 0f);
                }*/
            }
            else if (this.user.movement.playerInput.moveInput.x < 0)
            {
                this.user.transform.position = new Vector3(this.transform.position.x - this.teleportDistance, this.transform.position.y, 0f);
                if (this.user.transform.position.x < -14f && Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) <= 15f)
                {
                    this.user.transform.position = new Vector3(-14f, this.transform.position.y, 0f);
                }
                else if (this.user.tempOpponent != null && Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) > 15f)
                {
                    this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x - 15f, this.transform.position.y, 0f);
                    if (this.user.transform.position.x < -14f)
                    {
                        this.user.transform.position = new Vector3(-14f, this.transform.position.y, 0f);
                    }
                }
                /*else if (this.user.transform.position.x < -14f)
                {
                    this.user.transform.position = new Vector3(-14f, this.transform.position.y, 0f);
                }*/

            }
            
        }
        else if (this.user.movement != null && GameManager.Instance == null /*&& GameManager.Instance.gameCamera == null*/) //for fight ball, find better solution later
        {
            if (this.user.movement.playerInput.moveInput.x > 0)
            {
                this.user.transform.position = new Vector3(this.transform.position.x + this.teleportDistance, this.transform.position.y, 0f);
                if (this.user.transform.position.x > 11f)
                    this.user.transform.position = new Vector3(11f, this.transform.position.y, 0f);
            }
            else if (this.user.movement.playerInput.moveInput.x < 0)
            {
                this.user.transform.position = new Vector3(this.transform.position.x - this.teleportDistance, this.transform.position.y, 0f);
                if (this.user.transform.position.x < -11f)
                    this.user.transform.position = new Vector3(-11f, this.transform.position.y, 0f);

            }
        }
    }
}
