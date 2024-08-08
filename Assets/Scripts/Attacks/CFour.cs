using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFour : MonoBehaviour
{
    public bool isActive;
    public Explosion explosion;
    public TestPlayer belongsTo;
    public CFourAttack cFourAttackOwner;

    private Collider c4Collider;
    private Rigidbody rb;

    public GameObject model;
    public bool isP2 = false;

    public float activationTime = 0.3f;

    public CharacterSoundEffect explosionSfx;
    [Space]

    [Space]
    public MeshRenderer lamp;
    public Material activeMaterial;
    public Material inactiveMaterial;

    [Space]
    public MeshRenderer lampP2;
    public Material activeMaterialP2;
    public Material inactiveMaterialP2;

    [Space]
    public float explosionSize = 1f;

    [Space]
    public float innerDamage = 15f;
    public float innerKnockbackX = 900f;
    public float innerKnockbackY = 900f;
    public float innerSuperCharge = 3.75f;
    public float innerStun = 0.2f;

    [Space]
    public float outerDamage = 5f;
    public float outerKnockbackX = 300f;
    public float outerKnockbackY = 300f;
    public float outerSuperCharge = 1.25f;
    public float outerStun = 0.2f;
    // Start is called before the first frame update

    void Awake()
    {
        this.c4Collider = GetComponent<Collider>();
        this.rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if (this.model != null)
            this.model.SetActive(true);

        if (this.c4Collider != null)
            this.c4Collider.enabled = true;

        if (this.rb != null)
            this.rb.isKinematic = false;

        /*this.SetPlayerColor(this.isP2);
        this.StartCoroutine(this.ActivateCoroutine());*/
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        if (this.cFourAttackOwner != null && this.cFourAttackOwner.activeCFour == this)
            this.cFourAttackOwner.activeCFour = null;
    }

    private void OnDestroy()
    {
        this.StopAllCoroutines();
        if (this.cFourAttackOwner != null && this.cFourAttackOwner.activeCFour == this)
            this.cFourAttackOwner.activeCFour = null;
    }

    [ContextMenu("Detonate")]
    public void Detonate()
    {
        if (this.isActive)
        {
            this.Explode();
        }
    }

    
    public void Explode()
    {
        this.MakeActive(false);

        if (this.explosion != null)
        {
            Explosion explosionPrefab = this.explosion;

            explosionPrefab = Instantiate(explosionPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.Euler(0, 0, 0));

            if (this.belongsTo != null)
                explosionPrefab.SetOwner(this.belongsTo);

            explosionPrefab.SetDamage(this.innerDamage, this.outerDamage, this.innerKnockbackX, this.innerKnockbackY, this.outerKnockbackX, this.outerKnockbackY, this.innerSuperCharge, this.outerSuperCharge, this.innerStun, this.outerStun, true);

            explosionPrefab.SetSize(this.explosionSize);
            
        }

        //this.gameObject.SetActive(false);

        if (this.model != null)
            this.model.SetActive(false);

        if (this.c4Collider != null)
            this.c4Collider.enabled = false;

        if (this.rb != null)
            this.rb.isKinematic = true;

        if (this.cFourAttackOwner != null)
            this.cFourAttackOwner.activeCFour = null;

        this.explosionSfx.PlaySound();

        this.StartCoroutine(this.DisableCoroutine());
    }

    IEnumerator ActivateCoroutine()
    {
        /*if(this.transform.position.y > 0f)
        {
            float currentTime = 0f;
            float duration = 0.2f;
            float targetPosition = 0f;
            float start = this.transform.position.y;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(start, targetPosition, currentTime / duration), 0f);

                yield return null;
            }
            this.transform.position = new Vector3(this.transform.position.x, 0f, 0f);
        }*/
        

        yield return new WaitForSeconds(this.activationTime);
        this.MakeActive(true);
    }

    public void ActivateCFour()
    {
        this.SetPlayerColor(this.isP2);
        this.StartCoroutine(this.ActivateCoroutine());
    }

    public void SetOwner(TestPlayer player, CFourAttack cFourAttack)
    {
        if (player != null)
        {
            this.belongsTo = player;
        }

        if (cFourAttack != null)
        {
            this.cFourAttackOwner = cFourAttack;
        }
    }

    public void SetPlayerColor(bool player2 = false)
    {
        if (this.lamp != null)
            this.lamp.gameObject.SetActive(!player2);

        if (this.lampP2 != null)
            this.lampP2.gameObject.SetActive(player2);
    }


    public void MakeActive(bool active)
    {
        this.isActive = active;

        if (this.lamp != null)
        {
            if (active && this.activeMaterial != null)
                this.lamp.material = this.activeMaterial;
            else if (!active && this.inactiveMaterial != null)
                this.lamp.material = this.inactiveMaterial;
        }

        if (this.lampP2 != null)
        {
            if (active && this.activeMaterialP2 != null)
                this.lampP2.material = this.activeMaterialP2;
            else if (!active && this.inactiveMaterialP2 != null)
                this.lampP2.material = this.inactiveMaterialP2;
        }

    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        TestHitbox hitbox = other.GetComponent<TestHitbox>();
        if (hitbox != null && hitbox.instaKill)
        {
            this.gameObject.SetActive(false);
        }
    }
}
