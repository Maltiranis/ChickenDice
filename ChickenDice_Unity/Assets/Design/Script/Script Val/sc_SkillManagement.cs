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
    [Space(10)]
    [Header("Previous Gem")]
    [SerializeField] public GameObject _previous_lb_Gem = null;
    [SerializeField] public GameObject _previous_rb_Gem = null;
    [SerializeField] public GameObject _previous_x_Gem = null;
    [SerializeField] public GameObject _previous_y_Gem = null;
    [Space(10)]
    [Header("Offset")]
    [SerializeField] public GameObject _shootOffset;
    [Space(10)]
    [Header("Variables")]
    [SerializeField] public float _actionRange = 1.0f;

    [SerializeField] private GameObject[] nearest = null;
    [SerializeField] private GameObject bestTarget = null;

    //sc_Shooting scs;
    [HideInInspector] public sc_SpellAlchemist s_sa;
    sc_Gem_ID id;

    // Start is called before the first frame update
    void Start()
    {
        //scs = GetComponent<sc_Shooting>();
        s_sa = GetComponent<sc_SpellAlchemist>();
    }

    // Update is called once per frame
    void Update()
    {
        nearest = null;
        nearest = GameObject.FindGameObjectsWithTag("Gem");
        bestTarget = GetClosest();
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
        if (bestTarget != null)
        {
            id = bestTarget.GetComponent<sc_Gem_ID>();

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
        }
    }

    public void GetSlot(string _slot)
    {
        if (_slot == "X" && id._type == "P" && bestTarget.GetComponent<sc_Gem_ID>()._type == "P")
        {
            if (_Xempty == false)
            {
                SwitchGems(_previous_x_Gem, bestTarget.transform);//On echange les gemmes
            }

            SpellAttribution(_slot);//On la place où il faut
            _xPassive = _newPassive;
            _Xempty = false;
        }
        if (_slot == "Y" && id._type == "P" && bestTarget.GetComponent<sc_Gem_ID>()._type == "P")
        {
            if (_Yempty == false)
            {
                SwitchGems(_previous_y_Gem, bestTarget.transform);//On echange les gemmes
            }

            SpellAttribution(_slot);//On la place où il faut
            _yPassive = _newPassive;
            _Yempty = false;
        }
        if (_slot == "LB" && id._type == "A" && bestTarget.GetComponent<sc_Gem_ID>()._type == "A")
        {
            if (_lbEmpty == false)
            {
                SwitchGems(_previous_lb_Gem, bestTarget.transform);//On echange les gemmes
            }

            SpellAttribution(_slot);//On la place où il faut
            _lbActive = _newActive;
            _lbEmpty = false;
        }
        if (_slot == "RB" && id._type == "A" && bestTarget.GetComponent<sc_Gem_ID>()._type == "A")
        {
            if (_rbEmpty == false)
            {
                SwitchGems(_previous_rb_Gem, bestTarget.transform);//On echange les gemmes
            }

            SpellAttribution(_slot);//On la place où il faut
            _rbActive = _newActive;
            _rbEmpty = false;
        }
    }

    public void SwitchGems(GameObject g, Transform pos)
    {
        g.transform.position = pos.position;
        g.transform.rotation = pos.rotation;

        g.SetActive(true);
        //
        //Debug.Log("On a remplacé le biniou !");
        //
    }

    public void SpellAttribution(string s)
    {
        if (bestTarget.GetComponent<sc_Gem_ID>()._type == "A")
        {
            if (bestTarget.GetComponent<sc_Gem_ID>()._preEstablishedSpell != null)
            {
                _newActive = bestTarget.GetComponent<sc_Gem_ID>()._preEstablishedSpell;
            }
            else
            {
                _newActive = bestTarget.GetComponent<sc_Gem_ID>().RandomSelectedSpell();
            }

            if (s == "LB")
                _previous_lb_Gem = bestTarget;
            if (s == "RB")
                _previous_rb_Gem = bestTarget;

            bestTarget.SetActive(false);
        }
        if (bestTarget.GetComponent<sc_Gem_ID>()._type == "P")
        {
            if (bestTarget.GetComponent<sc_Gem_ID>()._preEstablishedSpell != null)
            {
                _newPassive = bestTarget.GetComponent<sc_Gem_ID>()._preEstablishedSpell;
            }
            else
            {
                _newPassive = bestTarget.GetComponent<sc_Gem_ID>().RandomSelectedSpell();
            }

            if (s == "X")
                _previous_x_Gem = bestTarget;
            if (s == "Y")
                _previous_y_Gem = bestTarget;

            bestTarget.SetActive(false);
        }
    }

    GameObject GetClosest()
    {
        GameObject newGo = null;

        foreach (GameObject g in nearest)//_agl.Gem
        {
            if (g.activeSelf == true)
            {
                Vector3 directionToTarget = g.transform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < _actionRange)
                {
                    newGo = g;
                    return newGo;
                }
            }
        }

        return newGo;
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
                s_sa.Shoot(_rbActive, _yPassive, _xPassive, "RT");
                //scs.Shoot(_rbActive, _yPassive, _xPassive, "RT");
        }
        if (_slot == "LT")
        {
            if (_lbActive != null)
                s_sa.Shoot(_lbActive, _yPassive, _xPassive, "LT");
                //scs.Shoot(_lbActive, _yPassive, _xPassive, "LT");
        }
    }
}
