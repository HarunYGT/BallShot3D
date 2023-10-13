using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] Panels;
    public TextMeshProUGUI starNum;
    public TextMeshProUGUI levelNum;
    [Header("Ball Settings")]
    public GameObject[] Balls;
    int activeBallIndex;
    public GameObject FirePoint;
    [SerializeField] private float ballPower;

    [Header("Level Settings")]
    [SerializeField] private int targetBallNum;
    [SerializeField] private int standBallNum;
    int GoalBall;
    public Slider LevelSlider;
    public TextMeshProUGUI remainBallNum_Text;

    // Start is called before the first frame update
    void Start()
    {
        LevelSlider.maxValue = targetBallNum; 
        remainBallNum_Text.text = standBallNum.ToString();
    }

    public void isBallEnter(bool enter)
    {
        if(enter)
        {
            GoalBall++;
            LevelSlider.value = GoalBall;
            if(GoalBall == targetBallNum)
            {
                // Lock Cannon
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") +1);
                PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") +10);
                starNum.text = PlayerPrefs.GetInt("Star").ToString();
                levelNum.text = "Level : " + PlayerPrefs.GetInt("Level").ToString();
                Panels[1].SetActive(true);
            }
            if(standBallNum == 0 && GoalBall != targetBallNum)
            {
                Panels[2].SetActive(true);
            }
            Debug.Log("Entered");
            if((standBallNum + GoalBall) < targetBallNum)
            {
                Panels[2].SetActive(true);
            }
        }
        else
        {
            if(standBallNum == 0)
            {
                Panels[2].SetActive(true);
            }
            if((standBallNum + GoalBall) < targetBallNum)
            {
                Panels[2].SetActive(true);  
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            standBallNum--;
            remainBallNum_Text.text = standBallNum.ToString();
            Balls[activeBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position,FirePoint.transform.rotation);  
            Balls[activeBallIndex].SetActive(true);
            Balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(Balls[activeBallIndex].transform.TransformDirection(90,90,0) * ballPower, ForceMode.Force);
            if(Balls.Length-1 == activeBallIndex)
                activeBallIndex = 0;
            else
                activeBallIndex++;
        }
        
    }
    public void PanelButtons(string operation)
    {
        switch(operation)
        {
            case "Pause":
                Time.timeScale = 0f;
                Panels[0].SetActive(true);
                break;
            case "Retry":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Settings":
                Debug.Log("Maybe I add settings panel.");
                break;
            case "Resume":
                Time.timeScale = 1f;
                Panels[0].SetActive(false);
                break;
            case "NextLevel":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                break;

        }
    }
}
