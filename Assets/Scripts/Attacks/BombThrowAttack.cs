using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrowAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;
    public bool holdingBomb;
    public bool holdingGrenade;
    public bool holdingFlashbang;

    public Bomb bomb;
    public GameObject bombModel;
    public float bombXForce;
    public float bombYForce;

    public Bomb grenade;
    public GameObject grenadeModel;
    public float grenadeXForce;
    public float grenadeYForce;

    public Bomb flashbang;
    public GameObject flashbangModel;

    [Space]

    public float groundTime = 0.5f;
    public float groundStunTime = 0.4f;

    public float airTime = 0.2f;
    public float airStunTime = 0.3f;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            if (this.animations != null)
                this.animations.SetDefaultPose();

            /*if (this.holdingBomb)
                this.ThrowBomb(true);*/
        }
    }

    private void Update()
    {
        if (this.onGoing)
        {
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                this.user.rb.velocity = new Vector3(0f, this.user.rb.velocity.y, 0f);
        }
    }

    [ContextMenu("Initiate")]
    public override void Initiate()
    {
        base.Initiate();
        if (this.user != null)
        {
            //this.user.AddStun(0.2f, true);
            //this.StartCoroutine(this.ThrowBombCorutine());
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f) //On Air
            {
                //this.user.AddStun(1f, true);
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.ThrowBombCorutine(this.groundTime, this.groundStunTime));
            }
            else
            {
                //this.user.AddStun(0.6f, false);
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.ThrowBombCorutine(this.airTime, this.airStunTime));
            }
        }
    }

    private IEnumerator ThrowBombCorutine(float time = 0.5f, float stunTime = 0.5f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;
        if (this.animations != null)
            this.animations.SetStartThrowBombPose();

        float number = Random.Range(0f, 100f);
        //Debug.Log(number);

        yield return new WaitForSeconds(time / 2);

        if(number <= 10 && number >= 1)
        {
            if (this.grenadeModel != null)
                this.grenadeModel.SetActive(true);

            this.holdingGrenade = true;
        }
        else if (number <= 1)
        {
            if (this.flashbangModel != null)
                this.flashbangModel.SetActive(true);

            this.holdingFlashbang = true;
        }
        else
        {
            if (this.bombModel != null)
                this.bombModel.SetActive(true);

            this.holdingBomb = true;
        }
        /*if (this.bombModel != null)
            this.bombModel.SetActive(true);

        this.holdingBomb = true;*/

        /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f && Mathf.Abs(this.user.rb.velocity.x) > 0f)
            this.user.AddStun(0.1f, true);*/

        yield return new WaitForSeconds(time / 2);

        /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f && Mathf.Abs(this.user.rb.velocity.x) > 0f)
            this.user.AddStun(0.1f, true);*/

        if (this.bombModel != null)
            this.bombModel.SetActive(false);
        if (this.grenadeModel != null)
            this.grenadeModel.SetActive(false);

        if (this.flashbangModel != null)
            this.flashbangModel.SetActive(false);

        if (this.holdingBomb)
            this.ThrowBomb();
        else if (this.holdingGrenade)
            this.ThrowGrenade();
        else if (this.holdingFlashbang)
            this.ThrowFlashbang();


        this.holdingBomb = false;
        this.holdingGrenade = false;

        this.holdingFlashbang = false;

        if (this.animations != null)
            this.animations.SetPunchPose();
        //this.ThrowBomb();

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();

        /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f && Mathf.Abs(this.user.rb.velocity.x) > 0f)
            this.user.AddStun(0.1f, true);*/

        yield return new WaitForSeconds(stunTime);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);


        /*this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        yield return new WaitForSeconds(1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);*/
    }

    public void ThrowBomb(bool canceled = false)
    {
        if (this.bomb != null && this.user != null)
        {
            Bomb bombPrefab = this.bomb;
            float forward = this.user.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            if (!canceled)
            {
                bombPrefab = Instantiate(bombPrefab, new Vector3(forward + this.user.transform.position.x, this.user.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));
                bombPrefab.KnockBack(new Vector3(forward * this.bombXForce, this.bombYForce, 0));
            }
            else
            {
                bombPrefab = Instantiate(bombPrefab, new Vector3(this.user.transform.position.x + (forward * -1), this.user.transform.position.y + 1, 0), Quaternion.Euler(0, 0, 0));
                bombPrefab.KnockBack(new Vector3(0f, 0f, 0f));
            }

            if (this.user != null)
                bombPrefab.SetOwner(this.user);
        }
    }

    public void ThrowGrenade(bool canceled = false)
    {
        if (this.grenade != null && this.user != null)
        {
            Bomb grenadePrefab = this.grenade;
            float forward = this.user.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            if (!canceled)
            {
                grenadePrefab = Instantiate(grenadePrefab, new Vector3(forward + this.user.transform.position.x, this.user.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));
                grenadePrefab.KnockBack(new Vector3(forward * this.grenadeXForce, this.grenadeYForce, 0));
            }
            else
            {
                grenadePrefab = Instantiate(grenadePrefab, new Vector3(this.user.transform.position.x + (forward * -1), this.user.transform.position.y + 1, 0), Quaternion.Euler(0, 0, 0));
                grenadePrefab.KnockBack(new Vector3(0f, 0f, 0f));
            }

            if (this.user != null)
                grenadePrefab.SetOwner(this.user);
        }
    }

    public override void Stop()
    {
        base.Stop();
        if (this.bombModel != null)
            this.bombModel.SetActive(false);

        if (this.grenadeModel != null)
            this.grenadeModel.SetActive(false);

        if (this.flashbangModel != null)
            this.flashbangModel.SetActive(false);

        if (this.holdingBomb)
            this.ThrowBomb(true);
        else if (this.holdingGrenade)
            this.ThrowGrenade(true);
        else if (this.holdingFlashbang)
            this.ThrowFlashbang(true);

        this.holdingBomb = false;
        this.holdingGrenade = false;
        this.holdingFlashbang = false;

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }


    public void ThrowFlashbang(bool canceled = false)
    {
        if (this.flashbang != null && this.user != null)
        {
            Bomb flashbangPrefab = this.flashbang;
            float forward = this.user.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            if (!canceled)
            {
                flashbangPrefab = Instantiate(flashbangPrefab, new Vector3(forward + this.user.transform.position.x, this.user.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));
                flashbangPrefab.KnockBack(new Vector3(forward * this.grenadeXForce, this.grenadeYForce, 0));
            }
            else
            {
                flashbangPrefab = Instantiate(flashbangPrefab, new Vector3(this.user.transform.position.x + (forward * -1), this.user.transform.position.y + 1, 0), Quaternion.Euler(0, 0, 0));
                flashbangPrefab.KnockBack(new Vector3(0f, 0f, 0f));
            }

            if (this.user != null)
                flashbangPrefab.SetOwner(this.user);
        }
    }
}
