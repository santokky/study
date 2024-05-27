using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacter : MonoBehaviour
{
    #region Variables
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 6f;

    private Rigidbody rb;

    private Vector3 inputDirection = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerManager.instance.playerHp++;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();

        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");
        if (inputDirection != Vector3.zero) {
            transform.forward = inputDirection;
        }

        //process jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(
                jumpHeight * -2f * Physics.gravity.y);
            GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Vector3 dashVelocity = Vector3.Scale(transform.forward,
                dashDistance * new Vector3(3f, 0, 3f));

            GetComponent<Rigidbody>().AddForce(dashVelocity, ForceMode.VelocityChange);
        }

    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position
            + inputDirection * speed * Time.fixedDeltaTime);
    }

    #region Helper Methods
    void CheckGroundStatus() {
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down,
            groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
            Debug.Log("¶¥ ¹âÀ½");
        }
        else {
            isGrounded = false;
            Debug.Log("¶¥ ¸ø¹âÀ½");
        }
    }

    #endregion
}
