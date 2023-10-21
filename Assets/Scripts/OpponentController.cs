using UnityEngine;

public class OpponentController : MonoBehaviour
{
    private Rigidbody opponentRb;
    private Vector3 moveDirection;

    private Transform finishLine;

    private float moveTorque = 500;
    private float dstToGround;

    private GameManager gameManager;

    public bool isGrounded;
    public bool isFinish;
    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        opponentRb = GetComponent<Rigidbody>();

        finishLine = GameObject.Find("Finish Line").transform;

        moveDirection = (finishLine.position - transform.position).normalized; 

        opponentRb.maxAngularVelocity = 100;

        isFinish = false;

        gameManager = GameObject.Find("Track").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isRaceStarted)
        {
            opponentRb.useGravity = true;
            
            Movement();
        }
    }

    void Movement()
    {
        isGrounded = CheckGround();

        if(isGrounded && !isFinish)
        {
            opponentRb.AddForce(moveDirection * moveTorque * Time.deltaTime, ForceMode.Force);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Finish")) 
        {   
            isFinish = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground") 
        {   
            isGrounded = true;
        } 
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground") 
        {   
            isGrounded = false;
        } 
    }

    public bool CheckGround()
    {
        dstToGround = GetComponent<Collider>().bounds.extents.y;
        
        return Physics.Raycast(transform.position, Vector3.down, dstToGround + 0.1f);
    }
}
