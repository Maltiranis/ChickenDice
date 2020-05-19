using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_PeckOffset : sc_Peck
{
    [SerializeField] private GameObject _chicken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _chicken)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<sc_LifeEngine>().TakeDamage(_damages);
                other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(_pushForce, transform.position, _radius, 0, ForceMode.Impulse);
            }
        }
    }
}
