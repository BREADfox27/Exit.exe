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

    [Header("Raycast hit")]
    public float rayDistance = 50f;
    public LayerMask layermask;

    void Update()
    {
        CharacterMovement();
        CharacterJump();
        Raycast();
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

    void Raycast()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(origin, direction, out hit, rayDistance, layermask))
        {
            Debug.Log("We collided with: " + hit.collider.gameObject.name);
            Debug.DrawLine(origin, hit.point, Color.red);

            if (hit.collider.tag == "KeyCode")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FollowPlayerTrigger"))
        {
            variableAI.followPlayer = true;
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
        }
    }
}

