using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 350f;
    private GameObject sword;
    private GameObject shield;
    private string equipped = "";
    private float hp = 5.0f;
    bool onPlatform = false;
    public bool onFloor = true;
    public bool jumping = false;
    public bool dblJump = false;
    float x;
    float y;
    float mouseX;
    float mouseY;
    public float jumpDblTimer = 0;
    public float jumpDblCD = 200;

    // Start is called before the first frame update
    void Start()
    {
        sword = GameObject.Find("Sword");
        shield = GameObject.Find("Shield");
    }

    // Update is called once per frame
    void Update()
    {
        SetPositions();
        if (hp > 0)
        {
            Movement();
            Actions();
        }

    }
    private void Movement()
    {
        // keep from rotating
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // movement
        transform.Translate(Vector3.right * Time.deltaTime * speed * x, Space.World);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (!onFloor && !onPlatform)
        {
            jumping = true;

        }

        // Cant go under floor or platform
        if (onFloor && y < 0)
        {
            y = 0;
        }
    }
    private void Actions()
    {   
        if (equipped == "Sword")
        {
            //Component swordS = sword.GetComponent("SwordController");

        }
        else
        {
            //sword.transform.position = transform.position;
        }
        if (equipped == "Shield")
        {

        }
    }
    private void Jump()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        // Double jump available once
        if (jumpDblTimer <= 0)
        {
            if (!onFloor && !onPlatform && jumping && !dblJump)
            {
                dblJump = true;
                r.velocity = Vector3.zero;
                r.AddForce(Vector3.up * jumpForce);
            }
        }
        if (!jumping)
        {
            jumpDblTimer = jumpDblCD;
            r.AddForce(Vector3.up * jumpForce);
        }

    }
    private void SetPositions()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (jumpDblTimer > 0)
        {
            jumpDblTimer -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Floor")
        {
            dblJump = false;
            jumping = false;
            onFloor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Floor")
        {
            onFloor = false;
        }
    }
}
