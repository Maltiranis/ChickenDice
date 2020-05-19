using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Peck : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public int _damages = 10;
    [SerializeField] public float _pushForce = 100f;
    [SerializeField] public float _radius = 2f;
    [Space(10)]
    [Header("Damages Object")]
    [SerializeField] private GameObject _peckOffsetObject;
    SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col = _peckOffsetObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("xA"))
        {
            _peckOffsetObject.SetActive(true);
            StartCoroutine(DamageIEnumerator());
        }
    }

    public IEnumerator DamageIEnumerator()
    {
        yield return new WaitForEndOfFrame();
        _peckOffsetObject.SetActive(false);
    }
}
