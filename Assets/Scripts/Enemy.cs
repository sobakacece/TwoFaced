using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool isMoveable = true;
    public bool IsMoveable { get => isMoveable;  set => isMoveable = value; }
    [SerializeField] private int maxHealth;
    [SerializeField] protected float attackCD;
    private int currHealth;
    public int MyMaxHealth { get => maxHealth; }
    public int MyCurrentHealth { get => currHealth; set => currHealth = value; }
    public GameObject target;
    [SerializeField] protected float speed;
    protected Vector3 direction;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected bool IsAttackReady = true;
    protected Vector2 MyCurrentVelocity
    {
        get => rb.velocity;

        set
        {
            rb.velocity = value;
            if (rb.velocity == Vector2.zero)
                animator.SetBool("IsMoving", false);
            else
                animator.SetBool("IsMoving", true);
        }
    }

    protected void MoveToTarget()
    {
        direction = (target.transform.position - transform.position).normalized;
        if (IsMoveable && Vector2.Distance(target.transform.position, transform.position) > 1f)
            MyCurrentVelocity = speed * direction;
        else
            MyCurrentVelocity = Vector2.zero;
    }

    public IEnumerator AttackCd()
    {
        IsAttackReady = false;
        yield return new WaitForSeconds(attackCD);
        IsAttackReady = true;
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    public void OnHit(int amount)
    {
        MyCurrentHealth -= amount;
        animator.SetTrigger("Damaged");
        if (MyCurrentHealth <= 0)
        {
            animator.SetTrigger("Dead");
        }
    }
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            print("null");
        }
    }
    protected void UpdateFaceDirection()
    {
        if (MyCurrentVelocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (MyCurrentVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
