using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] private float _LifeTime = 0f;
    [SerializeField] public int _CrackLife = 0;
    //[SerializeField] private Collider _CrackCollider = null;

    //Variables cachées
    private int _CrackLifeBase;
    private bool _CanbeDesactive = true;
    [SerializeField] private bool _RarityDef = false;

    //Rareté Variable
    [Header("Variable automatique")]
    [SerializeField] private int _RarityPick;

    [Header("Commun Gem")]
    [SerializeField] private int _RangeCommun = 0;
    [SerializeField] private GameObject[] _CommunGem = null;
    [Header("Rare Gem")]
    [SerializeField] private int _RangeRare = 0;
    [SerializeField] private GameObject[] _RareGem = null;
    [Header("Unique Gem")]
    [SerializeField] private int _RangeUnique = 0;
    [SerializeField] private GameObject[] _UniqueGem = null;


    private void Start()
    {
        _CrackLifeBase = _CrackLife;
    }

    void Update()
    {
        if (gameObject.activeSelf && _CanbeDesactive == true)
        {
            StartCoroutine(Lifeduration());
            _CanbeDesactive = false;
        }

        if(_CrackLife !=_CrackLifeBase && _RarityDef == false)
        {
            _RarityDef = true;
            RarityToLoot();
        }

        if(_CrackLife <= 0)
        {
            _RarityDef = false;
            _CanbeDesactive = true;
            _CrackLife = _CrackLifeBase;
            gameObject.SetActive(false);
        }
    }

    // LIFE DURATION 
    private IEnumerator Lifeduration()
    {
        yield return new WaitForSeconds(_LifeTime);
        _CanbeDesactive = true;
        gameObject.SetActive(false);
    }

    //DEF DE LA RARETE ET DE LA GEM A LOOT
    private void RarityToLoot()
    {
        _RarityPick = Random.Range(0, _RangeUnique);

        if(_RarityPick <= _RangeCommun)
        {
            int _RandomRange = Random.Range(0, _CommunGem.Length);
            Debug.Log(_CommunGem[_RandomRange]);
        }
        if(_RarityPick > _RangeCommun && _RarityPick <= _RangeRare)
        {
            int _RandomRange = Random.Range(0, _RareGem.Length);
            Debug.Log(_RareGem[_RandomRange]);
        }
        if(_RarityPick > _RangeRare)
        {
            int _RandomRange = Random.Range(0, _UniqueGem.Length);
            Debug.Log(_UniqueGem[_RandomRange]);
        }


    }


/*
    //CHANGER UNE FOIS RELIER AU SCRIPT PLAYER
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("atouchéqlqlchose");

        if(other.name == "Player")
        {
            //Debug.Log("areconulenomjoueur");

            _CrackLife = _CrackLife-1;
        }
    }

    */

}
    