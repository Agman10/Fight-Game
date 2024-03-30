using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObjectAnim : MonoBehaviour
{
    public GameObject[] gameObjects;

    public int id = 0;

    private void OnEnable()
    {
        if (this.id == 0)
            this.StartCoroutine(this.TestAnim());
        else if (this.id == 1)
            this.StartCoroutine(this.TestAnim2());
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
}
