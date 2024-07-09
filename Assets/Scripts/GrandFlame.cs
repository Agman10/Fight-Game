using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrandFlame : MonoBehaviour
{
    public VisualEffect fire;
    public TestHitbox hitBox;
    public TestHitbox hitBox2;
    public MoveAndTeleportObject[] fireBalls;
    public TestHitbox[] fireBallHitboxes;
    public TestPlayer owner;

    public AudioSource flameSfx;
    private void OnEnable()
    {
        if (this.hitBox != null)
            this.hitBox.gameObject.SetActive(true);

        if (this.flameSfx != null)
            this.flameSfx.Play();
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        foreach(MoveAndTeleportObject fireBall in this.fireBalls)
        {
            if (fireBall != null)
            {
                fireBall.disableOnLimit = false;
                fireBall.gameObject.SetActive(true);
            }
                
        }
        if (this.hitBox2 != null)
            this.hitBox2.gameObject.SetActive(false);

        foreach (TestHitbox fireBallHitbox in this.fireBallHitboxes)
        {
            if (fireBallHitbox != null)
            {
                fireBallHitbox.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }

        if (this.flameSfx != null)
            this.flameSfx.Stop();
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        if (this.fire != null)
            this.fire.Stop();
        foreach (MoveAndTeleportObject fireBall in this.fireBalls)
        {
            if (fireBall != null)
            {
                fireBall.disableOnLimit = true;
                //fireBall.gameObject.SetActive(true);
            }

        }

        this.StartCoroutine(this.StopCoroutine());
    }

    private IEnumerator StopCoroutine()
    {
        //yield return new WaitForSeconds(0.4f);
        if (this.hitBox != null)
            this.hitBox.gameObject.SetActive(false);

        if (this.hitBox2 != null)
            this.hitBox2.gameObject.SetActive(true);

        foreach (TestHitbox fireBallHitbox in this.fireBallHitboxes)
        {
            if (fireBallHitbox != null)
            {
                fireBallHitbox.transform.localEulerAngles = new Vector3(0f, 180, 0f);
            }
        }

        float currentTime = 0;
        float duration = 0.2f;
        float targetVolume = 0f;
        float startVolume = 0.2f;
        if (this.flameSfx != null)
            startVolume = this.flameSfx.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if (this.flameSfx != null)
                this.flameSfx.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        if (this.flameSfx != null)
            this.flameSfx.Stop();
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.2f);
        //yield return new WaitForSeconds(0.4f);

        if (this.hitBox2 != null)
            this.hitBox2.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
    public void SetOwner(TestPlayer user)
    {
        if(user != null)
        {
            this.owner = user;
            if (this.hitBox != null)
                this.hitBox.belongsTo = user;
            if (this.hitBox2 != null)
                this.hitBox2.belongsTo = user;

            foreach (TestHitbox fireBallHitbox in this.fireBallHitboxes)
            {
                if (fireBallHitbox != null)
                {
                    fireBallHitbox.belongsTo = user;
                }
            }
        }
    }
}
