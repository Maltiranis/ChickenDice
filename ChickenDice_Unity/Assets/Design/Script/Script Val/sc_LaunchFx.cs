using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LaunchFx : MonoBehaviour
{
    public GameObject[] _FXs;
    public float _fxLifetime = 2f;
    public GameObject _rootFX;

    public void LaunchFx (int i)
    {
        GameObject newFx = Instantiate(_FXs[i], _rootFX.transform.position, _rootFX.transform.rotation);

        Destroy(newFx, _fxLifetime);
    }
}
