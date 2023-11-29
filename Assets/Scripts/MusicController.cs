using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Animator filteredMusicAnimator;
    [SerializeField] private Animator normalMusicAnimator;

    private string state = "Filtered";

    public string State { get => state; set => state = value; }

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
