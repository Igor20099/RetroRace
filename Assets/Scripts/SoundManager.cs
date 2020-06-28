using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip put;
    [SerializeField] private AudioClip destroy;
    [SerializeField] private AudioSource audioSource;
    public static SoundManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShot()
    {
      audioSource.PlayOneShot(shot);
    }
    public void PlayPut()
    {
        audioSource.PlayOneShot(put);
    }

    public void PlayDestroy()
    {
        audioSource.PlayOneShot(destroy);
    }
}
