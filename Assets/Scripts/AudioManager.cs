using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

enum AudioType
{
    Effect,
    Music
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private GameObject audioSourcePrefab;

    [SerializeField] private AudioMixerGroup groupEffect;
    [SerializeField] private AudioMixerGroup groupMusic;
    GameObject ActiveOst;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("trovato gia un AudioManager nella scena");
        }
        else
        {
            instance = this;

        }
    }

    /// <summary>
    /// Play effect audio
    /// </summary>
    /// <param name="clipToPlay"></param>
    public void PlayAudioClip(AudioClip clipToPlay, Vector3 position)
    {
        GameObject tempAudioClip=Instantiate(audioSourcePrefab, position, Quaternion.identity);

        tempAudioClip.GetComponent<AudioSource>().clip = clipToPlay;

        tempAudioClip.GetComponent <AudioSource>().outputAudioMixerGroup = groupEffect;

        tempAudioClip.GetComponent<AudioSource>().Play();

        Destroy(tempAudioClip, clipToPlay.length);
    }

    //public void PlayOstClip(AudioClip clipToPlay)
    //{
    //    GameObject tempOstClip = Instantiate(audioSourcePrefab);
    //    tempOstClip.gameObject.name = "OstAudio";
    //    tempOstClip.GetComponent<AudioSource>().clip = clipToPlay;
    //    tempOstClip.GetComponent<AudioSource>().loop = true;

    //    tempOstClip.GetComponent<AudioSource>().outputAudioMixerGroup = groupMusic;

    //    tempOstClip.GetComponent<AudioSource>().Play();

        
    //}

    public GameObject GetActiveOst()
    {
        return ActiveOst;
    }

    public void SetActiveOst(GameObject ost)
    {
        ActiveOst=ost;
    }

    

}
