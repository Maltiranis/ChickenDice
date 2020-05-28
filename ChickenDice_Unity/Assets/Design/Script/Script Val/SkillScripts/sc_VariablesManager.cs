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
    [SerializeField] public float _speed = 25.0f;
    [SerializeField] public float _radius = 0.5f;
    [SerializeField] public float _lifeTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (_isActive)
        {
            StatesChanges();
            Motor();
        }
    }

    void StatesChanges ()
    {
        transform.localScale *= _radius;
    }

    void Motor ()
    {
        gameObject.GetComponent<Rigidbody>().AddForce
        (
            transform.forward * _speed, ForceMode.Impulse
        );
        Destroy(gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
