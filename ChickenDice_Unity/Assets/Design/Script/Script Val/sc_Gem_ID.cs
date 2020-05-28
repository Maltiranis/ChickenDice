using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Gem_ID : MonoBehaviour
{
    [SerializeField] public string _type = ""; // Active = A ; Passive = P
    [SerializeField] public int _rarity = 0; // Rareté de 0 à 2
    [HideInInspector]
    [SerializeField] public GameObject[] _spells;
    sc_SkillBoard scs;
    [HideInInspector]
    [SerializeField] public GameObject _preEstablishedSpell = null;

    // Start is called before the first frame update
    void Start()
    {
        scs = GameObject.Find("SkillBoard").GetComponent<sc_SkillBoard>();
        GetSpell();
    }

    void GetSpell ()
    {
        if (_type == "A")
        {
            switch (_rarity)
            {
                case 0:
                    _spells = scs._comunActiveSpells;
                    break;
                case 1:
                    _spells = scs._rareActiveSpells;
                    break;
                case 2:
                    _spells = scs._legActiveSpells;
                    break;
                //-----------------//
                default:
                    break;
            }
        }
        if (_type == "P")
        {
            switch (_rarity)
            {
                case 0:
                    _spells = scs._comunPassiveSpells;
                    break;
                case 1:
                    _spells = scs._rarePassiveSpells;
                    break;
                case 2:
                    _spells = scs._legPassiveSpells;
                    break;
                //-----------------//
                default:
                    break;
            }
        }
    }

    public int SpellInCollection () // Le Spell en question
    {
        int r;

        r = Random.Range(0, _spells.Length);

        Debug.Log("Le spell n°= " + (r + 1) + " sur le max : " + _spells.Length);

        return r;
    }

    public GameObject RandomSelectedSpell()
    {
        GameObject rss;

        rss = _spells[SpellInCollection()];

        return rss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
