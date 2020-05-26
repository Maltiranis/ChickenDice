using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SpinningBomb : MonoBehaviour
{
    public GameObject _bomb;

    public float _ray = 1f;
    public float _maxRay = 7f;
    public float _grow = 1f;
    public float _delay = 1f;
    public float _rSpeed = 10f;
    float originRay;

    // Start is called before the first frame update
    void Start()
    {
        originRay = _ray;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = 0f;

        elapsed += Time.time;
        if (elapsed >= _delay)
        {
            elapsed = 0f;
            _ray += _grow;
        }

        _bomb.transform.localPosition = new Vector3(_ray, _bomb.transform.localPosition.y, _bomb.transform.localPosition.z);

        if (Vector3.Distance(_bomb.transform.localPosition, transform.position) >= _maxRay)
        {
            _ray = originRay;
        }

        transform.Rotate(Time.deltaTime * transform.up * _rSpeed, Space.Self);
    }
}
