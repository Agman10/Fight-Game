using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosiveDetonationAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject explosiveBox;

    public Explosion explosion;
    public Transform handle;

    public VisualEffect smoke;

    //public ParticleSystem particle;

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

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    /*private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }*/

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.ExplosiveDetonationCoroutine());
            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator ExplosiveDetonationCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        if (this.animations != null)
            this.animations.ExplosiveBox(0);

        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(true);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);

        yield return new WaitForSeconds(0.35f);
        if (this.animations != null)
            this.animations.ExplosiveBox(1);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.17f, 0f);


        yield return new WaitForSeconds(0.1f);
        if (this.explosion != null)
        {
            Explosion explosionPrefab = this.explosion;

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0), Quaternion.Euler(0, 0, 0));

            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0), Quaternion.Euler(0, 0, 0));
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, 0), Quaternion.Euler(0, 0, 0));

            explosionPrefab.SetOwner(this.user);

            explosionPrefab.SetDamage(15f, 5f, 500f, 800f, 200f, 300f, 5f, 3f, 0.5f, 0.2f, true, false);

            //explosionPrefab.SetSize(1.5f);

            //explosionPrefab.SetSize(1.6f);
            explosionPrefab.SetSize(1.55f);
            
        }

        if (this.smoke != null)
            this.smoke.Play();
        //this.user.TakeDamage(this.user.transform.position, 5);

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.ExplosiveBox(1, true);

        this.user.TakeDamage(this.user.transform.position, 5);

        //yield return new WaitForSeconds(0.7f);


        yield return new WaitForSeconds(0.4f);

        if (this.smoke != null)
            this.smoke.Stop();

        yield return new WaitForSeconds(0.3f);



        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(false);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);

        //End

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;

        //yield return new WaitForSeconds(0.3f);

        float currentTime = 0;
        float duration = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
            yield return null;
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        /*if (this.particle != null)
            this.particle.Stop();*/

        this.user.rb.isKinematic = false;

        if (this.explosiveBox != null)
            this.explosiveBox.SetActive(false);

        if (this.handle != null)
            this.handle.localPosition = new Vector3(0f, 1.7f, 0f);

        if (this.smoke != null)
            this.smoke.Stop();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
