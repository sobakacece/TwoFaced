using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sword : MonoBehaviour, IHit
{
    [SerializeField] private int damage;
    public int MyDamage { get => damage; }
    BoxCollider2D hitBox;
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    public void StartAttack()
    {
        hitBox.enabled = true;
    }
    public void StopAttack()
    {
        hitBox.enabled = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        foreach (IDamageable damageable in other.gameObject.GetComponents<IDamageable>())
        {
            damageable.Hit(MyDamage);
        }
    }
}
