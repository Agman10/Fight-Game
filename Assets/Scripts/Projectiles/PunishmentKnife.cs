using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentKnife : MonoBehaviour
{
    public TestPlayer belongsTo;
    public GameObject hitEffect;
    public Transform hitEffectTransform;

    public GameObject model;
    private Collider collision;

    private bool disabled;

    
    public CharacterSkinTest skin;

    public float startDelay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //this.StartCoroutine(this.MoveCoroutine());
        this.StartCoroutine(this.AppearCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!this.disabled)
            this.transform.Translate(new Vector3(10f * Time.deltaTime, 0f, 0f));*/
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null)
        {
            if (this.belongsTo == null || player != this.belongsTo)
            {
                this.HitEnemy();

                player.TakeDamage(player.transform.position, 1.25f);
            }
        }

        /*if (other.tag == "Player")
        {
            this.HitEnemy();
        }*/
    }

    public void HitEnemy()
    {
        this.disabled = true;

        if (this.collision != null)
            this.collision.enabled = false;

        /*if (this.model != null)
            this.model.gameObject.SetActive(false);*/

        if (this.hitEffect != null)
        {
            GameObject hitEffectPrefab = this.hitEffect;
            if (this.hitEffectTransform != null)
                hitEffectPrefab = Instantiate(hitEffectPrefab, this.hitEffectTransform.position, Quaternion.Euler(0, 0, 0));
            else
                hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (this.transform.forward.z * 0.45f), this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));

        }

        this.StartCoroutine(this.DisableCoroutine());
    }


    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.4f);

        this.gameObject.SetActive(false);
    }

    private IEnumerator AppearCoroutine()
    {
        float startScale = 1.3f;
        this.model.transform.localScale = new Vector3(startScale, startScale, startScale);

        yield return new WaitForSeconds(0.02f);

        if (this.model != null)
            this.model.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.05f);

        if (this.model != null)
            this.model.gameObject.SetActive(true);


        float currentTime = 0;
        float duration = 0.1f;

        
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            if(this.model != null)
            {
                this.model.transform.localScale = new Vector3(
                Mathf.Lerp(startScale, 1f, currentTime / duration),
                Mathf.Lerp(startScale, 1f, currentTime / duration),
                Mathf.Lerp(startScale, 1f, currentTime / duration));
            }
            yield return null;
        }

        //this.StartCoroutine(this.MoveCoroutine());
    }

    public void Move()
    {
        this.StartCoroutine(this.MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        //yield return new WaitForSeconds(this.startDelay);

        float startXPos = this.transform.localPosition.x;
        float endXPos = this.transform.localPosition.x;


        float currentTime = 0;
        float duration = 0.1f;
        //float duration = 0.1f;
        //float targetVolume = 0.1f;
        /*float targetPosition = 3.5f;
        float start = this.transform.position.y;*/
        while (currentTime < duration && !this.disabled)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(Mathf.Lerp(startXPos, -0.5f, currentTime / duration), 0f, 0f);
            yield return null;
        }

        if(!this.disabled)
            this.gameObject.SetActive(false);
    }



    public void SetOwner(TestPlayer player)
    {
        if (player != null)
        {
            this.belongsTo = player;

            if (this.skin != null && player.skin != null && player.skin.skin != null)
                this.skin.SetSkin(player.skin.skin);
        }
    }
}
