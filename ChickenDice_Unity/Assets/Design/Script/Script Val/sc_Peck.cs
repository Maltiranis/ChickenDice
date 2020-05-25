using System.Collections;
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
    [SerializeField] public bool _pecking = false;
    [Space(10)]
    [Header("Damages Object")]
    [SerializeField] private GameObject _peckOffsetObject;
    [SerializeField] private GameObject _root;
    [Space(10)]
    [Header("VFX Object")]
    [SerializeField] private GameObject _vfx;
    SphereCollider col;
    [SerializeField] public sc_AnimManagement _am;

    // Start is called before the first frame update
    void Start()
    {
        _id = _myID.ID;
        col = _peckOffsetObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pecking()
    {
        GameObject newVFX = Instantiate(_vfx, _root.transform.position, _root.transform.rotation);
        newVFX.transform.parent = _root.transform;
        newVFX.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f) ;
        Destroy(newVFX, 1.5f);
        _peckOffsetObject.SetActive(true);
        StartCoroutine(DamageIEnumerator());
        _am.Pecked();
        _pecking = true;
    }

    public void Unpeck()
    {
        _peckOffsetObject.SetActive(false);
    }

    public IEnumerator DamageIEnumerator()
    {
        yield return new WaitForSeconds(0.1f);
        _peckOffsetObject.SetActive(false);
        _pecking = false;
    }
}
