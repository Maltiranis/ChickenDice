using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] private float _LifeTime = 0f;
 
    void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Lifeduration());

        }
    }
    



    private IEnumerator Lifeduration()
    {
        yield return new WaitForSeconds(_LifeTime);
        gameObject.SetActive(false);
    }

   
}
    