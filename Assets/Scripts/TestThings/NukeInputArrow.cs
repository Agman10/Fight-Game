using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeInputArrow : MonoBehaviour
{
    public GameObject currentArrow;
    public GameObject upComingArrow;
    public GameObject finisedArrow;
    
    public void SetArrow(int stage)
    {
        if(this.currentArrow != null && this.upComingArrow != null && this.finisedArrow != null)
        {
            if (stage == 0)
            {
                this.currentArrow.SetActive(true);
                this.upComingArrow.SetActive(false);
                this.finisedArrow.SetActive(false);
            }
            else if (stage == 1)
            {
                this.currentArrow.SetActive(false);
                this.upComingArrow.SetActive(true);
                this.finisedArrow.SetActive(false);
            }
            else if (stage == 2)
            {
                this.currentArrow.SetActive(false);
                this.upComingArrow.SetActive(false);
                this.finisedArrow.SetActive(true);
            }
        }
        
    }
}
