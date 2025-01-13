using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTests : MonoBehaviour
{

    public TempPlayerAnimations animations;

    public GameObject objectoToEnable;

    public ObjectScaleLerp objectToScale;

    public GameObject startParticle;

    public int animId;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.animId == 0)
        {
            this.StartCoroutine(this.Test1());
        }
        else if (this.animId == 1)
        {
            //this.StartCoroutine(this.DiscoDance());
        }
        else
        {
            Debug.Log("dance id does not exist");
        }
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }


    private IEnumerator Test1()
    {
        if (this.animations != null)
        {
            float animSpeed = 0.2f;

            if (this.startParticle != null)
            {
                GameObject startParticlePrefab = this.startParticle;
                startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            }

            if (this.animations != null)
                this.animations.StupidDance(1);

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.StupidDance(0);

            

            yield return new WaitForSeconds(0.4f);

            if (this.animations != null)
                this.animations.StupidDance(1);

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.StupidDance(2);

            if (this.objectToScale != null)
                this.objectToScale.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.45f);

            if (this.objectToScale != null)
                this.objectToScale.ScaleDown2(0.05f, true);

            yield return new WaitForSeconds(0.1f);
            

            if (this.animations != null)
                this.animations.StupidDance(1);

            

            yield return new WaitForSeconds(0.05f);

            if (this.animations != null)
                this.animations.SetDefaultPose();

            yield return new WaitForSeconds(1f);

            this.StartCoroutine(this.Test1());
        }


    }
}
