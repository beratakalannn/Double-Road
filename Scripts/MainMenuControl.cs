using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MainMenuControl : MonoBehaviour
{
    public GameObject heroMenu;
    public Text starScoreText;
    public Image musicImg;
    public Sprite musicOff, musicOn;


    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void HeroMenu()
    {
        heroMenu.SetActive(true);
        starScoreText.text = "" + GameManager.instance.starScore;
    }

    public void HomeButton()
    {
        heroMenu.SetActive(false); 
    }

    public void MusicButton()
    {
        if (GameManager.instance.playSound)
        {
            musicImg.sprite = musicOff;
            GameManager.instance.playSound = false;
        }
        else
        {
            musicImg.sprite = musicOn;
            GameManager.instance.playSound = true;

        }
    }


}
