using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float rotateSpeed = 200f;

    private float rotateX;
    private float rotateY;

    [SerializeField]
    private GameObject playerObject = null;
    void Start()
    {
        MouseManager.Lock(true);
        MouseManager.Show(true);
    }

    void Update()
    {
        Move();
        CameraRotate();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    void CameraRotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse Y");
        float mouseY = Input.GetAxisRaw("Mouse X");

        rotateX += rotateSpeed * mouseX * Time.deltaTime;
        rotateY += rotateSpeed * mouseY * Time.deltaTime;

        rotateX = Mathf.Clamp(rotateX, -80f, 80f);

        playerObject.transform.rotation = Quaternion.Euler(new Vector3(-rotateX, rotateY, 0));
    }
}
