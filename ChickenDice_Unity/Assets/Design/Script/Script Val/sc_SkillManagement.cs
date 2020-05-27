using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SkillManagement : MonoBehaviour
{
    [Header("Equiped Spell")]
    [SerializeField] public bool _Xempty = true;
    [SerializeField] public bool _Yempty = true;
    [Space(10)]
    [Header("Equiped Spell")]
    [SerializeField] public GameObject _xActive = null;
    [SerializeField] public GameObject _yActive = null;
    [SerializeField] public GameObject _xPassive = null;
    [SerializeField] public GameObject _yPassive = null;
    [Space(10)]
    [Header("New Spell")]
    [SerializeField] public GameObject _newPassive = null;
    [SerializeField] public GameObject _newActive = null;
    [Space(10)]
    [Header("Offset")]
    [SerializeField] public GameObject _shootOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
     * X: ramasse ou échange gemme PASSIVE déjà équipée
     * Y: ramasse ou échange gemme PASSIVE déjà équipée
     * LB: ramasse ou échange gemme ACTIVE déjà équipée Slot 1
     * RB: ramasse ou échange gemme ACTIVE déjà équipée Slot 2
     * LT : Tire la gemme active ramassée avec LB -> gemme Slot 1
     * RT : Tire la gemme active ramassée avec RB -> gemme Slot 2
     */
    public void GetDropGem(string _slot)//X ; Y
    {
        if (_slot == "X")
        {
            GetSlot(_slot);
        }
        if (_slot == "Y")
        {
            GetSlot(_slot);
        }
    }

    public void GetSlot (string _slot)
    {
        if (_slot == "X")
        {
            if (_Xempty == true)
            {

            }
        }
        if (_slot == "Y")
        {
            if (_Yempty == true)
            {

            }
        }
    }

    public void UseGem(string _slot)
    {
        if (_slot == "X")
        {
            
        }
        if (_slot == "Y")
        {

        }
    }

    public void NeedInstance (GameObject prefab, GameObject modifier)
    {
        GameObject xA = Instantiate(prefab, transform.position, transform.rotation);
    }
}
