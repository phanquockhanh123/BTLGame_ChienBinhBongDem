using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelLose : MonoBehaviour
{
    public static panelLose _intance;
    public GameObject PanelLose;
    public Text txtMoss;
    public Text txtSkeletion;
    public Text txtSpider;
    public Text txtDiamod;
    public Text txt_Total_Moss;
    public Text txt_Total_Skeletion;
    public Text txt_Total_Spider;
    public Text txt_Total_Diamod;
    public Text txt_Total_Player;
    private int moss;
    private int skeletion;
    private int spider;
    private int diamod;
    private int _totalMoss;
    private int _totalSkeletion;
    private int _totalSpider;
    private int _totalDiamod;
    private int _totalPlayer;
    private void Awake()
    {
        _intance = this;
    }
    private void Start()
    {
        PanelLose.SetActive(true);
        moss = skeletion = spider = diamod = 0;
        _totalMoss = _totalSkeletion = _totalSpider = _totalDiamod = 0;
        _totalPlayer = 0;

    }
    private void Update()
    {
        SpiderPanel();
        SkeletionPanel();
        MossGiantPanel();
        DiamodPanel();
        PlayerTotalPanel();
    }
    void MossGiantPanel()
    {
        moss = MossGiant.soLuongMoss;
        txtMoss.text = moss.ToString();
        _totalMoss = moss * 15;
        txt_Total_Moss.text = _totalMoss.ToString();
    }
    void SpiderPanel()
    {
        spider = Spider.soLuong;
        txtSpider.text = spider.ToString();
        _totalSpider = spider * 5;
        txt_Total_Spider.text = _totalSpider.ToString();
    }
    void SkeletionPanel()
    {
        skeletion = Skeletion.soLuongSkeletion;
        txtSkeletion.text = skeletion.ToString();
        _totalSkeletion = skeletion * 10;
        txt_Total_Skeletion.text = _totalSkeletion.ToString();
    }
    void DiamodPanel()
    {
        diamod = Diamond.soLuongDiamod;
        txtDiamod.text = diamod.ToString();
        _totalDiamod = diamod * 5;
        txt_Total_Diamod.text = _totalDiamod.ToString();
    }
    void PlayerTotalPanel()
    {
        _totalPlayer = _totalMoss + _totalDiamod + _totalSkeletion + _totalSpider;
        txt_Total_Player.text = _totalPlayer.ToString();
    }

}
