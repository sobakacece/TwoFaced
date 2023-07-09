using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : CharacterData
{
    [SerializeField] float dashTime;
    public override float MyDashTime { get => dashTime; set => dashTime = value; }
    [SerializeField] float speed;
    public override float MySpeed { get => speed; set => speed = value; }
    [SerializeField] float dashSpeed;
    [SerializeField] public override float MyDashSpeed { get => dashSpeed; set => dashSpeed = value; }

    [SerializeField] RuntimeAnimatorController controller;
    public override RuntimeAnimatorController MyController { get => controller; }

    // Start is called before the first frame update
    [SerializeField] Projectile projectile;
    [SerializeField] Transform placeToSpawn;

    public override void SpecialAttack()
    {
        print("DragonFire");
        projectile.direction = transform.TransformDirection(Vector3.right);
        Instantiate(projectile, placeToSpawn.position, projectile.transform.rotation);
        
        // projectile.transform.parent = scene;
    }
    public override void EndSpecialAttack()
    {
        print("DragonFire");
    }
}
