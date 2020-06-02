using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_VariablesManager : MonoBehaviour
{
    [Header("Active / Passive")]
    [SerializeField] public bool _isActive = true;
    [Space(10)]
    [Header("Variables")]
    [SerializeField] public int _damage = 25;
    [SerializeField] public float _range = 10f;
    [SerializeField] public float _speed = 25.0f;
    [SerializeField] public float _radius = 0.5f;
    [SerializeField] public float _lifeTime = 5.0f;
    Vector3 shootPos;
    [SerializeField] public int id;
    [SerializeField] public GameObject _host;
    [Space(10)]
    [Header("Multiple projectiles")]
    [SerializeField] public bool nonLinear = false;
    [SerializeField] public bool _triple = false;
    [SerializeField] public float _angle = 30f;
    [HideInInspector] public bool isFirst = true;

    void Start ()
    {
        shootPos = transform.position;
        //On empile les codes spéciaux
        if (gameObject.GetComponent<sc_SpinningBomb>() != null)
        {
            nonLinear = true;
        }
        if (transform.childCount > 0)
        {
            if (transform.GetComponentInChildren<sc_VariablesManager>() != null)
            {
                sc_VariablesManager c_svm = transform.GetComponentInChildren<sc_VariablesManager>();
                c_svm._damage = _damage;
                c_svm._range = _range;
                c_svm._speed = _speed;
                c_svm._radius = _radius;
                c_svm._lifeTime = _lifeTime;
            }
            transform.DetachChildren();
        }

        if (_isActive)
            WhatAnActiveObjectMustDoOnStart();
    }

    private void Update()
    {
        if (_isActive)
            WhatAnActiveObjectMustDoOnUpdate();
    }

    void WhatAnActiveObjectMustDoOnStart()
    {
        Destroy(gameObject, _lifeTime);
        if (nonLinear == false)
        {
            //---------------------------------------------------------------------
            GetComponent<Rigidbody>().AddForce
            (
                transform.forward * _speed, ForceMode.Impulse
            );
            transform.localScale = new Vector3(_radius, _radius, _radius);
            //---------------------------------------------------------------------
        }
    }

    void WhatAnActiveObjectMustDoOnUpdate()
    {
        if (nonLinear == false)
        {
            if (Vector3.Distance(transform.position, shootPos) >= _range)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (gameObject.GetComponent<sc_SpinningBomb>() != null)
            {
                gameObject.GetComponent<sc_SpinningBomb>().BombIsSpinning();
            }
        }
    }

    public void BeforeTheEnd() //les fx ect...
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<sc_Chicken_ID>() != null)
        {
            if (other.GetComponent<sc_Chicken_ID>().ID != id)
            {
                other.GetComponent<sc_LifeEngine>().TakeDamage(_damage, id);
                BeforeTheEnd();
                Destroy(gameObject);
            }
        }
    }
}
