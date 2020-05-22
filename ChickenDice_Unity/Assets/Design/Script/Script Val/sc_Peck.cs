﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Peck : MonoBehaviour
{
    [Header("Chicken ID")]
    [SerializeField] public sc_Chicken_ID _myID;
    [HideInInspector] public int _id;
    [Header("Variables")]
    [SerializeField] public int _damages = 10;
    [SerializeField] public float _pushForce = 100f;
    [SerializeField] public float _radius = 2f;
    [Space(10)]
    [Header("Damages Object")]
    [SerializeField] private GameObject _peckOffsetObject;
    SphereCollider col;
    sc_AnimManagement _am;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        col = _peckOffsetObject.GetComponent<SphereCollider>();

        if (GetComponent<sc_AnimManagement>() != null)
        {
            _am = GetComponent<sc_AnimManagement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pecking();
        Taunting();
    }

    void Pecking()
    {
        if (Input.GetButtonDown("xA_" + _id.ToString()))
        {
            _peckOffsetObject.SetActive(true);
            StartCoroutine(DamageIEnumerator());
            _am.Pecked();
        }
        if (Input.GetButtonUp("xA_" + _id.ToString()))
        {
            _peckOffsetObject.SetActive(false);
        }
    }

    void Taunting()
    {
        if (Input.GetButtonDown("xB_" + _id.ToString()))
        {
            _am.Taunted();
        }
    }

    public IEnumerator DamageIEnumerator()
    {
        yield return new WaitForSeconds(0.1f);
        _peckOffsetObject.SetActive(false);
    }
}
