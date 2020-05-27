﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Gem_ID : MonoBehaviour
{
    [SerializeField] public string _type = ""; // Active = A ; Passive = P
    [SerializeField] public int _rarity = 0; // Rareté de 0 à 2
    [HideInInspector]
    [SerializeField] public GameObject[] _spells;
    [HideInInspector]
    [SerializeField] public GameObject _preEstablishedSpell = null;

    [SerializeField] public GameObject _ParentGemContainer;

    // Start is called before the first frame update
    void Start()
    {
        _spells = GameObject.Find("SkillBoard").GetComponent<sc_SkillBoard>()._spells;
    }

    public int SpellInCollection () // Le Spell en question
    {
        int r;

        r = Random.Range(0, _spells.Length);

        Debug.Log("Le spell n°= " + r + " sur le max : " + _spells.Length);

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
