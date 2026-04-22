using UnityEngine;
using UnityEngine.SceneManagement;

public class Selected : MonoBehaviour
{
    [Header("Raycast hit")]
    public float rayDistance = 2.5f;
    public LayerMask layermask;

    public GameObject interactiveText;
    public int keysCollected;
    public PlayerController playerconVariable;
    public GameObject gun;

    public GameObject enemy;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public int points = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keysCollected = 0;
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();

        if (keysCollected == 3)
        {
            SceneManager.LoadScene("Level2");
        }
    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(origin, direction, out hit, rayDistance, layermask))
        {
            // Debug.Log("We collided with: " + hit.collider.gameObject.name);
            // Debug.DrawLine(origin, hit.point, Color.red);

            if (hit.collider.tag == "Key")
            {
                interactiveText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<Keys>().Deactivate();
                    interactiveText.SetActive(false);
                    keysCollected++;
                }
            }

            if (hit.collider.tag == "Gun")
            {
                interactiveText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    gun.gameObject.SetActive(true);
                    hit.collider.transform.GetComponent<DeactivateGun>().Deactivate();
                    playerconVariable.speed = 0;
                    interactiveText.SetActive(false);
                    enemy.gameObject.SetActive(true);
                }
            }

            if (hit.collider.tag == "Enemy")
            {
                if (Input.GetMouseButtonDown(0) && points == 0)
                {
                    enemy.gameObject.SetActive(false);
                    enemy1.gameObject.SetActive(true);
                    points++;
                }
            }

            if (hit.collider.tag == "Enemy1")
            {
                if (Input.GetMouseButtonDown(0) && points == 1)
                {
                    enemy1.gameObject.SetActive(false);
                    enemy2.gameObject.SetActive(true);
                    points++;
                }
            }

            if (hit.collider.tag == "Enemy2")
            {
                if (Input.GetMouseButtonDown(0) && points == 2)
                {
                    enemy2.gameObject.SetActive(false);
                    enemy3.gameObject.SetActive(true);
                    points++;
                }
            }

            if (hit.collider.tag == "Enemy3")
            {
                if (Input.GetMouseButtonDown(0) && points == 3)
                {
                    enemy3.gameObject.SetActive(false);
                    points++;
                }

                if (points == 4)
                {
                    SceneManager.LoadScene("Win");
                }
            }
        }
    }
}
