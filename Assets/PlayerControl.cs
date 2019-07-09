using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnimator;

    public bool isJumping = false;
    public bool isWalking = false;
    public bool isCrounching = false;

    public Collider2D initialBodyCollider;
    public Collider2D initialNoseCollider;
    public Collider2D crounchCollider;

    public float force;

    private bool hasStarted = false;
    private bool isLanding = true;
    private Rigidbody2D playerBody;

    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void setCrounch() {
        this.isCrounching = true;
        this.isWalking = false;
        this.initialBodyCollider.enabled = false;
        this.initialNoseCollider.enabled = false;
        this.crounchCollider.enabled = true;
    }

    public bool CanJump() {
        return (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping && isLanding;
    }
    public void Jump() {
        this.playerBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        isJumping = true;
        isWalking = false;
        isLanding = false;
    }
    private void resetDefaultValues(){
        this.initialBodyCollider.enabled = true;
        this.initialNoseCollider.enabled = true;
        this.crounchCollider.enabled = false;
        this.isWalking = true;
        this.isCrounching = false;
        this.isJumping = false;
        this.playerAnimator.SetBool("isCrounching", false);
        this.playerAnimator.SetBool("isJumping", false);
    }
    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
            //Diferete do Javascript, o C# entende '' como um caractere e "" como uma string, então cuidado...
            // playerAnimator.SetBool("isJumping", true);
            Jump();
            hasStarted = true;
        }
        if(hasStarted) {
            if(CanJump()) {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isJumping", true);
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow) && isLanding) {
                setCrounch();
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isCrounching", true);

            }
            if(Input.GetKeyUp(KeyCode.DownArrow) && isCrounching) {
                resetDefaultValues();
            }
            if(isLanding)
                playerAnimator.SetBool("isWalking", isWalking);

        }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Ground"))
        {
            this.resetDefaultValues();
            this.isLanding = true;
        }
    }
}
