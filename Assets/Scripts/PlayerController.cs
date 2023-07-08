using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Walking")]
    public float walkingSpeed = 1f;
    public float collisionOffset = 0.05f;
    public bool IsMoveable = true;
    [Header("Dashing")]
    public float dashTime = 1f;
    public float dashSpeed = 10f;
    public ContactFilter2D movementFilter; //WHERE YOU WILL COLLIDE
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //LIST OF FOUND COLLISIONS AT THE END OF 
    Rigidbody2D rb;
    Vector2 movementInput; //DIRECTION INPUT 
    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
    void OnFire()
    {
        print("Fire");
        animator.SetTrigger("Attack");
    }
    void OnDash()
    {
        // print("Dash " + movementInput.ToString());
        StartCoroutine(Dash(dashTime, movementInput));
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
        print("resumed");
    }

}
