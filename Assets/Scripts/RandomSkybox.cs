using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    public bool randomSkybox = true;
    public bool randomStage = true;
    public Light directionalLight;

    public bool rgbLightning = false;
    private float numbb;


    [Space]
    public Material[] skyboxes;
    public int currentSkybox;
    [Space]
    public GameObject[] stages;
    public int currentStage;
    [Space]
    public GameObject sun;
    public GameObject moon;
    public GameObject hellMoon;
    public GameObject earth;

    public GameObject invisBounceWalls;
    [Space]
    public AudioSource[] songs;
    public int currentMusic;

    public System.Action<int> OnSkyboxChanged;
    /*[Space]
    public RandomTest[] randomTests;*/

    // Start is called before the first frame update
    void Start()
    {

        if (this.randomSkybox)
            this.LoadRandomSkybox();
        else 
            this.SetSkybox(this.currentSkybox);

        if (this.randomStage)
            this.LoadRandomStage();
        else if (this.currentStage > 0 && this.currentStage <= this.stages.Length - 1)
            this.SetStage(this.currentStage);

        //this.SetStage(2);
        //Debug.Log(this.stages.Length);
        //this.PullRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rgbLightning)
        {
            this.numbb += Time.deltaTime * 0.5f;
            if (this.numbb >= 1)
                this.numbb = 0;
            if (this.directionalLight != null)
                this.directionalLight.color = Color.HSVToRGB(this.numbb, 0.16f, 1f);

            /*if (this.directionalLight != null)
                this.directionalLight.color = Color.HSVToRGB(this.numbb, 0.26f, 1f);*/

            //RenderSettings.ambientLight = Color.HSVToRGB(this.numbb, 0.01f, 0.71f);

            //RenderSettings.ambientLight = Color.HSVToRGB(this.numbb, 0.05f, 0.51f);
            //RenderSettings.ambientLight = Color.HSVToRGB(this.numbb, 0.3f, 0.41f);

            RenderSettings.ambientLight = Color.HSVToRGB(this.numbb, 0.1f, 0.51f);
        }
    }

    public void LoadRandomSkybox()
    {
        float number = Random.Range(1, 101);
        //Debug.Log("skybox: " + number);

        if (number <= 40)
        {
            this.SetSkybox(0);
        }
        else if (number > 40 && number <= 60)
        {
            this.SetSkybox(1);
        }
        else if (number > 60 && number <= 80)
        {
            this.SetSkybox(2);
            /*if (this.moon != null)
                this.moon.SetActive(true);*/
        }
        else if (number > 80 && number <= 90)
        {
            this.SetSkybox(3);
        }
        else
        {
            this.SetSkybox(4);
        }
    }

    public void LoadRandomStage()
    {
        float number = Random.Range(1, 101);
        //Debug.Log(Random.Range(2, 4));

        if (number <= 20)
        {
            this.SetStage(0);
            this.SetMusic(0);
        }
        else if (number > 20 && number <= 35)
        {
            this.SetStage(1);
            this.SetMusic(1);
        }
        else if (number > 35 && number <= 40)
        {
            this.SetStage(2);
            this.SetMusic(2);
        }
        else if (number > 40 && number <= 55)
        {
            this.SetStage(3);
            this.SetMusic(3);
            this.SetSkybox(Random.Range(2, 4));

            float graveyardSkyboxNumber = Random.Range(1, 101);
            if (graveyardSkyboxNumber <= 2)
                this.SetSkybox(0);

            /*float graveyardSkyboxNumber = Random.Range(1, 101);
            if (graveyardSkyboxNumber <= 45)
                this.SetSkybox(2);
            else if(graveyardSkyboxNumber > 45 && graveyardSkyboxNumber <= 90)
                this.SetSkybox(3);
            else
                this.SetSkybox(7);*/

            RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
        }
        else if (number > 55 && number <= 65)
        {
            this.SetStage(4);
            this.SetMusic(Random.Range(4, 6));
            //this.SetSkybox(Random.Range(5, 7));

            float skromeNumber = Random.Range(1, 101);
            if (skromeNumber <= 75)
                this.SetSkybox(5);
            else
                this.SetSkybox(6);
        }
        else if (number > 65 && number <= 70)
        {
            this.SetStage(5);
            this.SetMusic(8);
        }
        else if(number > 70 && number <= 75)
        {
            this.SetStage(6);
            this.SetMusic(9);
        }
        else if(number > 75 && number <= 76)
        {
            this.SetStage(7);
            this.SetMusic(6);
            this.SetSkybox(0);
            /*if (this.moon != null)
                this.moon.SetActive(false);*/
        }
        else if(number > 76 && number <= 86)
        {
            this.SetStage(8);
            this.SetMusic(7);
            //this.SetSkybox(2);
            this.SetSkybox(7);
            /*float throneRoomSkyboxNumber = Random.Range(1, 101);
            if (throneRoomSkyboxNumber <= 50)
                this.SetSkybox(2);
            else
                this.SetSkybox(7);*/

            RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
        }
        else if (number > 86 && number <= 91)
        {
            this.SetStage(10);
            this.SetMusic(11);
            this.SetSkybox(8);
            /*if (this.moon != null)
                this.moon.SetActive(false);*/
            RenderSettings.ambientLight = new Color32(180, 135, 135, 255);
        }
        else if(number > 91 && number <= 96)
        {
            this.SetStage(11);
            this.SetMusic(12);
            //this.SetSkybox(0);

            float heavenSkyboxNumber = Random.Range(1, 101);
            if (heavenSkyboxNumber <= 70)
                this.SetSkybox(0);
            else if (heavenSkyboxNumber > 70 && heavenSkyboxNumber <= 90)
                this.SetSkybox(4);
            else
                this.SetSkybox(1);
        }
        else
        {
            this.SetStage(15);
            this.SetMusic(14);
            this.SetSkybox(2);
            if (this.moon != null)
                this.moon.SetActive(false);

            this.rgbLightning = true;

            //RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
            RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
            this.numbb = 0f;
        }
    }

    public void SetStage(int stageId)
    {
        if(this.stages.Length >= stageId + 1 && this.stages[stageId] != null)
        {
            //Debug.Log(this.stages.Length);
            for (int i = 0; i < this.stages.Length; i++)
            {
                /*if (this.stages[i] != null)
                    this.stages[i].SetActive(false);*/

                if (this.stages[i] != null)
                {
                    if (i == stageId)
                        this.stages[i].SetActive(true);
                    else
                        this.stages[i].SetActive(false);
                }
            }
            this.currentStage = stageId;
            RenderSettings.ambientLight = new Color32(180, 180, 180, 255);

            if (stageId == 12)
                Physics.gravity = new Vector3(0, -3.81f, 0);
            else
                Physics.gravity = new Vector3(0, -19.81f, 0);
            //this.stages[stageId].SetActive(true);
        }
    }
    public void SetSkybox(int skyboxId)
    {
        if (this.skyboxes.Length >= skyboxId + 1 && this.skyboxes[skyboxId] != null)
        {
            RenderSettings.skybox = this.skyboxes[skyboxId];
            this.currentSkybox = skyboxId;
            this.OnSkyboxChanged?.Invoke(skyboxId);
            //RenderSettings.ambientLight = new Color32(180, 180, 180, 255);

            if (this.moon != null)
            {
                if (skyboxId == 2 || skyboxId == 7)
                    this.moon.SetActive(true);
                else
                    this.moon.SetActive(false);
            }

            if (this.hellMoon != null)
            {
                if (skyboxId == 8)
                    this.hellMoon.SetActive(true);
                else
                    this.hellMoon.SetActive(false);
            }

            if (this.earth != null)
            {
                if (skyboxId == 9)
                    this.earth.SetActive(true);
                else
                    this.earth.SetActive(false);
            }

            if (this.directionalLight != null)
            {
                if(skyboxId == 1)
                    this.directionalLight.color = new Color32(255, 204, 174, 255);
                else if (skyboxId == 2)
                    this.directionalLight.color = new Color32(219, 214, 255, 255);
                else if (skyboxId == 3)
                    this.directionalLight.color = new Color32(214, 235, 255, 255);
                else if (skyboxId == 4)
                    this.directionalLight.color = new Color32(249, 214, 255, 255);
                else if (skyboxId == 6 || skyboxId == 7)
                    this.directionalLight.color = new Color32(235, 214, 255, 255);
                else if (skyboxId == 8)
                    this.directionalLight.color = new Color32(255, 215, 215, 255);
                else if (skyboxId == 9)
                    this.directionalLight.color = new Color32(214, 214, 255, 255);
                /*else if (skyboxId == 2 || skyboxId == 6 || skyboxId == 7)
                    this.directionalLight.color = new Color32(218, 179, 255, 255);*/
                else
                    this.directionalLight.color = new Color32(255, 244, 214, 255);
            }

        }
    }

    public void SetMusic(int musicId)
    {
        if (this.songs.Length >= musicId + 1 && this.songs[musicId] != null)
        {
            /*if (this.songs.Length >= currentMusic + 1 && this.songs[this.currentMusic] != null)
                this.songs[this.currentMusic].Stop();*/

            this.songs[musicId].Play();
            this.currentMusic = musicId;

        }
    }

    [ContextMenu("LoadNextSkybox")]
    public void LoadNextSkybox()
    {
        if(this.skyboxes.Length > 0)
        {
            if(this.currentSkybox >= this.skyboxes.Length - 1)
            {
                /*RenderSettings.skybox = this.skyboxes[0];
                this.currentSkybox = 0;*/
                this.SetSkybox(0);
            }
            else
            {
                /*RenderSettings.skybox = this.skyboxes[this.currentSkybox + 1];
                this.currentSkybox = this.currentSkybox + 1;*/

                this.SetSkybox(this.currentSkybox + 1);

            }
            if(this.moon != null)
            {
                if (this.currentSkybox == 2 || this.currentSkybox == 7)
                    this.moon.SetActive(true);
                else
                    this.moon.SetActive(false);
            }

            /*if (this.directionalLight != null)
            {
                if (this.currentSkybox == 2 || this.currentSkybox == 7)
                    this.directionalLight.color = new Color32(235, 214, 255, 255);
                else
                    this.directionalLight.color = new Color32(255, 244, 214, 255);
            }*/

        }
    }
    [ContextMenu("LoadNextStage")]
    public void LoadNextStage(bool previous = false)
    {
        if (this.stages.Length > 0)
        {
            if (!this.randomStage)
            {
                foreach (GameObject stage in this.stages)
                {
                    stage.SetActive(false);
                }
            }

            if (previous)
            {
                if (this.currentStage <= 0 /*this.stages.Length - 1*/)
                {
                    this.stages[this.stages.Length - 1].SetActive(true);
                    this.stages[this.currentStage].SetActive(false);
                    this.currentStage = this.stages.Length - 1;
                }
                else
                {
                    this.stages[this.currentStage - 1].SetActive(true);
                    this.stages[this.currentStage].SetActive(false);
                    this.currentStage = this.currentStage - 1;
                }
            }
            else
            {
                if (this.currentStage >= this.stages.Length - 1)
                {
                    this.stages[0].SetActive(true);
                    this.stages[this.currentStage].SetActive(false);
                    this.currentStage = 0;
                }
                else
                {
                    this.stages[this.currentStage + 1].SetActive(true);
                    this.stages[this.currentStage].SetActive(false);
                    this.currentStage = this.currentStage + 1;
                }
            }

            
        }
        RenderSettings.ambientLight = new Color32(180, 180, 180, 255);

        if (this.currentStage == 12)
            Physics.gravity = new Vector3(0, -3.81f, 0);
        else
            Physics.gravity = new Vector3(0, -19.81f, 0);

        this.rgbLightning = false;

        if (this.currentStage == 13)
        {
            if (this.invisBounceWalls != null)
                this.invisBounceWalls.gameObject.SetActive(true);
        }
        else
        {
            if (this.invisBounceWalls != null)
                this.invisBounceWalls.gameObject.SetActive(false);
        }

        foreach (AudioSource music in songs)
        {
            if (music != null)
                music.Stop();
        }
        switch (this.currentStage)
        {
            case 0:
                this.SetMusic(0);
                this.LoadRandomSkybox();
                break;
            case 1:
                this.SetMusic(1);
                break;
            case 2:
                this.SetMusic(2);
                break;
            case 3:
                this.SetMusic(3);
                this.SetSkybox(Random.Range(2, 4));

                float graveyardSkyboxNumber = Random.Range(1, 101);
                if (graveyardSkyboxNumber <= 2)
                    this.SetSkybox(0);

                /*float graveyardSkyboxNumber = Random.Range(1, 101);
                if (graveyardSkyboxNumber <= 45)
                    this.SetSkybox(2);
                else if (graveyardSkyboxNumber > 45 && graveyardSkyboxNumber <= 90)
                    this.SetSkybox(3);
                else
                    this.SetSkybox(7);*/

                RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
                break;
            case 4:
                this.SetMusic(Random.Range(4, 6));
                //this.SetSkybox(Random.Range(5, 7));
                float skromeNumber = Random.Range(1, 101);
                if (skromeNumber <= 75)
                    this.SetSkybox(5);
                else
                    this.SetSkybox(6);
                break;
            case 5:
                this.SetMusic(8);
                this.LoadRandomSkybox();
                break;
            case 6:
                this.SetMusic(9);
                break;
            case 7:
                this.SetMusic(6);
                this.SetSkybox(0);
                /*if (this.moon != null)
                    this.moon.SetActive(false);*/
                break;
            case 8:
                this.SetMusic(7);
                //this.SetSkybox(2);
                /*if (this.moon != null)
                    this.moon.SetActive(true);*/
                this.SetSkybox(7);

                /*float throneRoomSkyboxNumber = Random.Range(1, 101);
                if (throneRoomSkyboxNumber <= 50)
                    this.SetSkybox(2);
                else
                    this.SetSkybox(7);*/

                //RenderSettings.ambientLight = new Color32(60, 50, 130, 255);
                RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
                break;
            case 9:
                this.SetMusic(10);
                this.SetSkybox(2);
                if (this.moon != null)
                    this.moon.SetActive(false);
                RenderSettings.ambientLight = new Color32(125, 130, 180, 255);
                break;
            case 10:
                this.SetMusic(11);
                this.SetSkybox(8);
                /*if (this.moon != null)
                    this.moon.SetActive(false);*/
                RenderSettings.ambientLight = new Color32(180, 135, 135, 255);
                break;
            case 11:
                this.SetMusic(12);
                //this.SetSkybox(0);

                float number = Random.Range(1, 101);
                if (number <= 70)
                    this.SetSkybox(0);
                else if (number > 70 && number <= 90)
                    this.SetSkybox(4);
                else
                    this.SetSkybox(1);

                /*if (this.moon != null)
                    this.moon.SetActive(false);*/
                //RenderSettings.ambientLight = new Color32(180, 135, 135, 255);
                break;
            case 12:
                this.SetMusic(12);
                this.SetSkybox(9);
                /*if (this.moon != null)
                    this.moon.SetActive(false);*/
                //RenderSettings.ambientLight = new Color32(180, 135, 135, 255);
                break;
            case 13:
                this.SetMusic(12);
                this.SetSkybox(Random.Range(10, 15));
                /*if (this.moon != null)
                    this.moon.SetActive(false);*/
                //RenderSettings.ambientLight = new Color32(180, 135, 135, 255);
                break;
            case 14:
                this.SetMusic(13);
                this.SetSkybox(15);
                break;
            case 15:
                this.SetMusic(14);


                this.SetSkybox(2);
                if (this.moon != null)
                    this.moon.SetActive(false);

                this.rgbLightning = true;

                //RenderSettings.ambientLight = new Color32(110, 110, 130, 255);
                RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
                this.numbb = 0f;
                break;
            case 16:
                this.SetMusic(15);
                this.SetSkybox(0);
                this.directionalLight.color = new Color32(242, 249, 255, 255);
                //RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
                break;
            case 17:
                this.SetMusic(15);
                this.SetSkybox(0);
                this.directionalLight.color = new Color32(242, 249, 255, 255);
                //RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
                break;
            case 18:
                this.SetMusic(15);
                this.SetSkybox(0);
                this.directionalLight.color = new Color32(242, 249, 255, 255);
                //RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
                break;
            case 19:
                this.SetMusic(15);
                this.SetSkybox(0);
                this.directionalLight.color = new Color32(242, 249, 255, 255);
                //RenderSettings.ambientLight = new Color32(140, 140, 160, 255);
                break;
            default:
                this.SetMusic(12);
                break;
        }
            

        }
    /*public void PullRandom()
    {
        for (int i = 0; i <= randomTests.Length - 1; i++)
        {
            float randomIndex = Mathf.Round(Random.Range(0.01f, 100f) * 100) / 100;

            if (randomIndex <= randomTests[i].rarity)
            {
                //Debug.Log(i);
                int itemCount = 1;

                for (int e = 0; e < itemCount; e++)
                    Debug.Log(i);
            }


        }
    }*/
}

/*[System.Serializable]
public class RandomTest
{
    [Range(0, 100)]
    public float rarity;
}*/
