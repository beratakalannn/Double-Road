using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScrpit : MonoBehaviour
{
    private Animator anim;
    private string walkanim = "PlayerWalk";

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void PlayerWalk()
    {
        anim.Play(walkanim);

        if (PlayerController.instance.playerJump)
        {
            PlayerController.instance.playerJump = false;
        }
    }

    public void PausePanelClose()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

   
}
