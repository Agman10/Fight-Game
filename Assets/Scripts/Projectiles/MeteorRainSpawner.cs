using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRainSpawner : MonoBehaviour
{
    public TestPlayer owner;
    public ParticleSystem flameEffect;
    public TestHitbox hitbox;
    public MeteorSpawner meteorSpawner;
    public ArmageddonMeteor meteor;
    private float startPosY;
    private float startPosX;
    public float targetPosX;
    private void OnEnable()
    {
        this.startPosX = this.transform.position.x;
        this.startPosY = this.transform.position.y;
        this.SetTargetPos();
        this.StartCoroutine(this.MoveToTargetPositionCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    public void SetTargetPos()
    {
        float maxXPos = 11.5f;

        this.targetPosX = this.startPosX + (this.transform.forward.z * 6.2f);


        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.gameMode == 1)
                maxXPos = 6.5f;
            else if (GameManager.Instance.gameCamera != null)
                maxXPos = GameManager.Instance.gameCamera.maxX + 5f;
        }

        if (this.targetPosX >= maxXPos)
            this.targetPosX = maxXPos;
        else if (this.targetPosX <= -maxXPos)
            this.targetPosX = -maxXPos;

        //Debug.Log(this.targetPosX);

        //Debug.Log(this.targetPosX - this.startPosX);

        float testFloat = this.targetPosX - this.startPosX;

        if (this.hitbox != null)
        {
            this.hitbox.horizontalKnockback = this.hitbox.horizontalKnockback * (testFloat / 6.2f);

            //Debug.Log(this.hitbox.horizontalKnockback * (testFloat / 6.2f));
        }


    }

    public void MoveToTargetPosition()
    {

    }
    private IEnumerator MoveToTargetPositionCoroutine()
    {
        float currentTime = 0;
        float duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            this.transform.position = new Vector3(Mathf.Lerp(this.startPosX, this.targetPosX, currentTime / duration), Mathf.Lerp(this.startPosY, 12f, currentTime / duration), 0f);
            yield return null;
        }

        if(this.hitbox != null)
        {
            this.hitbox.gameObject.SetActive(false);
        }

        if(this.flameEffect != null)
        {
            this.flameEffect.Stop();
        }

        this.SpawnMetorSpawner();

        this.StartCoroutine(this.DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }


    public void SetOwner(TestPlayer owner)
    {
        if (owner != null)
            this.owner = owner;

        if (this.hitbox != null)
            this.hitbox.belongsTo = owner;
    }
    public void SpawnMetorSpawner()
    {
        if (this.meteorSpawner != null)
        {
            MeteorSpawner meteorSpawnerPrefab = this.meteorSpawner;
            meteorSpawnerPrefab = Instantiate(meteorSpawnerPrefab, new Vector3(this.transform.position.x, 0f, 0), Quaternion.Euler(0f, 0f, 0f));
            meteorSpawnerPrefab.meteor = this.meteor;
            meteorSpawnerPrefab.SetOwner(this.owner);
        }
    }
}
