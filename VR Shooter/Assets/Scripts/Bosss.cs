using UnityEngine;
using UnityEngine.AI;

public class Bosss : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public float enemyHealth = 100;
    public GameObject keydrop;
    GameObject player;
    Animator anim;
    Player playerscript;
  
    bool followingPlayer;

    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10;
    public float distoDetect = 15;
    bool attacked;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Walk Forward", true);
        followingPlayer = true;
        player = GameObject.FindGameObjectWithTag("Player");      
        playerscript = player.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();

       
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        Debug.Log("Going");
        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
       
        if (enemyHealth > 0)
        {
            RaycastHit hit;
            float dis = Vector3.Distance(transform.position, player.transform.position);
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, 200f))
            {
                if (hit.transform.tag == "Player")
                {
                    if (dis < distoDetect || attacked)
                    {
                        if (enemyHealth > 0 && followingPlayer)
                        {
                            
                            agent.SetDestination(player.transform.position);
                        }
                    }
                    else
                    {
                        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 0.5f)
                            GotoNextPoint();
                    }
                }
                else
                {
                    if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 0.5f)
                        GotoNextPoint();
                    attacked = false;
                }               
            }
        }
    }
    public void TakeDamege(float amount)
    {
        attacked = true;
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            death();
            gameObject.layer = 2;
        }
        else anim.SetTrigger("Damaged");
    }

    private void death()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        anim.SetTrigger("Die");
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
        Instantiate(keydrop, pos, transform.rotation);
        Destroy(gameObject, 1.4f);
    }
}
