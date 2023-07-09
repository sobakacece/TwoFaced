using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHit
{
    [SerializeField] public float speed;
    [SerializeField] int damage;
    public Vector3 direction;
    public int MyDamage { get => damage; }
    [SerializeField] string ignoreLayer;
    public string ignoreCollisions { get => ignoreLayer; set => ignoreLayer = value; }
    Rigidbody2D rb;
    public void OnTriggerEnter2D(Collider2D other)
    {
        foreach (IDamageable damageable in other.gameObject.GetComponents<IDamageable>())
        {
            if (other.gameObject.layer.ToString() != ignoreCollisions)
                damageable.OnHit(MyDamage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        print(direction);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void UpdateFacingDirection()
    {
          if (direction.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0)
        {
            transform.localScale = transform.localScale;
        }
    }
}
