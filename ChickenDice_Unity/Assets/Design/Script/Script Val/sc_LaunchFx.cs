using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_LaunchFx : MonoBehaviour
{
    public GameObject[] _FXs; 
    public GameObject[] _Sounds; 
    public float _fxLifetime = 2f;
    public GameObject _rootFX;
    public GameObject _rootHead;

    public GameObject[] _eyes;
    public GameObject[] _dustRun;

    private void Start()
    {
        foreach (GameObject d in _dustRun)
        {
            d.SetActive(false);
        }
        foreach (GameObject e in _eyes)
        {
            e.SetActive(false);
        }

        //code hyper crade
        _dustRun[transform.parent.parent.gameObject.GetComponent<sc_Chicken_ID>().ID].SetActive(true);

    }

    public void LaunchFx(int i)
    {
        GameObject newFx = Instantiate(_FXs[i], _rootFX.transform.position, _rootFX.transform.rotation);
        if (i == 4)
        {
            /*GameObject clonedParticle = Instantiate(_FXs[i].transform.GetChild(0).gameObject, _rootFX.transform.position, _rootFX.transform.rotation);
            clonedParticle.transform.parent = gameObject.transform;
            Destroy(clonedParticle, _fxLifetime);
            newFx.transform.GetChild(1).gameObject.SetActive(false);*/
        }

        Destroy(newFx, _fxLifetime);
    }

    public void LaunchSounds(int i)
    {
        GameObject newSound = Instantiate(_Sounds[i], _rootFX.transform.position, _rootFX.transform.rotation);

        Destroy(newSound, _fxLifetime);
    }

    public void SetEye (int e)//0 : mort , 1 : pleure, 2 : plisse
    {
        GameObject newEyeFx = Instantiate(_eyes[e], _rootHead.transform.position, _rootHead.transform.rotation);
        newEyeFx.transform.SetParent(_rootHead.transform);
        newEyeFx.SetActive(true);
        newEyeFx.GetComponent<ParticleSystem>().Play();


        int eyeChilds = newEyeFx.transform.childCount;

        List<GameObject> eyeList = new List<GameObject>();

        for (int i = 0; i < eyeChilds; i++)
        {
            eyeList.Add(newEyeFx.transform.GetChild(i).gameObject);
        }

        foreach (GameObject g in eyeList)
        {
            g.GetComponent<ParticleSystem>().Play();
        }

        StartCoroutine(TimedDestroyAndCleanList(newEyeFx, _fxLifetime, eyeList));
    }

    public IEnumerator TimedDestroyAndCleanList(GameObject g, float time, List<GameObject> l)
    {
        yield return new WaitForSeconds(time);

        l.Clear();
        Destroy(g);
    }
}
