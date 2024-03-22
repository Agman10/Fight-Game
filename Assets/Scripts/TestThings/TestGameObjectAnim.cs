using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameObjectAnim : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void OnEnable()
    {
        this.StartCoroutine(this.TestAnim());
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
}
