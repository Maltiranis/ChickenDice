using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _CrackToSpawn = null;
    [SerializeField] private float _TimeBetweenCrack = 0f;
    [SerializeField] private int _RandomCrack = 0;


    private void Start()
    {
        StartCoroutine(LoopSpawn());
    }

    private IEnumerator LoopSpawn()
    {
        yield return new WaitForSeconds(_TimeBetweenCrack);
        SpawnCrackFonction();
        StartCoroutine(LoopSpawn());
    }


    private void SpawnCrackFonction()
    {
        _RandomCrack = Random.Range(0, _CrackToSpawn.Length);

        Debug.Log(_RandomCrack);

        if (!_CrackToSpawn[_RandomCrack].activeSelf)
        {
            _CrackToSpawn[_RandomCrack].SetActive(true);
        }
        else
        {
            SpawnCrackFonction();
        }
    }
}
