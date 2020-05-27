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
    [SerializeField] public GameObject _lbActive = null;
    [SerializeField] public GameObject _rbActive = null;
    [SerializeField] public GameObject _xPassive = null;
    [SerializeField] public GameObject _yPassive = null;
    [Space(10)]
    [Header("New Spell")]
    [SerializeField] public GameObject _newPassive = null;
    [SerializeField] public GameObject _newActive = null;
    [HideInInspector]
    [SerializeField] public GameObject _tempGameObject = null;
    [Space(10)]
    [Header("Offset")]
    [SerializeField] public GameObject _shootOffset;
    [Space(10)]
    [Header("Variables")]
    [SerializeField] public float _actionRange = 1.0f;

    GameObject _previousActiveGem;
    GameObject _previousPassiveGem;

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
                GameObject[] Gem = GameObject.FindGameObjectsWithTag("Gem");

                if (GetClosest(Gem) != null)
                {
                    _tempGameObject = GetClosest(Gem);//On garde en mémoire la gemme la plus proche

                    SpellAttribution(_tempGameObject);//On la place où il faut

                    _xPassive = _newPassive;
                    _Xempty = false;
                }
            }
            else
            {
                GameObject[] Gem = GameObject.FindGameObjectsWithTag("Gem");

                if (GetClosest(Gem) != null)
                {
                    _tempGameObject = GetClosest(Gem);//On garde en mémoire la gemme la plus proche

                    if (_previousPassiveGem != null)
                        SwitchGems(_previousPassiveGem, _tempGameObject.transform);//On echange les gemmes
                    
                    SpellAttribution(_tempGameObject);//On la place où il faut

                    _xPassive = _newPassive;
                    _Xempty = false;
                }
            }
        }
        if (_slot == "Y")
        {
            if (_Yempty == true)
            {
                //la
            }
            else
            {

            }
        }
    }

    public void SwitchGems (GameObject g, Transform pos)
    {
        Instantiate(g, pos.position, pos.rotation);
    }

    public void SpellAttribution (GameObject o)
    {
        sc_Gem_ID id;
        id = o.GetComponent<sc_Gem_ID>();

        if (id._type == "A")
        {
            if (id._preEstablishedSpell != null)
            {
                _newActive = id._preEstablishedSpell;
            }
            else
            {
                _newActive = id.RandomSelectedSpell();
            }

            _previousActiveGem = id._ParentGemContainer;
        }
        if (id._type == "P")
        {
            if (id._preEstablishedSpell != null)
            {
                _newPassive = id._preEstablishedSpell;
            }
            else
            {
                _newPassive = id.RandomSelectedSpell();
            }

            _previousPassiveGem = id._ParentGemContainer;
        }

        switch (id._rarity)
        {
            case 0:
                //set rarity
                break;
            case 1:
                //set rarity
                break;
            case 2:
                //set rarity
                break;
            //-----------------//
            default:
                break;
        }
    }

    GameObject GetClosest(GameObject[] gem)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = _actionRange; //Ou Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject g in gem)
        {
            if (g.activeSelf == true)
            {
                Vector3 directionToTarget = g.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = g;
                }
                else
                {
                    bestTarget = null;
                }
            }
        }

        return bestTarget;
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
