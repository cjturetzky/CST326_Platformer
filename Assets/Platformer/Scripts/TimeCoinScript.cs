using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCoinScript : MonoBehaviour
{
    public TextMeshProUGUI timerTextmesh;
    public int maxTime = 100;
    private int currentTime;
    private float elapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 1.0f){
            currentTime--;
            elapsedTime = 0;
        }
        if(currentTime > 0){
            timerTextmesh.text = currentTime.ToString();
        }
        if(currentTime == 0){
            timerTextmesh.text = "Time's up! Game Over";
            Debug.Log("Timer ran out.");
            currentTime = -1;
        }
    }
}
