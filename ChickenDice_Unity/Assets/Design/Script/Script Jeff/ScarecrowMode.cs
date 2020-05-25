using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowMode : MonoBehaviour
{
    public int _PeckCounter = 0;
    private int _PeckCounterBaseUpdate = 0;
    [SerializeField] private GameObject _ToScale = null;
    [SerializeField] private float _ScaleFactor = 0f;
    

    private void Update()
    {
        if(_PeckCounter != _PeckCounterBaseUpdate)
        {
            _PeckCounterBaseUpdate = _PeckCounter;
            _ToScale.transform.localScale += new Vector3(_ScaleFactor,_ScaleFactor,_ScaleFactor);

        }
    }
}
