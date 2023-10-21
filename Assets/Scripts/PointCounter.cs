using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private GameObject racer;
    public ParticleSystem finishLineSFX;
    private float score = 10;
    private int count = 1;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Track").GetComponent<GameManager>();
    }

    void Update()
    {
        if(count == 8)
        {
            gameManager.isRaceDone = true;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        racer = other.gameObject;

        if(other.CompareTag("Player")) 
        {   
            racer.GetComponent<PlayerController>().score = score;

            racer.GetComponent<PlayerController>().racePos = count;

            finishLineSFX.Play();
        }

        if(other.CompareTag("Opponent")) 
        {   
            racer.GetComponent<OpponentController>().score = score;
        }

        count++;
        score--;
    }
}
