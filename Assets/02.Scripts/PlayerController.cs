using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        dir.y = 0f;
        dir.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 15f;
        }
        else
        {
            moveSpeed = 10f;
        }
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

    //public float moveSpeed = 10.0f;
    //public float rotationSpeed = 200.0f;

    //private Transform playerTransform;

    //private readonly float initHp = 100.0f;

    //public float curHp;

    //private Image hpBar;

    //[SerializeField]
    //private GameObject playerObject;
    //private float rotateX;
    //private float rotateY;

    //IEnumerator Start()
    //{
    //    playerTransform = GetComponent<Transform>();

    //    rotationSpeed = 0.0f;
    //    yield return new WaitForSeconds(0.3f);
    //    rotationSpeed = 80.0f;

    //    curHp = initHp;

    //    hpBar = GameObject.FindGameObjectWithTag("HPBAR")?.GetComponent<Image>();
    //    //DisplayHP();

    //    MouseManager.Lock(true);
    //    MouseManager.Show(true);
    //}

    //void Update()
    //{
    //    Move();
    //    CameraRotate();
    //}

    //private void Move()
    //{
    //    float h = Input.GetAxisRaw("Horizontal");
    //    float v = Input.GetAxisRaw("Vertical");

    //    Vector3 dir = Vector3.right * h + Vector3.forward * v;
    //    dir = Camera.main.transform.TransformDirection(dir);
    //    dir.y = 0f;
    //    dir.Normalize();

    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        moveSpeed *= 1.5f;
    //    }
    //    else
    //    {
    //        moveSpeed = 10f;
    //    }
    //    transform.Translate(dir * moveSpeed * Time.deltaTime);
    //}

    //void CameraRotate()
    //{
    //    float mouseX = Input.GetAxisRaw("Mouse Y");
    //    float mouseY = Input.GetAxisRaw("Mouse X");

    //    rotateX += rotationSpeed * mouseX * Time.deltaTime;
    //    rotateY += rotationSpeed * mouseY * Time.deltaTime;

    //    rotateX = Mathf.Clamp(rotateX, -80f, 80f);

    //    playerObject.transform.rotation = Quaternion.Euler(new Vector3(-rotateX, rotateY, 0));
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("PUNCH") && curHp > 0.0f)
    //    {
    //        curHp -= 10.0f;
    //        Debug.Log($"Player Hp = {curHp}");

    //        //DisplayHP();

    //        if(curHp <= 0.0f)
    //        {
    //            PlayerDie();
    //        }
    //    }
    //}

    //void PlayerDie()
    //{
    //    Debug.Log("Player Die");

    //    GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
    //    foreach(GameObject monster in monsters)
    //    {
    //        monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
    //    }

    //    GameMgr.GetInstance().IsGameOver = true;
    //}

    ////void DisplayHP()
    ////{
    ////    hpBar.fillAmount = curHp / initHp;
    ////}
}
