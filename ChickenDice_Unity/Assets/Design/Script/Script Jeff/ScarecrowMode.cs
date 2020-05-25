using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowMode : MonoBehaviour
{
    public int _PeckCounter = 0;
    private int _PeckCounterBaseUpdate = 0;
    [SerializeField] private GameObject _ToScale = null;
    [SerializeField] private float _ScaleFactor = 0f;
    public int _ModeSelect = 0;

    [SerializeField] private bool _Mode01 = false;
    [SerializeField] private bool _Mode02 = false;
    [SerializeField] private bool _Mode03 = false;

    private void Start()
    {
        if(_ModeSelect == 1)
        {
            _Mode01 = true;
        }
        if(_ModeSelect == 2)
        {
            _Mode02 = true;
        }
        if(_ModeSelect == 3)
        {
            _Mode03 = true;
        }
    }
    private void Update()
    {
        if(_PeckCounter != _PeckCounterBaseUpdate)
        {
            _PeckCounterBaseUpdate = _PeckCounter;
            _ToScale.transform.localScale += new Vector3(_ScaleFactor,_ScaleFactor,_ScaleFactor);

        }
    }
}
