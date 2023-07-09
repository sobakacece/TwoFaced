using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int currHealth;
    public int MyMaxHealth { get => maxHealth;  }
    public int MyCurrentHealth { get => currHealth; set => currHealth = value; }
    Animator animator;
    Sword sword;

    public void Hit(int amount)
    {
        MyCurrentHealth -= amount;
        animator.SetTrigger("Damaged");
        if (MyCurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        MyCurrentHealth = MyMaxHealth;
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCd();
    }
    public void Attack()
    {

    }
    private IEnumerator StartCd()
    {
        print("Knight attacks");
        sword.StartAttack();
        yield return new WaitForSeconds(2);
        sword.StopAttack();
    }
}
