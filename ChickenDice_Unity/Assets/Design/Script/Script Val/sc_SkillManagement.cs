using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_SkillManagement : MonoBehaviour
{
    [Header("Equiped Spell")]
    [SerializeField] public bool _lbEmpty = true;
    [SerializeField] public bool _rbEmpty = true;
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

    sc_Shooting scs;

    // Start is called before the first frame update
    void Start()
    {
        scs = GetComponent<sc_Shooting>();
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
    public void GetDropGem(string _slot)//X ; Y ; LB ; RB
    {
        if (_slot == "X")
        {
            GetSlot(_slot);
        }
        if (_slot == "Y")
        {
            GetSlot(_slot);
        }
        if (_slot == "LB")
        {
            GetSlot(_slot);
        }
        if (_slot == "RB")
        {
            GetSlot(_slot);
        }
        /*if (_tempGameObject != null)
            _tempGameObject.SetActive(false);*/
    }

    public void GetSlot (string _slot)
    {
        GameObject[] Gem = GameObject.FindGameObjectsWithTag("Gem");
        sc_Gem_ID id;

        if (GetClosest(Gem) != null)
        {
            _tempGameObject = GetClosest(Gem);//On garde en mémoire la gemme la plus proche
        }
        else
        {
            return;
        }

        if (_tempGameObject == null)
        {
            return;
        }

        id = _tempGameObject.GetComponent<sc_Gem_ID>();

        if (_slot == "X" && id._type == "P")
        {
            if (_Xempty == true)
            {
                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _xPassive = _newPassive;
                _Xempty = false;
            }
            else
            {
                SwitchGems(_previousPassiveGem, _tempGameObject.transform);//On echange les gemmes
                    
                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _xPassive = _newPassive;
                _Xempty = false;
            }
        }
        if (_slot == "Y" && id._type == "P")
        {
            if (_Yempty == true)
            {
                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _yPassive = _newPassive;
                _Yempty = false;
            }
            else
            {
                SwitchGems(_previousPassiveGem, _tempGameObject.transform);//On echange les gemmes

                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _yPassive = _newPassive;
                _Yempty = false;
            }
        }
        if (_slot == "LB" && id._type == "A")
        {
            if (_lbEmpty == true)
            {
                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _lbActive = _newActive;
                _lbEmpty = false;
            }
            else
            {
                SwitchGems(_previousActiveGem, _tempGameObject.transform);//On echange les gemmes

                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _lbActive = _newActive;
                _lbEmpty = false;
            }
        }
        if (_slot == "RB" && id._type == "A")
        {
            if (_lbEmpty == true)
            {
                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _rbActive = _newActive;
                _rbEmpty = false;
            }
            else
            {
                SwitchGems(_previousActiveGem, _tempGameObject.transform);//On echange les gemmes

                SpellAttribution(_tempGameObject);//On la place où il faut

                _tempGameObject.SetActive(false);

                _rbActive = _newActive;
                _rbEmpty = false;
            }
        }
    }

    public void SwitchGems (GameObject g, Transform pos)
    {
        GameObject newGem = Instantiate(g, pos.position, pos.rotation);
        //newGem.transform.parent = pos.parent;

        newGem.SetActive(true);
        //Destroy(newGem, 10.0f);//A méditer

        //
        Debug.Log("On a remplacé le biniou !");
        //
    }

    public void SpellAttribution (GameObject o)
    {
        sc_Gem_ID id;
        id = o.GetComponent<sc_Gem_ID>();
        GameObject newO = o;

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
        }
        _previousActiveGem = newO;
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

    /*
    _lbActive
    _rbActive
    _xPassive
    _yPassive
    */
    public void UseGem(string _slot)
    {
        if (_slot == "RT")
        {
            if (_rbActive != null)
                scs.Shoot(_rbActive, _yPassive, _xPassive);
        }
        if (_slot == "LT")
        {
            if (_lbActive != null)
                scs.Shoot(_lbActive, _yPassive, _xPassive);
        }
    }
}
