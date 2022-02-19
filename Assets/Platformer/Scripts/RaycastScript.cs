using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
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
                if(hit.transform.name == "Brick"){
                    Destroy(hit.transform.gameObject);
                }
                else if(hit.transform.name == "Question"){
                    Destroy(hit.transform.gameObject);
                    Debug.Log("Add 1 to coin count");
                }
            }  
        } 
    }
}
