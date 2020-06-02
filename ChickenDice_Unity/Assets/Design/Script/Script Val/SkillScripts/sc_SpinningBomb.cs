using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof (sc_VariablesManager))]
public class sc_SpinningBomb : MonoBehaviour
{
    GameObject chicken;

    public float _ray = 1f;
    public float _maxRay = 7f;
    public float _grow = 1f;
    public float _delay = 1f;
    public float _rSpeed = 10f;
    float originRay;
    float elapsed = 0f;
    GameObject newPivot;

    // Start is called before the first frame update
    void Start()
    {
        originRay = _ray;
        chicken = GetComponent<sc_VariablesManager>()._host;
        newPivot = new GameObject();
        newPivot.transform.parent = chicken.transform;
        transform.parent = newPivot.transform;
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void BombIsSpinning()
    {

        elapsed += Time.time;
        if (elapsed >= _delay)
        {
            elapsed = 0f;
            _ray += _grow;
        }


        if (Vector3.Distance(transform.localPosition, chicken.transform.position) >= _maxRay)
        {
            //Destroy(gameObject);
        }

        if (_ray >= _maxRay)
        {
            Destroy(newPivot);
        }
        newPivot.transform.localPosition = Vector3.zero;
        newPivot.transform.Rotate(new Vector3(0, _rSpeed, 0), Space.Self);

        transform.Translate(newPivot.transform.forward * _ray * Time.deltaTime);

        //transform.Rotate(Time.deltaTime * transform.up * _rSpeed, Space.Self);
    }
    private void OnDestroy()
    {
        Destroy(newPivot);
    }
}
