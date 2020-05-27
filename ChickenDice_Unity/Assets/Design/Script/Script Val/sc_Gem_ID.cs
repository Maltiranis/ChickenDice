using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_Gem_ID : MonoBehaviour
{
    [SerializeField] public string _type = ""; // Active = A ; Passive = P
    [SerializeField] public int _rarity = 0; // Rareté de 0 à 2

    [SerializeField] public GameObject[] _spells;

    // Start is called before the first frame update
    void Start()
    {

    }

    public int SpellInCollection () // Le Spell en question
    {
        int r;

        r = Random.Range(0, _spells.Length);

        Debug.Log("Le spell n°= " + r + " sur le max : " + _spells.Length);

        return r;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
