using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    public AIPath aIPath;
    public Transform spawnPoint;
    public Animator animator;
    public GameObject exclaimation;
    public GameObject heartExclaimation;
    public float moveSpeed = 5f;
    public bool useAiPathTest; // FOR DEMO, TO REMOVE
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Transform flameLocation;
    public bool flameInRange { get; set; }
    public bool playerInRange { get; set; }
    public bool stare = false;

    // Start is called before the first frame update
    void Start()
    {
        // move to spawn point
        transform.position = spawnPoint.position;
        // initialise variables
        flameInRange = false;
        playerInRange = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (flameInRange && !stare)
        {
            Debug.Log("Chase Flame");
            if (distanceCloserThan(flameLocation.position, 0.8f))
            {
                Stare();
            }
            else
            {
                ChaseTarget(flameLocation.position);
            }
        }

        else if (playerInRange)
        {
            ChasePlayer();
        }
        else if (!distanceCloserThan(spawnPoint.position, 0.2f))
        {
            ReturnToSpawnPoint();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private bool distanceCloserThan(Vector3 target, float distance)
    {
        return (Mathf.Abs((target - transform.position).magnitude) <= distance);
    }

    public void ChaseFlame(Transform flame)
    {
        heartExclaimation.SetActive(true);
        flameLocation = flame;
        flameInRange = true;
    }

    public void ChasePlayer()
    {
        exclaimation.SetActive(true);
        if (useAiPathTest)
        {
            Debug.DrawLine(transform.position, aIPath.GetNextStep(transform.position), Color.white);
            ChaseTarget(aIPath.GetNextStep(transform.position));
        }
        else
        {
            ChaseTarget(Player.instance.transform.position);
        }
    }

    public void ChaseTarget(Vector3 targetPos)
    {
        Debug.Log("Chasing");
        direction = (targetPos - transform.position);
        // prevents interpolation between left/right/up/down movement
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0f;
        }
        else
        {
            direction.x = 0f;
        }
        direction.Normalize();
        rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    IEnumerator Stare()
    {
        Debug.Log("Start to stare");
        stare = true;
        animator.SetFloat("Speed", 0);
        animator.Play("Goblin_Idle");
        while(stare)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    void ReturnToSpawnPoint()
    {
        // move to spawn point
        ChaseTarget(spawnPoint.position);

    }
}