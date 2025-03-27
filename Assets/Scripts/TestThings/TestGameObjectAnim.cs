using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestGameObjectAnim : MonoBehaviour
{
    public GameObject[] gameObjects;

    public int id = 0;

    public Rigidbody rb;

    public float speed = 0.2f;

    public GameObject objectToEnable;

    public VisualEffect vfx;

    private Vector3 originalPos;


    private void OnEnable()
    {
        this.originalPos = this.transform.localPosition;

        if (this.id == 0)
            this.StartCoroutine(this.TestAnim());
        else if (this.id == 1)
            this.StartCoroutine(this.TestAnim2());
        else if (this.id == 2)
            this.StartCoroutine(this.TestAnim3());
        else if (this.id == 3)
            this.StartCoroutine(this.TestAnim4());
        else if (this.id == 4)
            this.StartCoroutine(this.TestAnim5());
        else if (this.id == 5)
            this.StartCoroutine(this.TestAnim6());
        else if (this.id == 6)
            this.StartCoroutine(this.TestAnim7());
        else if (this.id == 7)
            this.StartCoroutine(this.TestAnim8());
        else if (this.id == 8)
            this.StartCoroutine(this.TestAnim9());
        else if (this.id == 9)
            this.StartCoroutine(this.TestAnim10());
        else if (this.id == 10)
            this.StartCoroutine(this.TestAnim11());
        else if (this.id == 11)
            this.StartCoroutine(this.TestAnim12());
        else if (this.id == 12)
            this.StartCoroutine(this.TestAnim13());
        else if (this.id == 13)
            this.StartCoroutine(this.TestAnim14());
        else if (this.id == 14)
            this.StartCoroutine(this.TestAnim15());
        else if (this.id == 15)
            this.StartCoroutine(this.TestAnim16());
        else if (this.id == 16)
            this.StartCoroutine(this.TestAnim17());
        else if (this.id == 17)
            this.StartCoroutine(this.TestAnim18());
        else if (this.id == 18)
            this.StartCoroutine(this.TestAnim19());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator TestAnimTemplate()
    {
        if (this.gameObjects.Length == 3)
        {
            float animSpeed = this.speed;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.StartCoroutine(this.TestAnimTemplate());
        }
    }

    private IEnumerator TestAnim()
    {
        if (this.gameObjects.Length == 4)
        {
            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);

            yield return new WaitForSeconds(0.3f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.3f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.35f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(0.3f);

            this.StartCoroutine(this.TestAnim());
        }

        
    }


    private IEnumerator TestAnim2()
    {
        if (this.gameObjects.Length == 3)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed + 0.05f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(animSpeed - 0.05f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            /*yield return new WaitForSeconds(0.35f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);*/


            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed - 0.05f);

            this.StartCoroutine(this.TestAnim2());
        }


    }

    private IEnumerator TestAnim3()
    {
        if (this.gameObjects.Length == 8)
        {
            this.gameObjects[0].SetActive(false);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);

            this.gameObjects[7].SetActive(false);

            yield return new WaitForSeconds(0.1f);

            /*if (this.rb != null)
                this.rb.AddForce(new Vector3(0f, 0f, 150f));*/




            float animSpeed = 0.06f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);

            this.gameObjects[7].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            if (this.rb != null)
                this.rb.AddForce(new Vector3(0f, 0f, 150f));


            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);


            yield return new WaitForSeconds(animSpeed + 0.025f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);


            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[5].SetActive(false);
            this.gameObjects[7].SetActive(true);



            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);


            this.gameObjects[7].SetActive(false);


            yield return new WaitForSeconds(0.3f);

            this.StartCoroutine(this.TestAnim3());
        }


    }

    private IEnumerator TestAnim4()
    {
        if (this.gameObjects.Length == 5)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);

            yield return new WaitForSeconds(0.05f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.025f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);


            yield return new WaitForSeconds(0.025f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);
            

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);
            

            yield return new WaitForSeconds(0.4f);


            this.StartCoroutine(this.TestAnim4());
        }


    }

    private IEnumerator TestAnim5()
    {
        if (this.gameObjects.Length == 3)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(0.8f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.05f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.4f);

            this.StartCoroutine(this.TestAnim5());
        }


    }


    private IEnumerator TestAnim6()
    {
        if (this.gameObjects.Length == 6)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);

            yield return new WaitForSeconds(0.6f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.05f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(0.05f);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(0.2f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            yield return new WaitForSeconds(0.05f);

            this.StartCoroutine(this.TestAnim6());
        }


    }


    private IEnumerator TestAnim7()
    {
        if (this.gameObjects.Length == 2)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);

            yield return new WaitForSeconds(0.2f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.2f);

            this.StartCoroutine(this.TestAnim7());
        }


    }

    private IEnumerator TestAnim8()
    {
        if (this.gameObjects.Length == 3)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.StartCoroutine(this.TestAnim8());
        }


    }


    private IEnumerator TestAnim9()
    {
        if (this.gameObjects.Length == 3)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(0.5f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(1f);

            this.StartCoroutine(this.TestAnim9());
        }


    }



    private IEnumerator TestAnim10()
    {
        if (this.gameObjects.Length == 2)
        {
            float animSpeed = this.speed;
            //float animSpeed = 0.05f;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.StartCoroutine(this.TestAnim10());
        }


    }

    private IEnumerator TestAnim11()
    {
        if (this.gameObjects.Length == 8)
        {
            float animSpeed = this.speed;
            //float animSpeed = 0.05f;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(false);


            int amount = 20;
            //int amount = 16;
            int laughId = 0;
            //bool idForward = true;
            while (amount > 0)
            {
                this.gameObjects[0].SetActive(true);
                this.gameObjects[1].SetActive(false);
                yield return new WaitForSeconds(0.03f);
                this.gameObjects[0].SetActive(false);
                this.gameObjects[1].SetActive(true);

                /*yield return new WaitForSeconds(0.001f);
                if (laughId == 0)
                {
                    this.gameObjects[0].SetActive(true);
                    this.gameObjects[1].SetActive(false);
                    laughId = 1;
                }
                else
                {

                    this.gameObjects[0].SetActive(false);
                    this.gameObjects[1].SetActive(true);
                    laughId = 0;
                }*/


                amount -= 1;
                //Debug.Log(laughId);

                if (this.objectToEnable != null && amount <= 6)
                    this.objectToEnable.SetActive(true);

                /*if (this.objectToEnable != null && amount <= 3)
                    this.objectToEnable.SetActive(true);*/


                yield return null;
            }

            if (this.objectToEnable != null)
                this.objectToEnable.SetActive(false);

            //yield return new WaitForSeconds(animSpeed);

            /*if (this.rb != null)
                this.rb.AddForce(new Vector3(0f, 0f, 650f));*/

            if (this.rb != null)
                this.rb.AddForce(new Vector3(650f, 0f, 0f));

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(0.45f);

            /*if (this.rb != null)
                this.rb.velocity = new Vector3(0f, 0f, this.rb.velocity.z / 2f);*/

            if (this.rb != null)
                this.rb.velocity = new Vector3(this.rb.velocity.x / 2f, 0f, 0f);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(0.15f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            //yield return new WaitForSeconds(animSpeed);
            yield return new WaitForSeconds(0.01f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            //this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(true);

            if (this.rb != null)
                this.rb.velocity = Vector3.zero;
            //yield return new WaitForSeconds(animSpeed);
            yield return new WaitForSeconds(1f);

            this.transform.localPosition = this.originalPos;
            this.StartCoroutine(this.TestAnim11());
        }


    }


    private IEnumerator TestAnim12()
    {
        if (this.gameObjects.Length == 8)
        {
            float animSpeed = this.speed;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(false);

            yield return new WaitForSeconds(0.5f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            yield return new WaitForSeconds(0.2f);

            if (this.vfx != null)
                this.vfx.Play();

            yield return new WaitForSeconds(0.1f);

            if (this.vfx != null)
                this.vfx.Stop();

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);

            yield return new WaitForSeconds(animSpeed);

            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(true);

            yield return new WaitForSeconds(2f);

            this.StartCoroutine(this.TestAnim12());
        }
    }


    private IEnumerator TestAnim13()
    {
        if (this.gameObjects.Length == 4)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);

            yield return new WaitForSeconds(0.01f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.04f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);


            yield return new WaitForSeconds(0.1f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);


            yield return new WaitForSeconds(0.2f);


            this.StartCoroutine(this.TestAnim13());
        }


    }

    private IEnumerator TestAnim14()
    {
        if (this.gameObjects.Length == 4)
        {
            float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);

            yield return new WaitForSeconds(0.04f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.01f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);


            yield return new WaitForSeconds(0.1f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);


            yield return new WaitForSeconds(0.2f);


            this.StartCoroutine(this.TestAnim14());
        }


    }

    private IEnumerator TestAnim15()
    {
        if (this.gameObjects.Length == 7)
        {
            //float animSpeed = 0.2f;

            this.gameObjects[0].SetActive(true);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);

            yield return new WaitForSeconds(0.2f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(this.speed);

            if (this.rb != null)
                this.rb.AddForce(new Vector3(0f, 0f, 1000f));

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);


            yield return new WaitForSeconds(this.speed);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(this.speed);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(this.speed);

            if (this.rb != null)
            {
                this.rb.velocity = new Vector3(0f, 0f, this.rb.velocity.z / 2f);
                //this.rb.velocity = new Vector3(0f, 0f, 0f);
            }
                

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            yield return new WaitForSeconds(0.15f);

            if (this.rb != null)
            {
                //this.rb.velocity = new Vector3(0f, 0f, this.rb.velocity.z / 2f);
                this.rb.velocity = new Vector3(0f, 0f, 0f);
            }

            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);

            yield return new WaitForSeconds(0.4f);

            this.transform.localPosition = this.originalPos;

            this.StartCoroutine(this.TestAnim15());
        }


    }

    private IEnumerator TestAnim16()
    {
        if (this.gameObjects.Length == 6)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);

            yield return new WaitForSeconds(0.2f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(2f);

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            yield return new WaitForSeconds(0.2f);

            this.StartCoroutine(this.TestAnim16());
        }


    }


    private IEnumerator TestAnim17()
    {
        if (this.gameObjects.Length == 2)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);

            yield return new WaitForSeconds(this.speed);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(1f);

            this.StartCoroutine(this.TestAnim17());
        }


    }

    private IEnumerator TestAnim18()
    {
        if (this.gameObjects.Length == 3)
        {
            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);

            yield return new WaitForSeconds(1f);

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.5f);

            this.StartCoroutine(this.TestAnim18());
        }


    }




    private IEnumerator TestAnim19()
    {
        if (this.gameObjects.Length == 12)
        {
            float animSpeed = this.speed;
            //float animSpeed = 0.05f;

            this.gameObjects[0].SetActive(true);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(false);

            this.gameObjects[8].SetActive(false);
            this.gameObjects[9].SetActive(false);
            this.gameObjects[10].SetActive(false);
            this.gameObjects[11].SetActive(false);



            int amount = 20;
            //int amount = 16;
            int laughId = 0;
            //bool idForward = true;
            while (amount > 0)
            {
                this.gameObjects[0].SetActive(true);
                this.gameObjects[1].SetActive(false);
                yield return new WaitForSeconds(0.03f);
                this.gameObjects[0].SetActive(false);
                this.gameObjects[1].SetActive(true);

                /*yield return new WaitForSeconds(0.001f);
                if (laughId == 0)
                {
                    this.gameObjects[0].SetActive(true);
                    this.gameObjects[1].SetActive(false);
                    laughId = 1;
                }
                else
                {

                    this.gameObjects[0].SetActive(false);
                    this.gameObjects[1].SetActive(true);
                    laughId = 0;
                }*/


                amount -= 1;
                //Debug.Log(laughId);

                if (this.objectToEnable != null && amount <= 6)
                    this.objectToEnable.SetActive(true);

                /*if (this.objectToEnable != null && amount <= 3)
                    this.objectToEnable.SetActive(true);*/


                yield return null;
            }

            if (this.objectToEnable != null)
                this.objectToEnable.SetActive(false);

            //yield return new WaitForSeconds(animSpeed);

            /*if (this.rb != null)
                this.rb.AddForce(new Vector3(0f, 0f, 650f));*/

            if (this.rb != null)
            {
                //this.rb.AddForce(new Vector3(650f, 0f, 0f));
                this.rb.velocity = new Vector3(30f, this.rb.velocity.y, 0f);
            }
                

            this.gameObjects[0].SetActive(false);
            this.gameObjects[1].SetActive(false);
            this.gameObjects[2].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[2].SetActive(false);
            this.gameObjects[3].SetActive(true);

            yield return new WaitForSeconds(0.25f);

            /*if (this.rb != null)
                this.rb.velocity = new Vector3(0f, 0f, this.rb.velocity.z / 2f);*/

            if (this.rb != null)
            {
                //this.rb.velocity = new Vector3(this.rb.velocity.x / 2f, 0f, 0f);

                //this.rb.velocity = new Vector3(this.rb.velocity.x / 5f, 0f, 0f);
                this.rb.velocity = new Vector3(5f, this.rb.velocity.y, 0f);
            }
                

            this.gameObjects[3].SetActive(false);
            this.gameObjects[4].SetActive(true);

            yield return new WaitForSeconds(0.15f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(true);

            //yield return new WaitForSeconds(animSpeed);
            yield return new WaitForSeconds(0.01f);

            this.gameObjects[4].SetActive(false);
            this.gameObjects[5].SetActive(false);
            this.gameObjects[6].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            if (this.rb != null)
            {
                //this.rb.velocity = new Vector3(this.rb.velocity.x / 2f, 0f, 0f);
                this.rb.velocity = Vector3.zero;
            }


            this.gameObjects[6].SetActive(false);
            this.gameObjects[7].SetActive(true);

            yield return new WaitForSeconds(0.05f);

            this.gameObjects[7].SetActive(false);
            this.gameObjects[8].SetActive(true);

            yield return new WaitForSeconds(0.01f);

            this.gameObjects[8].SetActive(false);
            this.gameObjects[9].SetActive(true);


            yield return new WaitForSeconds(0.01f);

            this.gameObjects[9].SetActive(false);
            this.gameObjects[10].SetActive(true);

            yield return new WaitForSeconds(0.1f);

            this.gameObjects[10].SetActive(false);
            this.gameObjects[9].SetActive(true);

            yield return new WaitForSeconds(0.01f);




            //this.gameObjects[5].SetActive(false);
            this.gameObjects[9].SetActive(false);
            this.gameObjects[10].SetActive(false);
            this.gameObjects[11].SetActive(true);

            if (this.rb != null)
                this.rb.velocity = Vector3.zero;
            //yield return new WaitForSeconds(animSpeed);
            yield return new WaitForSeconds(1f);

            this.transform.localPosition = this.originalPos;
            this.StartCoroutine(this.TestAnim19());
        }


    }
}
