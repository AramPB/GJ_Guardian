using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image character;
    [SerializeField] private GameObject credits;

    private void Start()
    {
        int randomPos = Random.Range(0, sprites.Count - 1);
        character.sprite = sprites[randomPos];
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void HideCredits()
    {
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
