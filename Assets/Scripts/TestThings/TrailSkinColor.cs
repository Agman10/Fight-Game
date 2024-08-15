using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSkinColor : MonoBehaviour
{
    public ParticleSystem[] trails;
    public CharacterSkinTest skin;

    public int trailColorIndex = 0;

    public ParticleSystem trail1;
    public ParticleSystem trail2;

    public int trailColorIndex2 = 0;
    //public Gradient gradient;
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
        /*if(this.trails.Length >= 1 && this.skin != null && this.skin.skin != null *//*&& this.skin.skin.colors.Length >= this.trailColorIndex*//*)
        {
            foreach (ParticleSystem trail in this.trails)
            {
                //trail.trails.colorOverLifetime.color = this.skin.skin.colors[0];
                //trail.trails.lifetime = 1f;
                trail.startColor = this.skin.skin.colors[this.trailColorIndex];
            }
        }*/

        //MAKE IT BETTER AND MORE CUSTOMIZABLE
        
        if(this.skin != null && this.skin.skin != null)
        {
            if (this.trail1 != null)
                this.trail1.startColor = this.skin.skin.colors[this.trailColorIndex];

            if (this.trail2 != null)
                this.trail2.startColor = this.skin.skin.colors[this.trailColorIndex2];
        }
    }
}
