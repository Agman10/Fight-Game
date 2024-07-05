using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fire : MonoBehaviour
{
    public TestPlayer belongsTo;
    public TestHitbox hitbox;
    public VisualEffect flame;
    public float lifeTime = 1.5f;

    public float moveSpeed = 0f;

    public AudioSource fireSfx;
    void OnEnable()
    {
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(true);

        this.StartCoroutine(this.DisableTimerCoroutine());

        if (this.fireSfx != null)
            this.fireSfx.Play();
    }
    void OnDisable()
    {
        this.StopAllCoroutines();
    }

    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.hitbox != null)
                this.hitbox.belongsTo = player;
        }
    }

    IEnumerator DisableTimerCoroutine()
    {
        if (this.moveSpeed == 0f)
        {
            yield return new WaitForSeconds(this.lifeTime);
        }
        else
        {
            //WHEN THERE IS A DEDICATED FLOOR LAYER THIS WONT BE NEEDED THEN THE FIRE WILL USE RIGIDBODY AND ONLY COLIDE WITH FLOOR

            float waitTime = this.lifeTime;
            float fallSpeed = -5f;
            while (waitTime > 0f)
            {
                waitTime -= Time.deltaTime;

                //if (Mathf.Abs(this.transform.position.y) <= 0.5f)
                if (this.transform.position.y > 0.4f && this.transform.position.y <= 0.5f)
                {
                    fallSpeed = 0f;
                    this.transform.position = new Vector3(this.transform.position.x, 0.5f, 0f);
                }
                else
                {
                    fallSpeed = -5f;
                }


                this.transform.Translate(new Vector3(this.moveSpeed * Time.deltaTime, fallSpeed * Time.deltaTime, 0f));



                yield return null;
            }
        }
        

        //yield return new WaitForSeconds(this.lifeTime);
        
        if (this.flame != null)
            this.flame.Stop();


        yield return new WaitForSeconds(0.25f);
        if (this.hitbox != null)
            this.hitbox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
        /*if (!this.destroy)
            this.gameObject.SetActive(false);
        else
            Destroy(this.gameObject);*/
    }
}
