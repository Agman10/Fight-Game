using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSpellProjectile : MonoBehaviour
{
    public TestPlayer belongsTo;
    public GameObject model;
    private Collider collision;

    public float sleepTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        this.collision = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null)
        {
            if (this.belongsTo == null || player != this.belongsTo)
            {
                player.sleepLogic.Sleep(this.sleepTime);

                /*if (this.hitbox != null)
                    this.hitbox.gameObject.SetActive(false);*/

                /*if (this.collision != null)
                    this.collision.enabled = false;*/

                //this.Disable();

                //Debug.Log("test");
            }
        }
    }
}
