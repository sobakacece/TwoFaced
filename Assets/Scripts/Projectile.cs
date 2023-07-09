using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHit
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    public int MyDamage { get => damage; }

    public void OnTriggerEnter2D(Collider2D other)
    {
        foreach (IDamageable damageable in other.gameObject.GetComponents<IDamageable>())
        {
            damageable.Hit(MyDamage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Vector3.forward;
    }
}
