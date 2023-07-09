using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnightEnemy : Enemy
{
    HitBox sword;
    [SerializeField] float distanceToHit;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        MyCurrentHealth = MyMaxHealth;
        sword = GetComponentInChildren<HitBox>();
    }

    // Update is called once per frame
    void Update()
    {
        print($"IsMoveable {IsMoveable.ToString()}, CanAttack = {CanAttack().ToString()}, IsAttackReady = {IsAttackReady.ToString()}");

        if (IsMoveable && !CanAttack())
            MoveToTarget();
        else
        {
            Attack();
        }
        UpdateFaceDirection();
    }
    public void Attack()
    {
        sword.StartAttack();
        animator.SetTrigger("Attack");
        StartCoroutine(AttackCd());
    }
    public bool CanAttack()
    {
        if (Vector2.Distance(target.transform.position, transform.position) > distanceToHit || !IsAttackReady)
        {
            return false;
        }
        else
            return true;
    }
    public void FinishAttack()
    {
        IsMoveable = true;
        sword.StopAttack();
    }
}
