using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damage;
    public int MyDamage { get => damage; set => damage = value; }
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
