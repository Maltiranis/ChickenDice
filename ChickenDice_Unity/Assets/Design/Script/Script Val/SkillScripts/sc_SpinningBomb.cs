using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SpinningBomb : MonoBehaviour
{
    [SerializeField] public float _ray = 1f;
    [SerializeField] public float _maxRay = 7f;
    [SerializeField] public float _grow = 1f;
    [SerializeField] public float _delay = 1f;
    [SerializeField] public float _rSpeed = 10f;
    [SerializeField] public int _damage = 25;

    [SerializeField] float elapsed = 0f;
    [SerializeField] public GameObject _host;

    [SerializeField] public int id;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.GetComponent<sc_VariablesManager>() != null)
        {
            _host = transform.GetComponent<sc_VariablesManager>()._host;
        }

        if (_host == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void Update ()
    {
        if (_host != null)
        {
            transform.position = _host.transform.position;
            transform.Rotate(new Vector3(0, _rSpeed * Time.deltaTime, 0), Space.Self);

            elapsed += Time.time;
            if (elapsed >= _delay)
            {
                elapsed = 0f;

                if (_ray >= _maxRay)
                {
                    _ray = _maxRay;
                }
                else
                {
                    _ray += _grow;
                }
            }

            //transform.Rotate(Time.deltaTime * transform.up * _rSpeed, Space.Self);
        }
        else
        {
            Destroy(gameObject);
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
                Destroy(gameObject);
            }
        }
    }
}
