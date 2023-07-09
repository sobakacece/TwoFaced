using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HitBox : MonoBehaviour, IHit
{
    [SerializeField] private int damage;
    public int MyDamage { get => damage; }
    public string ignoreCollisions { get => ignoreGroup; set => ignoreGroup = value; }
    [SerializeField] string ignoreGroup;
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
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != ignoreCollisions)
            foreach (IDamageable damageable in other.gameObject.GetComponents<IDamageable>())
            {
                damageable.OnHit(MyDamage);
            }
    }
}
