using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip campBGM1;
    [SerializeField] private AudioClip fieldBGM;
    [SerializeField] private AudioClip campBGM2;

    // Start is called before the first frame update
    void Start()
    {
         PlayCamp1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayField()
    {
        audioSource.clip = fieldBGM;
        audioSource.Play();
    }

    public void PlayCamp1()
    {
        audioSource.clip = campBGM1;
        audioSource.Play();
    }

    public void PlayCamp2()
    {
        audioSource.clip = campBGM2;
        audioSource.Play();
    }
}
