using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject itemHolder;
    [SerializeField] int currentItem;
    [SerializeField] TextMeshProUGUI gemText;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip _select;
    [SerializeField] AudioClip _buy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
            SelectItem(0);
            gemText.text = GameManager.instance.gem + " G";
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int itemIndex)
    {
        currentItem = itemIndex;
        source.PlayOneShot(_select);
        // 0 = sword, 1 = boot, 2 = key // curItem srciptObj, scroll view index
        switch (itemIndex)
        {
            case 0:
                {
                    UIManager.instance.SetSelectionPos(-106);
                    break;
                }
            case 1:
                {
                    UIManager.instance.SetSelectionPos(-300);
                    break;
                }
            case 2:
                {
                    UIManager.instance.SetSelectionPos(-493);
                    break;
                }
        }
    }

    public void BuyItem()
    {
        source.PlayOneShot(_buy);
        switch (currentItem)
        {
            case 0:
                {
                    DecreaseGem(200);
                    GameManager.instance.hasFlame = true;
                    break;
                }
            case 1:
                {
                    DecreaseGem(300);

                    GameManager.instance.hasBoot = true;
                    break;
                }
            case 2:
                {
                    DecreaseGem(100);
                    GameManager.instance.hasKey = true;
                    break;
                }
        }        
    }

    void DecreaseGem(int amount)
    {
        if (GameManager.instance.gem < amount)
        {
            print("Not enough gem");
        }
        else
        {
            GameManager.instance.AddGem(-amount);
            gemText.text = GameManager.instance.gem + " G";
        }
    }
}
