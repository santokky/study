using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{
    #region Variables
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 6f;

    public float gravity = -9.81f;
    public Vector3 drags = new Vector3(8, 8, 8);
    
    private CharacterController characterController;

    private Vector3 inputDirection = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    private Vector3 calcVelocity;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && calcVelocity.y < 0) {
            calcVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero) {
            transform.forward = move;
        }
        
        //process jump input
        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
                dashDistance * new Vector3(3f, 0, 3f));

            calcVelocity += dashVelocity;

         }

        calcVelocity.y += gravity * Time.deltaTime;

        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }
}
