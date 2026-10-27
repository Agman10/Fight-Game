using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPosAnimation : MonoBehaviour
{
    public Transform posGameObject;
    public AnimationPosition[] AnimationPositions;
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
        this.StartCoroutine(this.AnimateCoroutine());
    }

    private IEnumerator AnimateCoroutine()
    {
        int amount = this.AnimationPositions.Length;
        if (this.posGameObject != null)
        {
            for (int i = 0; i < amount; i++)
            {

                this.posGameObject.localPosition = this.AnimationPositions[i].position;


                yield return new WaitForSeconds(this.AnimationPositions[i].duration);
            }

            this.StartCoroutine(this.AnimateCoroutine());
        }
        
    }
}
[System.Serializable]
public class AnimationPosition
{
    public Vector3 position;
    public float duration;
}