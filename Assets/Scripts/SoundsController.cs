using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] public AudioSource buttonSound;
    [SerializeField] public AudioSource acceptSound;
    [SerializeField] public AudioSource declineSound;
    [SerializeField] public AudioSource criminalSound;
    [SerializeField] public AudioSource scannSound;

    public static SoundsController Instance { get; private set; }

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion

    public void buttonSoundPlay()
    {
        buttonSound.Play();
    }

    public void acceptSoundPlay()
    {
        acceptSound.Play();
    }

    public void declineSoundPlay()
    {
        declineSound.Play();
    }

    public void criminalSoundPlay()
    {
        criminalSound.Play();
    }

    public void scannSoundPlay()
    {
        scannSound.Play();
    }
}
