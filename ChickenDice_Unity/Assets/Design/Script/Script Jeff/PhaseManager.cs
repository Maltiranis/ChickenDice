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
    [SerializeField] private int _IndexScarcrow01 = 0;
    [SerializeField] private int _IndexScarcrow02 = 0;
    [SerializeField] private int _ModeWin = 0;


    [Header("Time Phase")]
    [SerializeField] private int _TimeLootPhase = 0;
    [SerializeField] private int _TimeFightPhase = 0;

    [Header("Cover")]
    [SerializeField] private GameObject _CoverA101 = null;
    [SerializeField] private GameObject _CoverA201 = null;
    [SerializeField] private GameObject _CoverA301 = null;

    [Header("Spawn Player ArenaFight01")]
    [SerializeField] private Transform _SpawnFightA1P1 = null;
    [SerializeField] private Transform _SpawnFightA1P2 = null;
    [SerializeField] private Transform _SpawnFightA1P3 = null;
    [SerializeField] private Transform _SpawnFightA1P4 = null;

    [Header("Spawn Player ArenaFight02")]
    [SerializeField] private Transform _SpawnFightA2P1 = null;
    [SerializeField] private Transform _SpawnFightA2P2 = null;
    [SerializeField] private Transform _SpawnFightA2P3 = null;
    [SerializeField] private Transform _SpawnFightA2P4 = null;

    [Header("Spawn Player ArenaFight02")]
    [SerializeField] private Transform _SpawnFightA3P1 = null;
    [SerializeField] private Transform _SpawnFightA3P2 = null;
    [SerializeField] private Transform _SpawnFightA3P3 = null;
    [SerializeField] private Transform _SpawnFightA3P4 = null;

    [Header("Camera")]
    [SerializeField] private GameObject _LootCam = null;
    [SerializeField] private GameObject _FightCam = null;

    [Header("Scarcrow")]
    [SerializeField] private GameObject _Scarcrow01 = null;
    [SerializeField] private GameObject _Scarcrow02 = null;

    private void Awake()
    {
        SetupScarcrow();
    }

    private void Start()
    {
        StartCoroutine(LootPhaseTimer());
    }

    private void SetupScarcrow()
    {
        if(_IndexScarcrow01 == 0)
        {
            _IndexScarcrow01 = Random.Range(1, 4);
        }
        _IndexScarcrow02 = Random.Range(1, 4);

        if(_IndexScarcrow01 == _IndexScarcrow02)
        {
            SetupScarcrow();
        } else
        {
            _Scarcrow01.GetComponent<ScarecrowMode>()._ModeSelect = _IndexScarcrow01;
            _Scarcrow02.GetComponent<ScarecrowMode>()._ModeSelect = _IndexScarcrow02;
        }
    }

    private IEnumerator LootPhaseTimer()
    {
        yield return new WaitForSeconds(_TimeLootPhase);
        _LootCam.SetActive(false);
        _FightCam.SetActive(true);
        _LootArena.SetActive(false);
        SelectMode();
        StartCoroutine(FightPhaseTimer());
    }

    private void SelectMode()
    {
        int CompteurSC01 = _Scarcrow01.GetComponent<ScarecrowMode>()._PeckCounter;
        int CompteurSC02 = _Scarcrow02.GetComponent<ScarecrowMode>()._PeckCounter;
       
        if (CompteurSC01 >= CompteurSC02)
        {
            _ModeWin = _IndexScarcrow01;
        }
        else
        {
            _ModeWin = _IndexScarcrow02;
        }

        SetupMode();
    }

    private void SetupMode()
    {
        Transform _ChangeSpawnP01 = _Player01.GetComponent<sc_LifeEngine>()._startPosTransform;
        Transform _ChangeSpawnP02 = _Player02.GetComponent<sc_LifeEngine>()._startPosTransform;
        Transform _ChangeSpawnP03 = _Player03.GetComponent<sc_LifeEngine>()._startPosTransform;
        Transform _ChangeSpawnP04 = _Player04.GetComponent<sc_LifeEngine>()._startPosTransform;

        if(_ModeWin == 1)
        {
            _CoverA101.SetActive(true);

            _ChangeSpawnP01.position = _SpawnFightA1P1.transform.position;
            _Player01.transform.position = _SpawnFightA1P1.transform.position;
            _Player01.transform.rotation = _SpawnFightA1P1.transform.rotation;

            _ChangeSpawnP02.position = _SpawnFightA1P2.transform.position;
            _Player02.transform.position = _SpawnFightA1P2.transform.position;
            _Player02.transform.rotation = _SpawnFightA1P2.transform.rotation;

            _ChangeSpawnP03.position = _SpawnFightA1P3.transform.position;
            _Player03.transform.position = _SpawnFightA1P3.transform.position;
            _Player03.transform.rotation = _SpawnFightA1P3.transform.rotation;

            _ChangeSpawnP04.position = _SpawnFightA1P4.transform.position;
            _Player04.transform.position = _SpawnFightA1P4.transform.position;
            _Player04.transform.rotation = _SpawnFightA1P4.transform.rotation;

        }
        if(_ModeWin == 2)
        {
            _CoverA201.SetActive(true);

            _ChangeSpawnP01.position = _SpawnFightA2P1.transform.position;
            _Player01.transform.position = _SpawnFightA2P1.transform.position;
            _Player01.transform.rotation = _SpawnFightA2P1.transform.rotation;

            _ChangeSpawnP02.position = _SpawnFightA2P2.transform.position;
            _Player02.transform.position = _SpawnFightA2P2.transform.position;
            _Player02.transform.rotation = _SpawnFightA2P2.transform.rotation;

            _ChangeSpawnP03.position = _SpawnFightA2P3.transform.position;
            _Player03.transform.position = _SpawnFightA2P3.transform.position;
            _Player03.transform.rotation = _SpawnFightA2P3.transform.rotation;

            _ChangeSpawnP04.position = _SpawnFightA2P4.transform.position;
            _Player04.transform.position = _SpawnFightA2P4.transform.position;
            _Player04.transform.rotation = _SpawnFightA2P4.transform.rotation;
        }
        if(_ModeWin == 3)
        {
            _CoverA301.SetActive(true);

            _ChangeSpawnP01.position = _SpawnFightA3P1.transform.position;
            _Player01.transform.position = _SpawnFightA3P1.transform.position;
            _Player01.transform.rotation = _SpawnFightA3P1.transform.rotation;

            _ChangeSpawnP02.position = _SpawnFightA3P2.transform.position;
            _Player02.transform.position = _SpawnFightA3P2.transform.position;
            _Player02.transform.rotation = _SpawnFightA3P2.transform.rotation;

            _ChangeSpawnP03.position = _SpawnFightA3P3.transform.position;
            _Player03.transform.position = _SpawnFightA3P3.transform.position;
            _Player03.transform.rotation = _SpawnFightA3P3.transform.rotation;

            _ChangeSpawnP04.position = _SpawnFightA3P4.transform.position;
            _Player04.transform.position = _SpawnFightA3P4.transform.position;
            _Player04.transform.rotation = _SpawnFightA3P4.transform.rotation;
        }

    }

    private IEnumerator FightPhaseTimer()
    {
        yield return new WaitForSeconds(_TimeFightPhase);
        SceneManager.LoadScene("Scenes_Jeff");
    }

}
