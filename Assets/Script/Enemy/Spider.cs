using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public static Spider _intance;
    public int flame = 1;
    public int Health { get; set; }
    public GameObject AcidEffectPrefab;
    public bool isShoot = false;
    public static int soLuong = 0;
    private void Awake()
    {
        _intance = this;
    }
    /*private void Update()
    {
        Debug.Log("soLuong: " + soLuong);
    }*/
    public void Damage()
    {
        flame = PlayerPrefs.GetInt("flame3", flame);
        if (IsDead == true)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            IsDead = true;
            soLuong++;
            GameObject diamod = Instantiate(DiamodPrefab, transform.position, Quaternion.identity) as GameObject;
            diamod.GetComponent<Diamond>().gems = base.gems;
            Destroy(this.gameObject, 2);
        }
    }
    public override void Movement()
    {

    }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public void Attack()
    {
        if (isShoot == true)
        {
            Instantiate(AcidEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
