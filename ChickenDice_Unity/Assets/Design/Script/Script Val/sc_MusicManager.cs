using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_MusicManager : MonoBehaviour
{
    List<GameObject> musics = new List<GameObject>();
    public int _musicSelected = 0;
    int newMusicSelected = 10000;
    [TextArea(5, 100)]
    public string MusicList = "";

    // Start is called before the first frame update
    void Start()
    {
        DynamicList(_musicSelected);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_musicSelected != newMusicSelected)
        {
            newMusicSelected = _musicSelected;
            if (_musicSelected <= transform.childCount)
            {
                PlayNewMusic(_musicSelected);
            }
            else
            {
                return;
            }
        }
    }

    public void DynamicList (int playing)
    {
        MusicList = "";
        for (int i = 0; i < transform.childCount; i++)
        {
            musics.Add(transform.GetChild(i).gameObject);
            if (playing == i)
            {
                MusicList += "Track_" + i.ToString() + " : " + transform.GetChild(i).gameObject.name + " <-" + System.Environment.NewLine;
            }
            else
            {
                MusicList += "Track_" + i.ToString() + " : " + transform.GetChild(i).gameObject.name + System.Environment.NewLine;
            }
        }
    }

    public void PlayNewMusic(int music)
    {
        foreach (GameObject m in musics)
        {
            m.SetActive(false);
        }
        DynamicList(music);
        musics[music].SetActive(true);
    }
}

/*
// TRAVAUX SUR LE SMOOTH
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sc_MusicManager : MonoBehaviour
{
    List<GameObject> musics = new List<GameObject>();
    public int _musicSelected = 0;
    int newMusicSelected = 10000;
    [TextArea(10, 100)]
    public string MusicList = "";

    float getVol = 0f;
    float wantedVol = 0f;

    // Start is called before the first frame update
    void Start()
    {
        DynamicList(_musicSelected + 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_musicSelected != newMusicSelected)
        {
            newMusicSelected = _musicSelected;
            if (_musicSelected <= transform.childCount)
            {
                Smoother();
            }
            else
            {
                return;
            }
        }
    }

    public void DynamicList (int playing)
    {
        if (playing == 0)
        {
            MusicList = "No Track" + " <-" + System.Environment.NewLine;
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            musics.Add(transform.GetChild(i).gameObject);
            if (playing - 1 == i)
            {
                MusicList += "Track_" + i + 1.ToString() + " : " + transform.GetChild(i).gameObject.name + " <-" + System.Environment.NewLine;
            }
            else
            {
                MusicList += "Track_" + i + 1.ToString() + " : " + transform.GetChild(i).gameObject.name + System.Environment.NewLine;
            }
        }
    }

    public void PlayNewMusic(int music)
    {
        foreach (GameObject m in musics)
        {
            m.SetActive(false);
        }
        DynamicList(music + 1);
        musics[music].SetActive(true);
    }

    public void Smoother()
    {
        foreach (GameObject m in musics)
        {
            if (m.activeSelf == true)
            {
                StartCoroutine(smoothSound(m.GetComponent<AudioSource>()));
            }
            else
            {
                m.GetComponent<AudioSource>().volume = 0f;
            }
        }
    }

    public IEnumerator smoothSound(AudioSource s)
    {
        yield return new WaitForSeconds(0.1f);

        getVol = s.volume;

        if(s.volume)
        PlayNewMusic(_musicSelected);
    }
}
*/