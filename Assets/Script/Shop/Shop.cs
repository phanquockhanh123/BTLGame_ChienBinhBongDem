using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopKeeper;
    public int currentSelecItem;
    public int currentSelecCost;

    private Player _player;
    public void SelectItem(int Item)
    {
        switch (Item)
        {
            case 0:
                UIManager.Instance.UpdateShopItem(70);
                currentSelecItem = 0;
                currentSelecCost = 200;
                //UIManager.Instance.UpdateGemCount(_player.diamod);
                BuyFlame();
                break;
            case 1:
                UIManager.Instance.UpdateShopItem(-40);
                currentSelecItem = 1;
                currentSelecCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopItem(-150);
                currentSelecItem = 2;
                currentSelecCost = 100;
                UIManager.Instance.UpdateGemCount(_player.diamod);
                break;
        }
    }
    private void Start()
    {
        Debug.Log("moss: " + MossGiant._intance.flame);
    }
    private void Update()
    {
        Debug.Log("moss: "+ MossGiant._intance.flame);   
        Debug.Log("skeletion: "+ Skeletion._intance.flame);
        
    }
    public void BuyFlame()
    {
        
        MossGiant._intance.flame ++;
        Skeletion._intance.flame ++;
        Spider._intance.flame ++;
        PlayerPrefs.SetInt("flame3", MossGiant._intance.flame);
        PlayerPrefs.SetInt("flame3", Skeletion._intance.flame);
        PlayerPrefs.SetInt("flame3", Spider._intance.flame);
    }
    public void BuyItem()
    {
        if (_player.diamod >= currentSelecCost)
        {
            if(currentSelecItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamod -= currentSelecCost;
            PlayerPrefs.SetInt("diamod", _player.diamod);
            ShopKeeper.SetActive(false);
        }
        else
        {
            ShopKeeper.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamod);
            }
            ShopKeeper.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ShopKeeper.SetActive(false);
        }
    }
}
