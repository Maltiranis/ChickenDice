using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] private float _LifeTime = 0f;
    [SerializeField] public int _CrackLife = 0;
    [SerializeField] private Transform _SpawnGem = null;
    //Variables cachées
    private int _CrackLifeBase;
    private bool _CanbeDesactive = true;
    [SerializeField] private bool _RarityDef = false;

    //Rareté Variable
    [Header("Variable automatique")]
    [SerializeField] private int _RarityPick;
    [SerializeField] private GameObject _GemToLoot = null;
    [SerializeField] private GameObject _Vfx30RarityToShow = null;
    [SerializeField] private GameObject _Vfx60RarityToShow = null;


    [Header("Commun Gem")]
    [SerializeField] private int _RangeCommun = 0;
    [SerializeField] private GameObject _VFXCommun30 = null;
    [SerializeField] private GameObject _VFXCommun60 = null;
    [SerializeField] private GameObject[] _CommunGem = null;
    [Header("Rare Gem")]
    [SerializeField] private int _RangeRare = 0;
    [SerializeField] private GameObject _VFXRare30 = null;
    [SerializeField] private GameObject _VFXRare60 = null;
    [SerializeField] private GameObject[] _RareGem = null;
    [Header("Unique Gem")]
    [SerializeField] private int _RangeLegendary = 0;
    [SerializeField] private GameObject _VFXLeg30 = null;
    [SerializeField] private GameObject _VFXLeg60 = null;
    [SerializeField] private GameObject[] _LegendaryGem = null;


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

        if(_CrackLife !=_CrackLifeBase)
        {
            VfxToShow();

            if(_RarityDef == false)
            {
                _RarityDef = true;
                RarityToLoot();
            }
        }
        if(_CrackLife <= 0)
        {
            LootGem();
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
        _RarityPick = Random.Range(0, _RangeLegendary);

        if(_RarityPick <= _RangeCommun)
        {
            _Vfx30RarityToShow = _VFXCommun30;
            _Vfx60RarityToShow = _VFXCommun60;
            int _RandomRange = Random.Range(0, _CommunGem.Length);
            Debug.Log(_CommunGem[_RandomRange]);
            _GemToLoot = _CommunGem[_RandomRange];
        }
        if(_RarityPick > _RangeCommun && _RarityPick <= _RangeRare)
        {
            _Vfx30RarityToShow = _VFXRare30;
            _Vfx60RarityToShow = _VFXRare60;
            int _RandomRange = Random.Range(0, _RareGem.Length);
            Debug.Log(_RareGem[_RandomRange]);
            _GemToLoot = _RareGem[_RandomRange];
            
        }
        if(_RarityPick > _RangeRare)
        {
            _Vfx30RarityToShow = _VFXLeg30;
            _Vfx60RarityToShow = _VFXLeg60;
            int _RandomRange = Random.Range(0, _LegendaryGem.Length);
            Debug.Log(_LegendaryGem[_RandomRange]);
            _GemToLoot = _LegendaryGem[_RandomRange];
           
        }


    }
    private void VfxToShow()
    {
        if (_CrackLife <=_CrackLifeBase *70/100 && _CrackLife > _CrackLifeBase *40/100 && !_Vfx30RarityToShow.activeSelf)
        {
            _Vfx30RarityToShow.SetActive(true);
        }

        if(_CrackLife<= _CrackLifeBase *40 / 100 && !_Vfx60RarityToShow.activeSelf)
        {
            _Vfx30RarityToShow.SetActive(false);
            _Vfx60RarityToShow.SetActive(true);
        }
    }
    private void LootGem()
    {
        Instantiate(_GemToLoot, _SpawnGem.position, _SpawnGem.rotation);
    }
}
    