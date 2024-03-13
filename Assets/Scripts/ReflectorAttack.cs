using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAttack : MonoBehaviour
{
    public TestPlayer user;
    public TempPlayerAnimations animations;

    public GameObject reflector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateReflector()
    {
        if (this.user != null)
        {
            this.user.AddStun(0.2f, true);
            //this.StartCoroutine(this.KickUppercutCoroutine());
            this.StartCoroutine(this.ReflectorCoroutine());
        }
    }

    IEnumerator ReflectorCoroutine()
    {
        if (this.reflector != null)
            this.reflector.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        if (this.reflector != null)
            this.reflector.gameObject.SetActive(false);
    }
}
