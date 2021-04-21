using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouselook : MonoBehaviour
{   
    //Sensitivity mysi a transformovani na hrace
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    // Ukotveni kursoru ve stredu obrazovky
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Aktualizovani pozice mysi
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Rotace postavy soucasne s postavou
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
