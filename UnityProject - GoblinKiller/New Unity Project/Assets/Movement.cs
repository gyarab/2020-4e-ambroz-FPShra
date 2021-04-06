using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    //Inicializace rychlosti, gravitace a vysky skoku
    public float rychlost = 12f;
    public float gravity = -9.81f;
    public float skok = 3f;
    //Zda je hrad na zemi
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    //Kontrola zda je hrac na zemi, a aktualizovani skoku a gravitace
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <0){
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * rychlost * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded){
            velocity.y = Mathf.Sqrt(skok * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller. Move(velocity * Time.deltaTime);
    }
}
