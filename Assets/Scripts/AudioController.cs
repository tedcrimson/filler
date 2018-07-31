using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    // Use this for initialization

    public List<AudioClip> HitSounds;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    public AudioSource AS;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayHitSound(State state)
    {
        AS.PlayOneShot(HitSounds[(int)state + 1]);
    }

}
