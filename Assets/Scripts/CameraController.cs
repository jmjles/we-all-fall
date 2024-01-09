using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private float x;
    private float y;
    public float zoom = -5.0f;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = player.transform.position;
        x = position.x;
        y = position.y;
        transform.position = new Vector3(x,2.5f,zoom);
    }
}
