using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_AnimManagement : MonoBehaviour
{
    [Header("Speed variation")]
    [Range(0f, 1f)]
    [SerializeField] private float _speed = 0f;
    [Space(10)]
    [Header("Kind of Death")]
    [Range(0, 3)]
    [SerializeField] private int _deaths = 100;
    [Space(10)]
    [Header("Mental Disease")]
    [Range(0, 3)]
    [SerializeField] private int _head = 100;
    [Space(10)]
    [Header("Combat State")]
    [SerializeField] public bool _onHit;
    [SerializeField] public bool _onPeck;
    [SerializeField] public bool _onTaunt;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetStates();
    }

    public void GetStates()
    {
        _speed = anim.GetFloat("floatSpeed");
        _deaths = anim.GetInteger("intDeath");
        _head = anim.GetInteger("intHead");
    }
}
