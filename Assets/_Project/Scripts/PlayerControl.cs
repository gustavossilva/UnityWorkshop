using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnimator;

    public bool isJumping = false;
    public bool isCrounching = false;
    [SerializeField] private bool isLanding = true;

    public Collider2D initialBodyCollider;
    public Collider2D initialNoseCollider;
    public Collider2D crounchCollider;

    public Transform groundCheck;
    public LayerMask groundMask;

    public AudioSource dinoSounds;
    public AudioClip jumpSound;
    private Rigidbody2D playerBody;
    public float force;

    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public bool TryToJump() {
        bool jumpClicked = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jumpClicked) {
            isJumping = true;
        }
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
        if(!GameManager.instance.hasStarted && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
            isJumping = true;
            //Diferete do Javascript, o C# entende '' como um caractere e "" como uma string, então cuidado...
            // playerAnimator.SetBool("isJumping", true);
            Jump();
            GameManager.instance.hasStarted = true;
            return;
        }
        if (!GameManager.instance.hasStarted) {
            return;
        }
        //Checa se o jogador está no chão
        this.isLanding = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
        //Acontece quando o jogador tenta pular e pode pular.
        if(TryToJump()) {
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isCrounching", false);
            playerAnimator.SetBool("isJumping", true);
            isLanding = false;
            Jump();
        }
        //Acontece caso o jogador pressione a tecla, está no chão e não está pulando
        else if(Input.GetKey(KeyCode.DownArrow) && isLanding && !isJumping) {
            this.isCrounching = true;
            this.initialBodyCollider.enabled = false;
            this.initialNoseCollider.enabled = false;
            this.crounchCollider.enabled = true;
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.SetBool("isCrounching", true);

        }
        //Acontece quando o player solta a tecla para baixo e está abaixo.
        else if(Input.GetKeyUp(KeyCode.DownArrow) && isCrounching) {
            this.initialBodyCollider.enabled = true;
            this.initialNoseCollider.enabled = true;
            this.crounchCollider.enabled = false;
            this.isCrounching = false;
        }
        //Está pulando e encostou no chão
        else if(isLanding && isJumping) {
            isJumping = false;
        }
        //Está no chão e 
        else if(isLanding && !isJumping && !isCrounching){
            playerAnimator.SetBool("isWalking", true);
            this.playerAnimator.SetBool("isCrounching", false);
            this.playerAnimator.SetBool("isJumping", false);
        }

    }
}
