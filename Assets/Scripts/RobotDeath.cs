using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDeath : MonoBehaviour
{
    public TestPlayer player;

    public GameObject explosion;

    public Renderer eyeLampOff;
    public Renderer eyeLampOn;

    public AudioSource explodeSfx;
    //public AudioSource explodeSfx2;
    private void OnEnable()
    {
        if (this.player != null)
        {
            this.player.OnDeath += this.Explode;
            this.player.OnReset += this.CancelExplosion;
        }


        //this.StartCoroutine(this.TurnEyeDarkCoroutine());
        this.TurnLampOn(true);
    }
    private void OnDisable()
    {
        if (this.player != null)
        {
            this.player.OnDeath -= this.Explode;
            this.player.OnReset -= this.CancelExplosion;
        }
        this.StopAllCoroutines();
    }
    public void Explode()
    {
        this.StartCoroutine(this.ExplodeCoroutine());


        this.TurnLampOn(false);
    }

    public void CancelExplosion()
    {
        this.StopAllCoroutines();


        this.TurnLampOn(true);
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(3f);
        //yield return new WaitForSeconds(0.01f);

        if (this.explodeSfx != null)
            this.explodeSfx.Play();

        /*if (this.explodeSfx2 != null)
            this.explodeSfx2.Play();*/

        this.player.ragdoll.gameObject.SetActive(false);

        if (this.explosion != null && player.ragdoll.transform.position.y >= -5f)
        {
            GameObject explosionPrefab = this.explosion;
            //explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.player.ragdoll.transform.position.x, this.player.ragdoll.transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            //explosionPrefab.transform.localScale = explosionPrefab.transform.localScale * 1.5f;

            CharacterSkinTest skinTest = explosionPrefab.GetComponent<CharacterSkinTest>();
            if (skinTest != null)
            {
                //Debug.Log("has it");
                if(this.player != null && this.player.skin != null && this.player.skin.skin != null)
                {
                    skinTest.SetSkin(this.player.skin.skin);
                }
            }
            /*else
            {
                Debug.Log("dont has it");
            }*/
        }
    }

    private IEnumerator TurnEyeDarkCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        if (this.eyeLampOff != null && this.eyeLampOff.materials.Length >= 1 && this.eyeLampOff.materials[0] != null)
        {
            this.eyeLampOff.material.SetColor("_EmissionColor", Color.black);
        }
    }

    public void TurnLampOn(bool turnOn = true)
    {
        if (turnOn)
        {
            if (this.eyeLampOn != null)
                this.eyeLampOn.gameObject.SetActive(true);

            if (this.eyeLampOff != null)
                this.eyeLampOff.gameObject.SetActive(false);
        }
        else
        {
            if (this.eyeLampOn != null)
                this.eyeLampOn.gameObject.SetActive(false);

            if (this.eyeLampOff != null)
                this.eyeLampOff.gameObject.SetActive(true);

            if (this.player != null && this.player.animations != null)
                this.player.animations.SetEyes(0);

            /*if (this.eyeLampOff != null && this.eyeLampOff.materials.Length >= 1 && this.eyeLampOff.materials[0] != null)
            {
                this.eyeLampOff.material.SetColor("_EmissionColor", Color.black);
            }*/
        }
    }
}
