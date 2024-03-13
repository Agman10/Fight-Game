using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardDayAndNight : MonoBehaviour
{
    public RandomSkybox skyboxLogic;

    public GameObject[] nightCreatures;
    public GameObject[] dayCreatures;
    public ZombieBurn zombie;

    private void OnEnable()
    {
        if (this.skyboxLogic != null)
        {
            this.skyboxLogic.OnSkyboxChanged += this.SkyboxChange;
            this.SkyboxChange(this.skyboxLogic.currentSkybox);
        }
    }
    private void OnDisable()
    {
        if (this.skyboxLogic != null)
            this.skyboxLogic.OnSkyboxChanged -= this.SkyboxChange;
    }

    public void SkyboxChange(int skyboxId)
    {
        if (skyboxId == 2 || skyboxId == 3 || skyboxId == 6 || skyboxId == 7 || skyboxId == 8)
        {
            foreach (GameObject nightCreature in this.nightCreatures)
            {
                if (nightCreature != null)
                    nightCreature.SetActive(true);
            }

            foreach (GameObject dayCreature in this.dayCreatures)
            {
                if (dayCreature != null)
                    dayCreature.SetActive(false);
            }


            if (this.zombie != null && this.zombie.burning)
                this.zombie.StopBurning();
        }
        else
        {
            foreach (GameObject nightCreature in this.nightCreatures)
            {
                if (nightCreature != null)
                    nightCreature.SetActive(false);
            }

            foreach (GameObject dayCreature in this.dayCreatures)
            {
                if (dayCreature != null)
                    dayCreature.SetActive(true);
            }


            if (this.zombie != null && !this.zombie.burning)
                this.zombie.BurnContinue();
        }
    }
}
