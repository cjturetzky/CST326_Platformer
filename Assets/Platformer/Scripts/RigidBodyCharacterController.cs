using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacterController : MonoBehaviour
{
    private Rigidbody body;
    private Collider charCol;
    public float runForce = 3f;
    public float maxRunSpeed = 6f;
    public float jumpForce = 10f;
    public float holdJumpForce = 5f;

    public bool feetOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        charCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        float castDistance = charCol.bounds.extents.y + 0.1f;
        feetOnGround = Physics.Raycast(transform.position, Vector3.down, castDistance);

        float axis = Input.GetAxis("Horizontal");
        body.AddForce(Vector3.right * axis * runForce, ForceMode.Force);

        if(feetOnGround && Input.GetKeyDown(KeyCode.Space)){
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if(Input.GetKey(KeyCode.Space)){
            body.AddForce(Vector3.up * holdJumpForce, ForceMode.Force);
        }

        if(Mathf.Abs(body.velocity.x) > maxRunSpeed){
            float newX = maxRunSpeed * Mathf.Sign(body.velocity.x);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }

        if(axis < 0.1f){
            float newX = body.velocity.x * (1f - Time.deltaTime * 3f);
            body.velocity = new Vector3(newX, body.velocity.y, body.velocity.z);
        }
    }
}
