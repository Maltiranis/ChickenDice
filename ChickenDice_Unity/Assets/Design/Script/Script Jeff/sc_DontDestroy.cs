using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
