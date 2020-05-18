using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] private float _LifeTime = 0f;
    [SerializeField] private bool _CanbeDesactive = true;
 
    void Update()
    {
        if (gameObject.activeSelf && _CanbeDesactive == true)
        {
            StartCoroutine(Lifeduration());
            _CanbeDesactive = false;
        }
    }
    private IEnumerator Lifeduration()
    {
        yield return new WaitForSeconds(_LifeTime);
        _CanbeDesactive = true;
        gameObject.SetActive(false);
    }

   
}
    