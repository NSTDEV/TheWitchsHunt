using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 5f; //velocidad del personaje
    void Start()
    {
        
    }

    void Update()
    {
        float velocityX = Input.GetAxis("Horizontal")*Time.deltaTime*velocity;
        float velocityY = Input.GetAxis("Vertical")*Time.deltaTime*velocity;
        Vector3 positionC = transform.position;
        transform.position = new Vector3(velocityX + positionC.x , velocityY + positionC.y, positionC.z);
    }
}
