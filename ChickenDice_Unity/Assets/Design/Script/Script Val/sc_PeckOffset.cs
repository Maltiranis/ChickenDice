using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_PeckOffset : sc_Peck
{
    [SerializeField] private GameObject _chicken;
    private int _my_ID;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.activeSelf)
            gameObject.SetActive(false);
        _my_ID = _chicken.GetComponent<sc_Chicken_ID>().ID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrackDetected (GameObject c)
    {
        c.GetComponent<Crack>()._CrackLife -= _damages;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _chicken)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                if (other.gameObject.GetComponent<sc_LifeEngine>() != null)
                    other.gameObject.GetComponent<sc_LifeEngine>().TakeDamage(_damages, _my_ID);
                if (other.gameObject.GetComponent<Crack>() != null)
                    CrackDetected(other.gameObject);
                if (other.gameObject.GetComponent<ScarecrowMode>() != null)
                    other.gameObject.GetComponent<ScarecrowMode>()._PeckCounter++;
                other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(_pushForce, transform.position, _radius, 0, ForceMode.Impulse);
            }
        }
    }
}
