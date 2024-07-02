using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFourAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public GameObject cFourModel;
    public GameObject cFourDetonatior;

    

    public CFour cFour;

    public CFour activeCFour;

    public GameObject c4LampP1;
    public GameObject c4LampP2;

    public override void OnHit()
    {
        base.OnHit();
        if (!this.user.dead && this.onGoing)
        {
            this.Stop();
            /*if (this.animations != null)
                this.animations.SetDefaultPose();*/
        }
    }

    public override void OnDeath()
    {
        if (this.onGoing)
            this.Stop();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (this.user != null)
            this.user.OnDisableItems += this.DisableItem;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (this.user != null)
            this.user.OnDisableItems -= this.DisableItem;
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
            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());*/
            if (this.c4LampP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
            {
                this.c4LampP2.SetActive(true);

                if (this.c4LampP1 != null)
                    this.c4LampP1.SetActive(false);
            }
            else
            {
                if (this.c4LampP1 != null)
                    this.c4LampP1.SetActive(true);

                if (this.c4LampP2 != null)
                    this.c4LampP2.SetActive(false);
            }


            if (this.activeCFour != null)
            {
                this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.DetonateCFourCorutine());
            }
            else
            {
                /*this.user.AddStun(0.2f, false);
                this.StartCoroutine(this.PlaceCFourCorutine());*/
                if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
                {
                    this.user.AddStun(0.2f, false);
                    this.StartCoroutine(this.PlaceCFourCorutine(0.4f));
                }
                else
                {
                    this.user.AddStun(0.2f, false);
                    this.StartCoroutine(this.PlaceCFourCorutine(0.2f));
                }

            }

            /*if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {


            }*/
        }
    }

    private IEnumerator PlaceCFourCorutine(float time = 0.4f)
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartThrowBombPose();

        yield return new WaitForSeconds(time);

        if (this.cFourModel != null)
            this.cFourModel.SetActive(true);

        yield return new WaitForSeconds(time);

        if (this.cFourModel != null)
            this.cFourModel.SetActive(false);

        //yield return new WaitForSeconds(time);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.PlaceCFour();

        yield return new WaitForSeconds(0.3f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    private IEnumerator DetonateCFourCorutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        

        if (this.animations != null)
            this.animations.C4Detonate(0);

        yield return new WaitForSeconds(0.1f);

        if (this.cFourDetonatior != null)
            this.cFourDetonatior.SetActive(true);

        /*yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.C4Detonate(1);*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.C4Detonate(2);
        yield return new WaitForSeconds(0.1f);

        if (this.activeCFour != null)
            this.activeCFour.Detonate();

        /*yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.C4Detonate(1);*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.C4Detonate(0);

        yield return new WaitForSeconds(0.1f);

        if (this.cFourDetonatior != null)
            this.cFourDetonatior.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        

        if (this.animations != null)
            this.animations.SetDefaultPose();

        

        yield return new WaitForSeconds(0.3f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlaceCFour()
    {
        if(this.cFour != null)
        {
            CFour cFourPrefab = this.cFour;

            if (this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                cFourPrefab.isP2 = true;
            else
                cFourPrefab.isP2 = false;

            cFourPrefab = Instantiate(cFourPrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y, 0), this.user.transform.rotation);

            this.activeCFour = cFourPrefab;
            if (this.user != null)
                cFourPrefab.SetOwner(this.user, this);

            
        }
    }

    public override void Stop()
    {
        base.Stop();

        /*if (this.cFourModel != null)
            this.cFourModel.SetActive(false);

        if (this.cFourDetonatior != null)
            this.cFourDetonatior.SetActive(false);*/

        if (this.user != null && !this.user.stopAnimationOnHit)
        {
            this.DisableItem();
        }

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void DisableItem()
    {
        if (this.cFourModel != null)
            this.cFourModel.SetActive(false);

        if (this.cFourDetonatior != null)
            this.cFourDetonatior.SetActive(false);
    }
}
