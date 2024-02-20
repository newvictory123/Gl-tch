using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public GameObject player;
    public GameObject arm;
    public float cameraXMoveSpeed;
    public float cameraYMoveSpeed;
    public float mouseXInput;
    public float mouseYInput;
    public float xRotation;
    public float yRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.rotation = player.transform.rotation;
    }

    private void Update()
    {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + player.transform.localScale.y / 2, player.transform.position.z);
        GetInput();
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void GetInput()
    {
        mouseXInput = Input.GetAxisRaw("Mouse X") * cameraXMoveSpeed * Time.deltaTime;
        mouseYInput = Input.GetAxisRaw("Mouse Y") * cameraYMoveSpeed * Time.deltaTime;

        yRotation += mouseXInput;

        xRotation -= mouseYInput;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
