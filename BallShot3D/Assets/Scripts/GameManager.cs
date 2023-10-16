using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI starNum;
    [SerializeField] private TextMeshProUGUI levelNum;
    [Header("Ball Settings")]
    public GameObject[] Balls;
    int activeBallIndex;
    public GameObject FirePoint;
    [SerializeField] private float ballPower;
    [SerializeField] private Animator ballThrower;
    [SerializeField] private ParticleSystem ballThrowEffect;
    [SerializeField] private ParticleSystem[] ballDestructEffect;
    int activeBallEffectIndex;

    [Header("Level Settings")]
    [SerializeField] private int targetBallNum;
    [SerializeField] private int standBallNum;
    int GoalBall;
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI remainBallNum_Text;
    [SerializeField] private AudioSource[] Sounds;

    [Header("Other Settings")]
    [SerializeField] private Renderer bucketTrans;
    float bucketStartValue;
    float bucketStatValue;
    string LevelName;

    // Start is called before the first frame update
    void Start()
    {
        activeBallEffectIndex = 0;
        bucketStartValue = 0.5f;
        bucketStatValue = .25f / targetBallNum;
        LevelName = SceneManager.GetActiveScene().name;
        LevelSlider.maxValue = targetBallNum;
        remainBallNum_Text.text = standBallNum.ToString();
    }
    public void isBallEnter(bool enter)
    {
        if (enter)
        {
            Sounds[3].Play();
            GoalBall++;
            LevelSlider.value = GoalBall;
            bucketStartValue -= bucketStatValue;
            bucketTrans.material.SetTextureScale("_MainTex", new Vector2(1f, bucketStartValue));
            if (GoalBall == targetBallNum)
            {
                // Lock Cannon
                Time.timeScale = 0f;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 10);
                starNum.text = PlayerPrefs.GetInt("Star").ToString();
                levelNum.text = "Level : " + LevelName;
                Sounds[0].Play();
                Panels[1].SetActive(true);
            }
            int num = 0;
            foreach (var item in Balls)
            {
                if (item.activeInHierarchy)
                    num++;
            }
            if (num == 0)
            {
                if (standBallNum == 0 && GoalBall != targetBallNum)
                {
                    Lose();
                }
                Debug.Log("Entered");
                if ((standBallNum + GoalBall) < targetBallNum)
                {
                    Lose();
                }
            }
        }
        else
        {
            int num = 0;
            foreach (var item in Balls)
            {
                if (item.activeInHierarchy)
                    num++;
            }
            if (num == 0)
            {
                if (standBallNum == 0)
                {
                    Lose();
                }
                if ((standBallNum + GoalBall) < targetBallNum)
                {
                    Lose();
                }
            }
        }
    }
    public void PanelButtons(string operation)
    {
        switch (operation)
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
    public void ParcEffect(Vector3 pos, Color color)
    {
        ballDestructEffect[activeBallEffectIndex].transform.position = pos;

        var main = ballDestructEffect[activeBallEffectIndex].main;
        main.startColor = color;

        ballDestructEffect[activeBallEffectIndex].gameObject.SetActive(true);
        activeBallEffectIndex++;
        if (activeBallEffectIndex == ballDestructEffect.Length - 1)
            activeBallEffectIndex = 0;
    }
    void Lose()
    {
        Time.timeScale = 0f;
        Sounds[1].Play();
        Panels[2].SetActive(true);
    }
    public void ThrowBall()
    {
        if (Time.timeScale != 0)
        {
            standBallNum--;
            remainBallNum_Text.text = standBallNum.ToString();
            Sounds[2].Play();
            ballThrower.Play("BallThrow");
            ballThrowEffect.Play();
            Balls[activeBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[activeBallIndex].SetActive(true);
            Balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(Balls[activeBallIndex].transform.TransformDirection(90, 90, 0) * ballPower, ForceMode.Force);
            if (Balls.Length - 1 == activeBallIndex)
                activeBallIndex = 0;
            else
                activeBallIndex++;
        }
    }
}
