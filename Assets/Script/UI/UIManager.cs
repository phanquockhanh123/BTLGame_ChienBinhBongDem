using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UIManager");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public Text PlayerGemCountText;
    public Image selectionImg;
    public Text gemCountText;
    public Image[] healthBar;
    public void Again()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void OpenShop(int gemCount)
    {
        //_gemCount = gemCount;
        PlayerGemCountText.text = "" + gemCount+ "G";
    }
    public void UpdateShopItem(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }
    public void UpdateGemCount(int count)
    {
        /*count = PlayerPrefs.GetInt("count");*/
        gemCountText.text = "" + count;

    }
    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                healthBar[i].enabled = false;
            }
        }
    }
}
