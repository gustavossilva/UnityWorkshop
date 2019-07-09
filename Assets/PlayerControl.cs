using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isJumping = false;
    public bool isWalking = false;
    public bool isCrounching = false;
    private bool hasStarted = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public bool CanJump() {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && !isJumping;
    }
    public void SetJumping() {
        isJumping = true;
        isWalking = true;
    }
    private void resetAnimation(){
        playerAnimator.SetBool("isWalking", true);
        playerAnimator.SetBool("isCrounching", false);
        playerAnimator.SetBool("isJumping", false);
    }
    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            //Diferete do Javascript, o C# entende '' como um caractere e "" como uma string, então cuidado...
            // playerAnimator.SetBool("isJumping", true);
            SetJumping();
            hasStarted = true;
        }
        if(hasStarted) {
            if(CanJump()) {
                Debug.Log("Entrei");
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isJumping", true);
                SetJumping();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isCrounching", true);
                isCrounching = true;
                isWalking = false;
            }
            if(!isWalking)
                resetAnimation();
        }
    }
}
