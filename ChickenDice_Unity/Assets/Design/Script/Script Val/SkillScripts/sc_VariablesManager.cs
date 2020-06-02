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
    [SerializeField] public bool isFirst = true;
    [SerializeField] public bool _doDamage = true;
    [SerializeField] public float _explosionRadius = 10f;
    [Space(10)]
    [Header("VFX")]
    [SerializeField] public GameObject _onDeath_VFX;
    [SerializeField] public GameObject[] _aliveVFX;
    [SerializeField] public float _VFX_lifetime = 2.0f;
    Vector3 newScale;
    float newTime = 0f;

    void Start ()
    {
        newScale = new Vector3(_radius, _radius, _radius);
        shootPos = transform.position;
        //On empile les codes spéciaux
        if (_triple == true)
        {
            if (isFirst == true)
            {
                transform.localScale = new Vector3(_radius, _radius, _radius);
            }
            else
            {
                sc_VariablesManager c_svm = transform.parent.GetComponent<sc_VariablesManager>();
                _damage = c_svm._damage;
                _range = c_svm._range;
                _speed = c_svm._speed;
                _radius = c_svm._radius;
                _lifeTime = c_svm._lifeTime;
                _explosionRadius = c_svm._explosionRadius;
                id = c_svm.id;

                GameObject c = transform.parent.gameObject;
                transform.localScale = c.transform.localScale;
                if (_aliveVFX != null)
                    if (_aliveVFX.Length != 0)
                        foreach (GameObject fx in _aliveVFX)
                        {
                            fx.transform.localScale = new Vector3(_radius, _radius, _radius);
                        }
                transform.SetParent(null);
            }

        }

        if (_isActive)
            WhatAnActiveObjectMustDoOnStart();
        else
        {
            if (transform.GetComponentInChildren<sc_VariablesManager>() != null)
            {
                GameObject c = transform.GetChild(0).gameObject;
                sc_VariablesManager c_svm = c.GetComponent<sc_VariablesManager>();
                c_svm._damage = _damage;
                c_svm._range = _range;
                c_svm._speed = _speed;
                c_svm._radius = _radius;
                c_svm._lifeTime = _lifeTime;
                c_svm.id = id;

                c.transform.localScale = new Vector3(_radius, _radius, _radius);
                GameObject[] _aliveVFXchild;
                _aliveVFXchild = c_svm._aliveVFX;
                if (_aliveVFXchild != null)
                    if (_aliveVFXchild.Length != 0)
                        foreach (GameObject fx in _aliveVFXchild)
                        {
                            fx.transform.localScale = new Vector3(_radius, _radius, _radius);
                        }
            }
            /*if (nonLinear == true)
                Destroy(gameObject);*/
        }
    }

    private void Update()
    {
        if (_isActive)
            WhatAnActiveObjectMustDoOnUpdate();

        newTime += Time.deltaTime;

        if (newTime >= _lifeTime)
            BeforeTheEnd();
    }

    void WhatAnActiveObjectMustDoOnStart()
    {
        //---------------------------------------------------------------------
        if (nonLinear == false)
        {
            if (_aliveVFX != null)
                if (_aliveVFX.Length != 0)
                    foreach (GameObject fx in _aliveVFX)
                    {
                        fx.transform.localScale = new Vector3(_radius, _radius, _radius);
                    }

            transform.localScale = new Vector3(_radius, _radius, _radius);
            //---------------------------------------------------------------------
            GetComponent<Rigidbody>().AddForce
            (
                transform.forward * _speed, ForceMode.Impulse
            );
            //---------------------------------------------------------------------
        }
        else
        {
            if (gameObject.GetComponent<sc_SpinningBomb>() != null)
            {
                sc_SpinningBomb s_spb = gameObject.GetComponent<sc_SpinningBomb>();
                s_spb._host = _host;
                s_spb.id = id;
                s_spb._damage = _damage;
            }
        }
    }

    void WhatAnActiveObjectMustDoOnUpdate()
    {
        if (nonLinear == false)
        {
            if (Vector3.Distance(transform.position, shootPos) >= _range)
            {
                BeforeTheEnd();
            }
        }
        else
        {
            if (gameObject.GetComponentInParent<sc_SpinningBomb>() != null)
            {
                sc_SpinningBomb s_spb = gameObject.GetComponentInParent<sc_SpinningBomb>();
                transform.Translate(Vector3.forward * s_spb._grow * Time.deltaTime, Space.Self);
            }
        }
    }

    public void BeforeTheEnd() //les fx ect...
    {
        GameObject E = new GameObject("Explosion_FX");
        GameObject Explosion = Instantiate(E, transform.position, Quaternion.identity);
        Destroy(E);
        Explosion.name = "Explosion de Tangtang";
        Explosion.transform.SetParent(null);
        Explosion.AddComponent<sc_SelfDestruction>();
        GameObject tanguetteFX = Instantiate(_onDeath_VFX, Explosion.transform.position, Quaternion.identity);

        Vector3 eRadius = new Vector3(_explosionRadius, _explosionRadius, _explosionRadius);
        //on scale tout les trucs a Tanguette <3
        for (int i = 0; i < tanguetteFX.transform.childCount; i++)
        {
            tanguetteFX.transform.GetChild(i).transform.localScale = eRadius;
        }

        tanguetteFX.transform.parent = Explosion.transform;
        if (transform.GetComponentInChildren<sc_VariablesManager>() == null)
            Explosion.transform.position = transform.position;
        else
            Explosion.transform.position = transform.GetChild(0).position;
        Explosion.GetComponent<sc_SelfDestruction>().DestroyMyself(_VFX_lifetime);
        if (_doDamage == false)
            Explosion.GetComponent<sc_SelfDestruction>().KillEverybody(_damage, id, _range);//_explosionRadius =/= range

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<sc_Chicken_ID>() != null)
        {
            if (other.GetComponent<sc_Chicken_ID>().ID != id)
            {
                if (_doDamage == true)
                    other.GetComponent<sc_LifeEngine>().TakeDamage(_damage, id);
                BeforeTheEnd();
            }
        }
    }
}
