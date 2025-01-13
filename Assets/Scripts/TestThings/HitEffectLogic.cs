using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectLogic : MonoBehaviour
{
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoHitEffect(float yPos, float forwardZ, float zPos = 0f)
    {
        //this.StartCoroutine(this.DoHitEffectCoroutine(yPos, forwardZ));

        if (this.hitEffect != null)
        {
            GameObject hitEffectPrefab = this.hitEffect;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            //hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (this.transform.forward.z * 0.6f), yPos, 0f), Quaternion.Euler(0, 0, 0));
            hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (forwardZ * 0.6f), yPos, zPos), Quaternion.Euler(0, 0, 0));
        }
    }

    private IEnumerator DoHitEffectCoroutine(float yPos, float forwardZ)
    {
        yield return new WaitForSeconds(0.05f);
        if (this.hitEffect != null)
        {
            GameObject hitEffectPrefab = this.hitEffect;
            //startParticlePrefab = Instantiate(startParticlePrefab, new Vector3(this.user.transform.position.x, this.user.transform.position.y + 2f, -0.8f), Quaternion.Euler(0, 0, 0));
            hitEffectPrefab = Instantiate(hitEffectPrefab, new Vector3(this.transform.position.x + (forwardZ * 0.6f)/*(this.transform.forward.z * 0.6f)*/, yPos, 0f), Quaternion.Euler(0, 0, 0));
        }
    }
}
