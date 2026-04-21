using UnityEngine;

public class Selected : MonoBehaviour
{
    [Header("Raycast hit")]
    public float rayDistance = 50f;
    public LayerMask layermask;

    public GameObject interactiveText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(origin, direction, out hit, rayDistance, layermask))
        {
            // Debug.Log("We collided with: " + hit.collider.gameObject.name);
            Debug.DrawLine(origin, hit.point, Color.red);

            if (hit.collider.tag == "Key")
            {
                interactiveText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<Keys>().Deactivate();
                }
            }

            else
            {
                interactiveText.gameObject.SetActive(false);
            }
        }
    }
}
