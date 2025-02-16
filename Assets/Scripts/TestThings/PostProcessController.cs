using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    public static PostProcessController Instance;
    //public PostProcessVolume volume;
    public Volume volume;


    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;

        this.volume = this.GetComponent<Volume>();
    }
    public void TurnBlackAndWhite()
    {
        if(this.volume != null)
        {
            if (this.volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.saturation.value = -100f;
                //colorAdjustments.contrast.value = 50f;
            }
        }
        
    }

    public void ReturnColors()
    {
        if(this.volume != null)
        {
            if (this.volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.saturation.value = 0f;
                //colorAdjustments.contrast.value = 0f;
            }
        }
        
    }
}
