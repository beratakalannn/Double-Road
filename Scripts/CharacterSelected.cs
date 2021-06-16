using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelected : MonoBehaviour
{
    public GameObject[] avaliable_Hero;
    private int currentIndex;
    public Text selectedText;
    //public Sprite starIcon;
    public Image selectBtn;
    public Sprite button_Green, button_Blue;

    private bool[] heroes;

    public Text starScoreText;

    void Start()
    {
        InitializedCharacters();
    }

    void InitializedCharacters()
    {
        currentIndex = GameManager.instance.selectedIndex ;

        for (int i = 0; i < avaliable_Hero.Length; i++)
        {
            avaliable_Hero[i].SetActive(false);
        }

        avaliable_Hero[currentIndex].SetActive(true);

        heroes = GameManager.instance.heroes;

        
    }

    public void NextHero()
    {
        avaliable_Hero[currentIndex].SetActive(false);
        if (currentIndex + 1 == avaliable_Hero.Length)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }

        avaliable_Hero[currentIndex].SetActive(true);

        CheckUnlocked();
    }

    public void PrevoisHero()
    {
        avaliable_Hero[currentIndex].SetActive(false);
        if (currentIndex - 1 == -1 )
        {
            currentIndex = avaliable_Hero.Length  - 1;
        }
        else
        {
            currentIndex--;
        }

        avaliable_Hero[currentIndex].SetActive(true);

        CheckUnlocked();
         


    }

    void CheckUnlocked()
    {
        if (heroes[currentIndex])
        {
            //starIcon.SetActive(false);
            if (currentIndex ==  GameManager.instance.selectedIndex)
            {
                selectBtn.sprite = button_Green;
                selectedText.text = "Selected";
            }
            else
            {
                selectBtn.sprite = button_Blue;
                selectedText.text = "Select ?"; 
            }
        }
        else
        {
            selectBtn.sprite = button_Blue;
           // starIcon.SetActive(true);
            selectedText.text = "1000"; 
        }

    }

    public void SelectHero()
    {
        if (!heroes[currentIndex])
        {
            if (currentIndex != GameManager.instance.selectedIndex)
            {
                if (GameManager.instance.starScore >= 1000)
                {
                    GameManager.instance.starScore -= 1000;
                    selectBtn.sprite = button_Green;
                    selectedText.text = "Selected";
                    heroes[currentIndex] = true;

                    starScoreText.text = GameManager.instance.starScore.ToString();
                    GameManager.instance.selectedIndex = currentIndex;
                    GameManager.instance.heroes = heroes;

                    GameManager.instance.SaveGameData();
                }
                else
                {
                    print("NOT ENOUGH COİN POİNT TO UNLOCK THE PLAYER");
                }
            }
            
        }
        else
        {
            selectBtn.sprite = button_Green;
            selectedText.text = "Selected";
            GameManager.instance.selectedIndex = currentIndex;
            GameManager.instance.SaveGameData();

        }
    }
}
