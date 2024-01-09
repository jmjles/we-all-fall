using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GoblinController : MonoBehaviour
{
    private GameObject player;
    public float speed = 13;
    public float jumpForce = 5;
    public float horizontal = 0;
    public float xCD = 100;
    public float yCD = 100;
    public Timer horCD;
    public Timer verCD;
    bool jumping = false;
    public float oldHor = 0;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        horCD = new Timer(xCD);
        verCD = new Timer(yCD);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 gobPos = transform.position;
        // keep from rotating
        transform.rotation = Quaternion.Euler(0, 0, 0);

            if (horCD.IsDone())
            {   oldHor = horizontal;
                horizontal = playerPos.x < gobPos.x ? 1 : -1;
                horCD.Start();
            }
            else
            {
                horCD.Tick();
            }
        transform.Translate(Vector3.left * horizontal * speed * Time.deltaTime, Space.World);
        rb = GetComponent<Rigidbody>();
        bool jump = playerPos.y > 1 && Math.Abs(gobPos.x - playerPos.x) < 3;
        if (jump && !jumping)
        {
            print(jumpForce);
            rb.AddForce(Vector3.up * 5);
            verCD.Start();
        }
        else
        {
            verCD.Tick();
        }
    }
    public class Timer
    {
        public float cooldown { get; set; }
        private float cdTimer = 0;
        public Timer(float cooldown)
        {
            this.cooldown = cooldown;
        }
        public void Tick()
        {
            if (cdTimer >= 0)
            {
                cdTimer--;
            }
        }
        public bool IsDone() { 
            return cdTimer <= 0; 
        }
        public void Start()
        {
            cdTimer = cooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Floor")
        {
            jumping = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Floor")
        {
            jumping = true;
        }
    }
}
