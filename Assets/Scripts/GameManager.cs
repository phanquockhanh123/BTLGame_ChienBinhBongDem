using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    public int gem;
    public GameObject gemPrefab;
    public List<GameObject> gemPool;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool have;
    public int health;
    public int healthMax;


    // Start is called before the first frame update
    void Start()
    {
        // Object pool
        for (int i = 0; i < 20; i++)
        {
            gemPool.Add(Instantiate(gemPrefab, Vector2.zero, Quaternion.identity));
            gemPool[i].SetActive(false);
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            string jsonPlayer = File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
            PlayerData load = JsonUtility.FromJson<PlayerData>(jsonPlayer);
            gem = load.score;
            dieEnemy = load.dieEnemy;
            hasBoot = load.hasBoot;
            instance.hasKey = load.hasKey;
        }
        else
        {
            SavePlayerData();
        }
        
        InitStat();

    }

    void InitStat()
    {
        healthMax = 30;
        health = healthMax;
    }



    public void DropGems(int quantity, Vector2 position)
    {
        int spawned = 0;
        foreach (var item in gemPool)
        {
            if (spawned >= quantity)
            {
                break;
            }
            if (!item.activeSelf)
            {
                item.transform.position = position + new Vector2(Random.Range(-2f, 2f), Random.Range(0, 2f));
                item.SetActive(true);
                spawned++;
            }
        }
    }

    public void AddGem(int amount)
    {
        gem = Mathf.Clamp(gem + amount, 0, 200000);
        UIManager.instance.UpdateGem();
    }

    public int dieEnemy = 0;
    public bool hasFlame = false;
    public bool hasBoot = false;
    public bool hasKey = false;
    public string hadItem = "";

    public void OnApplicationQuit()
    {
        SavePlayerData();
    }
    public class PlayerData
    {
        public int score;
        public int dieEnemy;
        public bool hasFlame;
        public bool hasKey;
        public bool hasBoot;
    }
    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.score = 600;
        playerData.dieEnemy = dieEnemy;
        playerData.hasFlame = hasFlame;
        playerData.hasBoot = hasBoot;
        playerData.hasKey = hasKey;
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", json);
        //File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        //PlayerData load = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log("score:"+load.score);
        //Debug.Log("Enemy:"+load.dieEnemy);
        //Debug.Log("item:"+load.hasFlame);
    }

}
