using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    [Header("Prefab Players")]
    [SerializeField] private GameObject _Player01 = null;
    [SerializeField] private GameObject _Player02 = null;
    [SerializeField] private GameObject _Player03 = null;
    [SerializeField] private GameObject _Player04 = null;

    [Header("Prefab Arena")]
    [SerializeField] private GameObject _LootArena = null;
    [SerializeField] private GameObject _FightArena = null;

    [Header("Selection du mode")]
    [SerializeField] private bool _Mode01 = false;
    [SerializeField] private bool _Mode02 = false;

    [Header("Cover")]
    [SerializeField] private GameObject _Cover01 = null;

    [Header("Time Phase")]
    [SerializeField] private int _TimeLootPhase = 0;
    [SerializeField] private int _TimeFightPhase = 0;

    [Header("Spawn Player ArenaFight01")]
    [SerializeField] private Transform _SpawnFightP1 = null;
    [SerializeField] private Transform _SpawnFightP2 = null;
    [SerializeField] private Transform _SpawnFightP3 = null;
    [SerializeField] private Transform _SpawnFightP4 = null;

    [Header("Camera")]
    [SerializeField] private GameObject _LootCam = null;
    [SerializeField] private GameObject _FightCam = null;

    private void Start()
    {
        StartCoroutine(LootPhaseTimer());
    }

    private IEnumerator LootPhaseTimer()
    {
        yield return new WaitForSeconds(_TimeLootPhase);
        _LootCam.SetActive(false);
        _FightCam.SetActive(true);
        _LootArena.SetActive(false);
        StartCoroutine(FightPhaseTimer());


        if(_Mode01 == true)
        {
            _Cover01.SetActive(true);

            _Player01.transform.position = _SpawnFightP1.transform.position;
            _Player01.transform.rotation = _SpawnFightP1.transform.rotation;

            _Player02.transform.position = _SpawnFightP2.transform.position;
            _Player02.transform.rotation = _SpawnFightP2.transform.rotation;

            _Player03.transform.position = _SpawnFightP3.transform.position;
            _Player03.transform.rotation = _SpawnFightP3.transform.rotation;

            _Player04.transform.position = _SpawnFightP4.transform.position;
            _Player04.transform.rotation = _SpawnFightP4.transform.rotation;

        }


    }
    private IEnumerator FightPhaseTimer()
    {
        yield return new WaitForSeconds(_TimeFightPhase);
        SceneManager.LoadScene("Scenes_Jeff");
    }

}
