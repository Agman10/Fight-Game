using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTest : MonoBehaviour
{
    public AnimationGameObject[] animationGameObjects;

    private void OnEnable()
    {
        this.StartCoroutine(this.AnimateCoroutine());
        //this.StartCoroutine(this.AnimateCoroutine2());
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator AnimateCoroutine()
    {
        int amount = this.animationGameObjects.Length;
        //Debug.Log(amount);

        int animId = 0;

        //yield return new WaitForSeconds(1f);

        while (amount > 0)
        {
            foreach (AnimationGameObject animationGameObject in this.animationGameObjects)
            {
                if (animationGameObject.animGameObject != null)
                    animationGameObject.animGameObject.SetActive(false);
            }

            if (this.animationGameObjects[animId].animGameObject != null)
                this.animationGameObjects[animId].animGameObject.SetActive(true);


            yield return new WaitForSeconds(this.animationGameObjects[animId].duration);

            //Debug.Log(animId);

            animId += 1;
            amount -= 1;

            yield return null;
        }

        this.StartCoroutine(this.AnimateCoroutine());
    }

    private IEnumerator AnimateCoroutine2()
    {
        int amount = this.animationGameObjects.Length;

        for (int i = 0; i < amount; i++)
        {
            foreach (AnimationGameObject animationGameObject in this.animationGameObjects)
            {
                if (animationGameObject.animGameObject != null)
                    animationGameObject.animGameObject.SetActive(false);
            }

            if (this.animationGameObjects[i].animGameObject != null)
                this.animationGameObjects[i].animGameObject.SetActive(true);


            yield return new WaitForSeconds(this.animationGameObjects[i].duration);
        }

        this.StartCoroutine(this.AnimateCoroutine2());
    }
}

[System.Serializable]
public class AnimationGameObject
{
    public GameObject animGameObject;
    public float duration;
}
