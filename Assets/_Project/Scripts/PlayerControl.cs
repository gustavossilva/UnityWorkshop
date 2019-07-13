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

    public Transform groundCheck;
    public LayerMask groundMask;

    public AudioSource dinoSounds;
    public AudioClip jumpSound;
    public float force;
    [SerializeField] private bool isLanding = true;
    private Rigidbody2D playerBody;

    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public bool CanJump() {
        bool jumpClicked = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        return jumpClicked && isLanding && !isCrounching;
    }

    public void Jump() {
        dinoSounds.clip = jumpSound;
        dinoSounds.Play();
        this.playerBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        this.isLanding = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

        if(!GameManager.instance.hasStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
            //Diferete do Javascript, o C# entende '' como um caractere e "" como uma string, então cuidado...
            // playerAnimator.SetBool("isJumping", true);
            Jump();
            GameManager.instance.hasStarted = true;
        }
        if(GameManager.instance.hasStarted) {
            if(CanJump()) {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isJumping", true);
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow) && isLanding) {
                this.isCrounching = true;
                this.initialBodyCollider.enabled = false;
                this.initialNoseCollider.enabled = false;
                this.crounchCollider.enabled = true;
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isCrounching", true);

            }
            if(Input.GetKeyUp(KeyCode.DownArrow) && isCrounching) {
                this.initialBodyCollider.enabled = true;
                this.initialNoseCollider.enabled = true;
                this.crounchCollider.enabled = false;
                this.isCrounching = false;
            }
            if(isLanding && !isCrounching){
                playerAnimator.SetBool("isWalking", true);
                this.playerAnimator.SetBool("isCrounching", false);
                this.playerAnimator.SetBool("isJumping", false);
            }
        }
    }
}
