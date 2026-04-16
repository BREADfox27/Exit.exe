using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform[] DestinationPoints;

    public float distanceToFollowPath = 2;

    int i = 0;

    public bool followPlayer = false;
    GameObject player;
    float distanceToPlayer;
    public bool playerInRoom = false;

    public float distanceToFollow = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        i = 0;
        navMeshAgent.destination = DestinationPoints[i].position;
        player = FindFirstObjectByType<PlayerController>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FollowPlayerTrigger"))
        {
            followPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FollowPlayerTrigger"))
        {
            followPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= distanceToFollow && followPlayer == true)
        {
            FollowPlayer();
        }

        else
        {
            EnemyPath();
        }
    }

    public void EnemyPath()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= distanceToFollowPath)
        {
            if (i < DestinationPoints.Length - 1)
            {
                i++;
            }

            else
            {
                i = 0;
            }

            navMeshAgent.destination = DestinationPoints[i].position;
        }
    }

    public void FollowPlayer()
    {
        navMeshAgent.destination = player.transform.position;
    }
}
