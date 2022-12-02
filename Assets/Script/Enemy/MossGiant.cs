using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy,IDamageable
{
    public static MossGiant _intance;
    public int Health { get; set; }
    public int flame = 1;
    public static int soLuongMoss = 0;
    private void Awake()
    {
        _intance = this;
    }
    public void Damage()
    {
        flame = PlayerPrefs.GetInt("flame3",flame);
        if (IsDead == true)
        {
            return;
        }
        Health-=flame;
        IsHit = true;
        anim.SetTrigger("Hit");
        anim.SetBool("IsCombat", true);
        if (Health < 1)
        {
            anim.SetTrigger("Death");
            IsDead = true;
            soLuongMoss++;
            GameObject diamod =  Instantiate(DiamodPrefab, transform.position, Quaternion.identity) as GameObject;
            diamod.GetComponent<Diamond>().gems = base.gems;
            Destroy(this.gameObject, 2);
        }
    }
    public override void Movement()
    {
        base.Movement();
    }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
}
