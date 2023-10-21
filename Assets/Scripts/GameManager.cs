using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countdownText;

    public GameObject finishPanel;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI scoreText;

    private float timer;
    private float finalTimer;
    public bool isRaceStarted = false;
    public bool isRaceDone = false;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "00:00";

        countdownText.text = "3";

        positionText.text = "0";

        scoreText.text = "0";

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        StartCoroutine(Countdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        RaceEnd();
    }

    void RaceStart()
    {
        isRaceStarted = true;
    }

    void RaceEnd()
    {   
        if(isRaceDone)
        {
            finalTimer = timer;

            DisplayTime(finalTimer);

            positionText.text = "Position: " + playerController.racePos;

            scoreText.text = "Score: " + playerController.score;

            finishPanel.SetActive(true);
        }
        
        if(isRaceStarted && !isRaceDone)
        {
            timer += Time.deltaTime;

            DisplayTime(timer);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
       
        while (count > 0) {
           
            // display something...
            countdownText.text = count.ToString();

            yield return new WaitForSeconds(1);

            count --;
        }
       
        countdownText.gameObject.SetActive(false);

        // count down is finished...
        RaceStart();
    }
}
