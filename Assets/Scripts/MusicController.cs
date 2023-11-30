using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Animator filteredMusicAnimator;
    [SerializeField] private Animator normalMusicAnimator;

    [SerializeField] private AudioSource filteredMusicAS;
    [SerializeField] private AudioSource normalMusicAS;

    [SerializeField] private List<AudioClip> NormalMusics;
    [SerializeField] private List<AudioClip> FilteredMusics;

    private string state = "Filtered";
    private int newNight;

    public static MusicController Instance { get; private set; }

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

    public string State { get => state; set => state = value; }

    public void ChangeSong(int night)
    {
        newNight = night;
        if (state == "Filtered")
        {
            filteredMusicAnimator.Play("FilteredMusicFadeOut");
        }
        else
        {
            normalMusicAnimator.Play("NormalMusicFadeOut");
        }
        Invoke("StartNewSong", 1);
    }

    private void StartNewSong()
    {
        filteredMusicAS.clip = FilteredMusics[newNight - 1];
        normalMusicAS.clip = NormalMusics[newNight - 1];
        filteredMusicAnimator.Play("FilteredMusicFadeIn");
    }

    public void startNormalMusic()
    {
        filteredMusicAnimator.Play("FilteredMusicFadeOut");
        normalMusicAnimator.Play("NormalMusicFadeIn");
        State = "Normal";
    }

    public void startFilteredMusic()
    {
        filteredMusicAnimator.Play("FilteredMusicFadeIn");
        normalMusicAnimator.Play("NormalMusicFadeOut");
        State = "Filtered";
    }


}
