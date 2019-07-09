using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isJumping = false;
    public bool isWalking = true;
    public bool isCrounching = false;
    private bool hasStarted = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && Input.GetKeyDown(KeyCode.Space)) {
            playerAnimator.SetBool('isJumping', true)
        }
        if(hasStarted && Input.GetKeyDown(KeyCode.Space))
    }
}
