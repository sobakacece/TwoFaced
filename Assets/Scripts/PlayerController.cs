using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Character")]
    public CharacterData character;
    [Header("Walking")]
    public float walkingSpeed = 1f;
    public float collisionOffset = 0.05f;
    public bool IsMoveable = true;
    [Header("Dashing")]
    public float dashTime = 1f;
    public float dashSpeed = 10f;
    public ContactFilter2D movementFilter; //WHERE YOU WILL COLLIDE

    [Header("QTER")]
    [SerializeField] QTEEvent qEvent;
    [SerializeField] QTEManager qManager;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //LIST OF FOUND COLLISIONS AT THE END OF 
    protected Rigidbody2D rb;
    protected Vector2 movementInput; //DIRECTION INPUT 
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;



    [Header("Combat")]
    [SerializeField] private int maxHealth;
    protected static int currHealth;
    public int MyMaxHealth => maxHealth;

    public int MyCurrentHealth { get => currHealth; set => currHealth = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        MyCurrentHealth = maxHealth;

        ApplyCharData();

    }
    public void ApplyCharData()
    {
        animator.runtimeAnimatorController = character.MyController;
        dashSpeed = character.MyDashSpeed;
        walkingSpeed = character.MySpeed;
        dashTime = character.MyDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero && IsMoveable)
        {
            TryMove(movementInput, walkingSpeed);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        UpdateFaceDirection();
        // print($"IsMoveable = {IsMoveable.ToString()}");
    }

    private bool TryMove(Vector2 direction, float speed)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            animator.SetBool("isMoving", true);
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * direction);
            return true;
        }
        return false;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void UpdateFaceDirection()
    {
        if (movementInput.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (movementInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    protected virtual void OnFire()
    {
        animator.SetTrigger("Attack");
        // sword.StartAttack();
        character.SpecialAttack();
    }
    void OnDash()
    {
        // print("Dash " + movementInput.ToString());
        StartCoroutine(Dash(dashTime, movementInput));
    }
    void OnQTE()
    {
        print("QTE STARTED");
        animator.SetTrigger("Transformed");
        qManager.startEvent(qEvent);
    }
    IEnumerator Dash(float dashTime, Vector2 dashDirection)
    {
        StopMovement();
        float currTime = 0;
        while (currTime < dashTime)
        {
            currTime += Time.deltaTime;
            print(currTime);
            TryMove(dashDirection, dashSpeed);
            // rb.velocity = dashSpeed * dashDirection;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        ResumeMovement();

    }
    public void StopMovement()
    {
        IsMoveable = false;
    }
    public void ResumeMovement()
    {
        IsMoveable = true;
    }
    public void EndAttack()
    {
        ResumeMovement();
        character.EndSpecialAttack();
    }

    public void OnHit(int amount)
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
    // public void DisableInput()
    // {
    //     GetComponent<PlayerInput>().enabled = false;
    // }
    // public void EnableInput()
    // {
    //     GetComponent<PlayerInput>().enabled = false;

    // }
}
