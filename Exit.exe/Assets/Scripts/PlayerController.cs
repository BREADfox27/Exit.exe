using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    public CharacterController playercontroller;
    public float speed = 10f;

    float gravity = -9.81f;
    Vector3 velocity;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    bool isGrounded;
    public float jump = 1;

    public AI variableAI;
    public AI variableAI1;
    public AI variableAI2;
    public AI variableAI3;

    void Update()
    {
        CharacterMovement();
        CharacterJump();
    }

    void CharacterMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        playercontroller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        playercontroller.Move(velocity * Time.deltaTime);
    }

    void CharacterJump()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2  * gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FollowPlayerTrigger"))
        {
            variableAI.followPlayer = true;
            variableAI1.followPlayer = true;
            variableAI2.followPlayer = true;
            variableAI3.followPlayer = true;
        }

        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Lose");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FollowPlayerTrigger"))
        {
            variableAI.followPlayer = false;
            variableAI1.followPlayer = false;
            variableAI2.followPlayer = false;
            variableAI3.followPlayer = false;
        }
    }
}

