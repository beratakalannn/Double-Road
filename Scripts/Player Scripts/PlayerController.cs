using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private Animator anim;

    public GameObject player, shadow;
    public Vector3 firstPosPlayer, secondPosPlayer;

    [HideInInspector]
    public bool playerDied;
    [HideInInspector]
    public bool playerJump;

    public GameObject explosion;
    private SpriteRenderer player_Renderer;
    public Sprite trex_Sprite, player_Sprite;
    private bool trex_Trigger;
    private GameObject[] star_Effect;




    private void Awake()
    {
        instance = this;
        anim = player.GetComponent<Animator>();
        player_Renderer = player.GetComponent<SpriteRenderer>();
        star_Effect = GameObject.FindGameObjectsWithTag(MyTags.STAR_EFFECT);

    }

    void Start()
    {
        if (!playerDied)
        {
            SoundManager.instance.Bg();
        }

        string path = "Sprites/Player/hero" + GameManager.instance.selectedIndex + "_big";
        player_Sprite = Resources.Load<Sprite>(path);
        player_Renderer.sprite = player_Sprite;

    }

    void Update()
    {
    }


    public void HandleChangeLineRight()
    {
        
      //anim.Play(changelineAnimation);
      transform.localPosition = secondPosPlayer;
        SoundManager.instance.PlayMoveLineSound();
    }

    public void HandleChangeLineLeft()
    {

        //anim.Play(changelineAnimation);
        transform.localPosition = firstPosPlayer;
        SoundManager.instance.PlayMoveLineSound();
         
    }

    public void HandleJump()
    {
        
           if (!playerJump)
            {
            anim.Play("Jump");

            playerJump = true;
            SoundManager.instance.PlayJumpSound();
            
            }
    }


    void Die()
    {
        playerDied = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GamePlayController.instance.moveSpeed = 0;
        GamePlayController.instance.GameOverPanel();

        SoundManager.instance.DieSound();
        SoundManager.instance.GameOver();

    }

    void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);
        SoundManager.instance.DieSound();




    }

    IEnumerator TrexDur()
    {
        yield return new WaitForSeconds(5f);
        if (trex_Trigger)
        {
            trex_Trigger = false;
            player_Renderer.sprite = player_Sprite;
        }
    }

    void DestroyObstacle(Collider2D target)
    {
        explosion.transform.position = target.transform.position;
        explosion.SetActive(false);
        explosion.SetActive(true);

        SoundManager.instance.DieSound();

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.OBSTACLE)
        {
            if (!trex_Trigger)
            {
                 DieWithObstacle(target);
            }
            else
            {
                DestroyObstacle(target);
            }
        }

        if (target.tag == MyTags.T_REX)
        {
            trex_Trigger = true;
            player_Renderer.sprite = trex_Sprite;
            target.gameObject.SetActive(false);

            StartCoroutine(TrexDur());
        }

        if (target.tag == MyTags.STAR)
        {
            for (int i = 0; i < star_Effect.Length; i++)
            {
                if (!star_Effect[i].activeInHierarchy)
                {
                    star_Effect[i].transform.position = target.transform.position;
                    star_Effect[i].SetActive(true);
                    break;
                }
            }

            target.gameObject.SetActive(false);
            SoundManager.instance.PlayCoinSound();
            GamePlayController.instance.UpdateStarScore();
        }
    }
}
