using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource move, jump, bg, pw;
    public AudioClip coin, die, gameOver;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (GameManager.instance.playSound)
        {
            bg.Play();
        }
        
    }

    void Update()
    {
        
    }

    public void Bg()
    {
        bg.Play();
    }


    public void PlayMoveLineSound()
    {
        move.Play();
    }

    public void PlayJumpSound()
    {
        jump.Play();
    }

    public void DieSound()
    {
        pw.clip = die;
        pw.Play();
    }

    public void PlayCoinSound()
    {
        pw.clip = coin;
        pw.Play();
    }

    public void GameOver()
    {
        bg.clip = gameOver;
        bg.Stop();

        bg.loop = false;
        bg.Play();

    }
}
