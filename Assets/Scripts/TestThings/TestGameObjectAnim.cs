using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObjectAnim : MonoBehaviour
{
    public GameObject[] gameObjects;

    public int id = 0;

    public Rigidbody rb;

    private void OnEnable()
    {
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
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
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
}
