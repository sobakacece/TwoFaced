using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : CharacterData
{
    [SerializeField] float dashTime;
    public override float MyDashTime { get => dashTime; set => dashTime = value; }
    [SerializeField] float speed;
    public override float MySpeed { get => speed; set => speed = value; }
    [SerializeField] float dashSpeed;
    [SerializeField] public override float MyDashSpeed { get => dashSpeed; set => dashSpeed = value; }

    [SerializeField] RuntimeAnimatorController controller;
    public override RuntimeAnimatorController MyController { get => controller; }

    [SerializeField] HitBox sword;
    public override void Start()
    {
        print("start knitg");
    }
    public override void SpecialAttack()
    {
        sword.StartAttack();
        print("Knight attack");
    }
    public override void EndSpecialAttack()
    {
        sword.StopAttack();
    }
}
