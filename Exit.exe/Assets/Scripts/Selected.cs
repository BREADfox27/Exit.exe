using UnityEngine;
using UnityEngine.SceneManagement;

public class Selected : MonoBehaviour
{
    [Header("Raycast hit")]
    public float rayDistance = 2.5f;
    public LayerMask layermask;

    public GameObject interactiveText;

    public int keysCollected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keysCollected = 0;
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
        }
    }
}
