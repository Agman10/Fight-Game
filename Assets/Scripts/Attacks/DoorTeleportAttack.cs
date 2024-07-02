using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleportAttack : Attack
{
    public TempPlayerAnimations animations;
    public bool onGoing;

    public InterdimensionalDoor door;
    public InterdimensionalDoor doorP2;

    public InterdimensionalDoor currentDoor;

    public AudioSource openSfx;
    public AudioSource closeSfx;

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
            if(this.user.transform.position.y > -3.5f)
            {
                this.user.AddStun(0.2f, true);
                this.StartCoroutine(this.TemplateCoroutine());
            }
            /*this.user.AddStun(0.2f, true);
            this.StartCoroutine(this.TemplateCoroutine());*/

            if (Mathf.Abs(this.user.rb.velocity.y) <= 0f)
            {

            }

            /*if (this.user.superCharge >= this.user.maxSuperCharge * 0.5f)
            {
                this.user.GiveSuperCharge(-this.user.maxSuperCharge * 0.5f);
            }*/
        }
    }

    private IEnumerator TemplateCoroutine()
    {
        this.user.attackStuns.Add(this.gameObject);
        this.onGoing = true;

        this.user.rb.isKinematic = true;

        if (this.animations != null)
        {
            this.animations.SetDefaultPose();
            this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * -90f, 0f);
        }

        if (this.door != null)
        {
            InterdimensionalDoor doorPrefab = this.door;

            if (this.doorP2 != null && this.user != null && this.user.tempOpponent != null && this.user.tempOpponent.characterId == this.user.characterId && this.user.playerNumber == 2)
                doorPrefab = this.doorP2;

            doorPrefab = Instantiate(doorPrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y, 1.2f), Quaternion.Euler(0, 0, 0));

            doorPrefab.SetOwner(this.user);

            this.currentDoor = doorPrefab;
        }

        /*if(GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = true;
        }*/

        yield return new WaitForSeconds(0.2f);

        if (this.openSfx != null)
            this.openSfx.Play();
        yield return new WaitForSeconds(0.1f);

        if (this.currentDoor != null)
        {
            this.currentDoor.doorLid.transform.localEulerAngles = new Vector3(0f, 110f, 0f);

            this.currentDoor.ActivateHitbox();
        }



        yield return new WaitForSeconds(0.1f);
        if(this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, this.user.transform.forward.z * 0.5f);
            this.animations.DoorWalk(1);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, this.user.transform.forward.z * 1f);
            this.animations.DoorWalk(2);
        }

        if (this.user.collision != null)
            this.user.collision.enabled = false;


        yield return new WaitForSeconds(0.1f);

        /*if (this.closeSfx != null)
            this.closeSfx.Play();*/

        if (this.currentDoor != null)
            this.currentDoor.doorLid.transform.localEulerAngles = new Vector3(0f, 38f, 0f);

        if (this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, this.user.transform.forward.z * 1f);
            this.animations.DoorWalk(0);
        }

        yield return new WaitForSeconds(0.1f);

        if (this.currentDoor != null)
            this.currentDoor.doorLid.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        this.Disappear(true);





        yield return new WaitForSeconds(0.3f);

        if (this.currentDoor != null)
        {
            this.currentDoor.gameObject.SetActive(false);
        }


        yield return new WaitForSeconds(0.3f);

        this.TeleportDoor();

        yield return new WaitForSeconds(0.1f);

        if (this.currentDoor != null)
        {
            this.currentDoor.gameObject.SetActive(true);
        }


        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }*/


        yield return new WaitForSeconds(0.2f);

        /*if (this.openSfx != null)
            this.openSfx.Play();*/

        if (this.closeSfx != null)
            this.closeSfx.Play();

        yield return new WaitForSeconds(0.1f);

        if (this.currentDoor != null)
        {
            this.currentDoor.doorLid.transform.localEulerAngles = new Vector3(0f, 110f, 0f);

            this.currentDoor.ActivateHitbox();
        }

        if (this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, this.user.transform.forward.z * 1f);
            this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

            this.animations.DoorWalk(3);
        }

        this.Disappear(false);

        if (this.user.collision != null)
            this.user.collision.enabled = false;

        yield return new WaitForSeconds(0.3f);


        if (this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, this.user.transform.forward.z * 0.5f);
            //this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

            this.animations.DoorWalk(1);
        }

        if (this.user.collision != null)
            this.user.collision.enabled = true;


        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
        {
            this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, 0f);
            //this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

            this.animations.DoorWalk(2);
        }

        /*if (this.closeSfx != null)
            this.closeSfx.Play();*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
        {
            //this.animations.body.localPosition = new Vector3(0f, this.animations.defaultYPos, 0f);
            //this.animations.body.localEulerAngles = new Vector3(0f, this.user.transform.forward.z * 90f, 0f);

            this.animations.DoorWalk(0);
        }

        if (this.currentDoor != null)
            this.currentDoor.doorLid.transform.localEulerAngles = new Vector3(0f, 0f, 0f);


        /*if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }*/

        yield return new WaitForSeconds(0.1f);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.LookAtTarget();

        this.user.rb.isKinematic = false;



        yield return new WaitForSeconds(0.3f);

        if (this.currentDoor != null)
        {
            this.currentDoor.gameObject.SetActive(false);
            this.currentDoor = null;
        }

        //this.Disappear(false);

        /*if (this.animations != null)
            this.animations.SetDefaultPose();

        this.user.rb.isKinematic = false;*/

        yield return new WaitForSeconds(0.1f);

        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }
    public override void Stop()
    {
        base.Stop();

        this.user.rb.isKinematic = false;

        if(this.currentDoor != null)
        {
            this.currentDoor.gameObject.SetActive(false);
            this.currentDoor = null;
        }

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(true);

        if (!this.user.dead)
            this.Disappear(false);


        if (GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            GameManager.Instance.gameCamera.lockCamera = false;
        }


        this.onGoing = false;
        this.user.attackStuns.Remove(this.gameObject);
    }

    public void Disappear(bool disappear = true)
    {
        /*if (this.user.rb != null)
            this.user.rb.isKinematic = disappear;*/

        if (this.user.collision != null)
            this.user.collision.enabled = !disappear;

        if (this.animations != null)
            this.animations.body.gameObject.SetActive(!disappear);

        if (this.user.hitboxes != null)
            this.user.hitboxes.gameObject.SetActive(!disappear);
    }

    public void TeleportDoor()
    {
        if(GameManager.Instance != null)
        {
            float xMaxPos = 7f;

            if (GameManager.Instance.gameMode == 1)
                xMaxPos = 9f;

            if (this.user.movement != null && GameManager.Instance.gameCamera != null && this.currentDoor != null)
            {
                GameCamera cameraa = GameManager.Instance.gameCamera;

                if (this.user.movement.playerInput.moveInput.x > 0) //forward
                {
                    this.currentDoor.transform.position = new Vector3(cameraa.transform.position.x + xMaxPos, 0f, 1.2f);
                    this.user.transform.position = new Vector3(cameraa.transform.position.x + xMaxPos, 0f, 0f);



                    //this code below is to make it so the doors dont spawn at eachoter

                    if (this.user.tempOpponent != null &&
                        this.user.tempOpponent.characterId == this.user.characterId &&
                        this.user.tempOpponent.attacks.downSpecial is DoorTeleportAttack doorTeleport &&
                        doorTeleport.currentDoor != null &&
                        Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) < 3f &&
                        Vector3.Distance(new Vector3(0f, this.user.transform.position.y, 0f), new Vector3(0f, this.user.tempOpponent.transform.position.y, 0f)) < 4.15f)
                    {
                        //Debug.Log(Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)));

                        this.currentDoor.transform.position = new Vector3(this.user.tempOpponent.transform.position.x - 3f, 0f, 1.2f);
                        this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x - 3f, 0f, 0f);
                    }


                }
                else if (this.user.movement.playerInput.moveInput.x < 0) //backward
                {
                    this.currentDoor.transform.position = new Vector3(cameraa.transform.position.x - xMaxPos, 0f, 1.2f);
                    this.user.transform.position = new Vector3(cameraa.transform.position.x - xMaxPos, 0f, 0f);






                    if (this.user.tempOpponent != null &&
                        this.user.tempOpponent.characterId == this.user.characterId &&
                        this.user.tempOpponent.attacks.downSpecial is DoorTeleportAttack doorTeleport &&
                        doorTeleport.currentDoor != null && /*doorTeleport.currentDoor.gameObject.active &&*/
                        Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) < 3f &&
                        Vector3.Distance(new Vector3(0f, this.user.transform.position.y, 0f), new Vector3(0f, this.user.tempOpponent.transform.position.y, 0f)) < 4.15f)
                    {
                        Debug.Log(Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)));

                        this.currentDoor.transform.position = new Vector3(this.user.tempOpponent.transform.position.x + 3f, 0f, 1.2f);
                        this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x + 3f, 0f, 0f);
                    }


                }
                else //neutral
                {
                    this.currentDoor.transform.position = new Vector3(cameraa.transform.position.x, 0f, 1.2f);
                    this.user.transform.position = new Vector3(cameraa.transform.position.x, 0f, 0f);






                    if (this.user.tempOpponent != null &&
                        this.user.tempOpponent.characterId == this.user.characterId &&
                        this.user.tempOpponent.attacks.downSpecial is DoorTeleportAttack doorTeleport &&
                        doorTeleport.currentDoor != null &&
                        Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)) < 3f &&
                        Vector3.Distance(new Vector3(0f, this.user.transform.position.y, 0f), new Vector3(0f, this.user.tempOpponent.transform.position.y, 0f)) < 4.15f)
                    {
                        //Debug.Log(Vector3.Distance(new Vector3(this.user.transform.position.x, 0f, 0f), new Vector3(this.user.tempOpponent.transform.position.x, 0f, 0f)));


                        /*if (Vector3.Distance(new Vector3(0f, this.user.transform.position.y, 0f), new Vector3(0f, this.user.tempOpponent.transform.position.y, 0f)) < 4.15f) 
                        { 

                        }*/

                        float xOffset = 3f;

                        if (this.user.transform.position.x > 0f)
                        {
                            xOffset = -3f;
                        }

                        this.currentDoor.transform.position = new Vector3(this.user.tempOpponent.transform.position.x + xOffset, 0f, 1.2f);
                        this.user.transform.position = new Vector3(this.user.tempOpponent.transform.position.x + xOffset, 0f, 0f);
                    }
                }
            }
        }
        
    }
}