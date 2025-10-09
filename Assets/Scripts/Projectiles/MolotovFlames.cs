using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFlames : MonoBehaviour
{
    public TestPlayer owner;
    public ParticleSystem flameEffect;
    public TestHitbox hitbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetOwner(TestPlayer user)
    {
        if (user != null)
        {
            this.owner = user;

            if (this.hitbox != null)
                this.hitbox.belongsTo = user;
        }
    }
}
