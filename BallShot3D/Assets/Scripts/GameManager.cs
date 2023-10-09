using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Balls;
    int activeBallIndex;
    public GameObject FirePoint;
    [SerializeField] private float ballPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Balls[activeBallIndex].transform.SetPositionAndRotation(FirePoint.transform.position,FirePoint.transform.rotation);  
            Balls[activeBallIndex].SetActive(true);
            Balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(Balls[activeBallIndex].transform.TransformDirection(90,90,0) * ballPower, ForceMode.Force);
            if(Balls.Length-1 == activeBallIndex)
                activeBallIndex = 0;
            else
                activeBallIndex++;
        }
        
    }
}
