using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 movement;
    private Vector3 playerMove;
    private Quaternion camRotation;
    public GameObject ballSFX;
    private float movementX;
    private float movementZ;
    private float moveTorque = 500;
    private float dstToGround;

    private GameManager gameManager;

    public bool isGrounded;
    public bool isFinish;
    public int racePos = 0;
    public float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        playerRb.maxAngularVelocity = 100;

        isFinish = false;

        gameManager = GameObject.Find("Track").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.isRaceStarted)
        {
            playerRb.useGravity = true;

            ballSFX.SetActive(true);

            Movement();
        }
    }

    void Movement()
    {
        Vector3 movePos = new Vector3 (movementX, 0.0f, movementZ);

        movement = movePos * moveTorque;

        //camRotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);

        //playerMove = camRotation * movement;

        isGrounded = CheckGround();

        if(isGrounded && !isFinish)
        {
            playerRb.AddForce(movePos * moveTorque * Time.deltaTime, ForceMode.Force);
        }
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.normalized.x * 5;

        movementZ = movementVector.normalized.y;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Finish")) 
        {   
            isFinish = true;

            gameManager.isRaceDone = true;

            ballSFX.SetActive(false);
        }
    }

    public bool CheckGround()
    {
        dstToGround = GetComponent<Collider>().bounds.extents.y;

        return Physics.Raycast(transform.position, Vector3.down, dstToGround + 0.1f);
    }

}
