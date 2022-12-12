using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ContactFilter2D movementFilter;
    [SerializeField] private float collisionOffset;
    [SerializeField] private float moveSpeed;

    private bool hasKey = false;
    public GameObject endScreen;
    private int plantCount = 0;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Vector2 movementInput;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate(){
        if(movementInput != Vector2.zero){
            //Call TryToMove to check for collisions
            bool canMove = TryToMove(movementInput);

            //Call TryToMove again but only checking on either x or y axis to "slide" around obstacles
            if(!canMove){
                canMove = TryToMove(new Vector2(movementInput.x, 0));
                
                if(!canMove){
                    canMove = TryToMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        if(movementInput.x > 0){
            spriteRenderer.flipX = false;
        } else if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        }
    }

    private bool TryToMove(Vector2 direction){
        if(movementInput != Vector2.zero){
            int count = rb.Cast(movementInput, movementFilter, castCollisions,
                (moveSpeed * Time.fixedDeltaTime) + collisionOffset);

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    void OnMove(InputValue movementVal){
        movementInput = movementVal.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "SilverKey"){
            Destroy(collider.gameObject);
            Debug.Log("Key");
            hasKey = true;
        }
        if(collider.gameObject.name == "Chest1"){
            if(hasKey){
                Debug.Log("I got the key");
                Destroy(collider.gameObject);
                plantCount++;
            }
        }
        if(collider.gameObject.name == "ExitDoor" && plantCount >= 3){
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        if(collider.gameObject.name == "Monstera" || collider.gameObject.name == "Succulent"
        || collider.gameObject.name == "Cactus" || collider.gameObject.name == "Aloe Vera"){
            Destroy(collider.gameObject);
            plantCount++;
        }
    }

}
