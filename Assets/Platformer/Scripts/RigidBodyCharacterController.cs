using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RigidBodyCharacterController : MonoBehaviour
{
    private Rigidbody body;
    private Collider charCol;
    private Animator animComp;
    private RaycastHit headHitInfo;
    public TextMeshProUGUI coinCountUI;
    public TextMeshProUGUI scoreUI;
    public float runForce = 3f;
    public float maxRunSpeed = 6f;
    public float jumpForce = 10f;
    public float holdJumpForce = 5f;
    public bool jump = false;
    private int coins = 0;
    private int score = 0;
    private float timeSinceHit = 0f;

    public bool feetOnGround = true;
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        charCol = GetComponent<Collider>();
        animComp = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float castDistance = charCol.bounds.extents.y + 0.1f;
        feetOnGround = Physics.Raycast(transform.position, Vector3.down, castDistance);

        hit = Physics.Raycast(transform.position, Vector3.up, out headHitInfo, castDistance * 1.7f);

        float axis = Input.GetAxis("Horizontal");
        body.AddForce(Vector3.right * axis * runForce, ForceMode.Force);

        if(feetOnGround && Input.GetKeyDown(KeyCode.Space)){
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = true;
            Debug.Log("Jumping");
        }
        else if(feetOnGround && !Input.GetKey(KeyCode.Space)){
            jump = false;
            Debug.Log("Not Jumping");
        }

        if(hit && timeSinceHit > 0.1f){
            if(headHitInfo.transform.name == "Brick(Clone)"){
                Destroy(headHitInfo.transform.gameObject);
                score += 100;
                scoreUI.text = score.ToString();
                timeSinceHit = 0f;
            }
            else if(headHitInfo.transform.name == "Question(Clone)"){
                Debug.Log("Add 1 to coin count");
                coins++;
                this.GetComponent<AudioSource>().Play();
                if(coins < 10){
                    coinCountUI.text = $"$x0{coins.ToString()}";
                }
                else{
                    coinCountUI.text = $"$x{coins.ToString()}";
                }
                score += 100;
                scoreUI.text = score.ToString();
                timeSinceHit = 0f;
            }
        }
        else if(!hit){
            timeSinceHit += Time.deltaTime;
        }
        // else if(Input.GetKey(KeyCode.Space)){
        //     body.AddForce(Vector3.up * holdJumpForce, ForceMode.Force);
        // }

        if(Input.GetKey(KeyCode.LeftShift)){
            Debug.Log("Sprinting");
            maxRunSpeed = 10f;
            float newX = maxRunSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);

        }
        else{
            maxRunSpeed = 6f;
        }

        if(Mathf.Abs(body.velocity.x) > maxRunSpeed){
            float newX = maxRunSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }

        if(axis < 0.1f){
            float newX = body.velocity.x * (1f - Time.deltaTime * 3f);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }
        animComp.SetBool("Jumping", jump);
        if(!jump){
            animComp.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        }
        
    }

    void OnCollisionEnter(Collision col){
        if(col.transform.name == "Spike(Clone)"){
            Debug.Log("Spike touched");
            scoreUI.text = "You Died. Game Over";
        }
        if(col.transform.name == "Goal(Clone)"){
            Debug.Log("Goal touched");
            scoreUI.text = $"{scoreUI.text} YOU WIN!";
        }
    }
}
