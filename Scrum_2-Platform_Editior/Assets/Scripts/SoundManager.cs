using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip shootSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playJumpSound();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            playShootSound();
        }
    }

    public void playJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void playShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
}