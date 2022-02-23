using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastScript : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            if (Physics.Raycast(ray, out RaycastHit hit)) {    
                Debug.Log($"You have clicked {hit.transform.name}");
                if(hit.transform.name == "Brick(Clone)"){
                    Destroy(hit.transform.gameObject);
                }
                else if(hit.transform.name == "Question(Clone)"){
                    Debug.Log("Add 1 to coin count");
                    coins++;
                    this.GetComponent<AudioSource>().Play();
                    if(coins < 10){
                        coinText.text = $"$x0{coins.ToString()}";
                    }
                    else{
                        coinText.text = $"$x{coins.ToString()}";
                    }
                }
            }  
        } 
    }
}
