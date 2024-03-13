using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public LandMine landmine;
    public GameObject landmineModel;

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
            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.PlaceLandMineCorutine());
            }
        }
    }

    private IEnumerator PlaceLandMineCorutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        if (this.animations != null)
            this.animations.SetStartThrowBombPose();

        yield return new WaitForSeconds(0.4f);

        if (this.landmineModel != null)
            this.landmineModel.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        if (this.landmineModel != null)
            this.landmineModel.SetActive(false);

        //yield return new WaitForSeconds(time);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.PlaceLandmine();

        yield return new WaitForSeconds(0.3f);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();*/

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void PlaceLandmine()
    {
        if (this.landmine != null)
        {
            LandMine landminePrefab = this.landmine;
            landminePrefab = Instantiate(landminePrefab, new Vector3(this.transform.position.x, 0, 0), Quaternion.Euler(0, 0, 0));

            if (this.user != null)
                landminePrefab.SetOwner(this.user);
        }
    }

    public override void Stop()
    {
        base.Stop();
        if (this.landmineModel != null)
            this.landmineModel.SetActive(false);
        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
}
