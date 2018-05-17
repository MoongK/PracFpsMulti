using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float moveSpeed, angleSpeed;
    Animator anim;
    Transform playerCam;
    float angleY = 0;
    public bool isgrounded;
    public bool isJumping;

    private void Start()
    {
        moveSpeed = 6f;
        angleSpeed = 70f;
        playerCam = transform.Find("PlayerCam");
        anim = GetComponent<Animator>();

        if (isLocalPlayer)
        {
            gameObject.layer = 9;
            ChangeLayer(transform, 9);

        }
        else
        {
            gameObject.layer = 10;
            ChangeLayer(transform, 10);
            playerCam.gameObject.SetActive(false);
        }
    }

    void Update () {
        if (!isLocalPlayer)
            return;

        Movement();
        Rotation();
        
	}

    void ChangeLayer(Transform trans, int toThisLayer)  // 9 : me // 10 : other
    {
        foreach(Transform child in trans)
        {
            if (child.gameObject.layer == 13)
                return;

            child.gameObject.layer = toThisLayer;
            ChangeLayer(child, toThisLayer);
        }
    }

    void Movement()
    {
        float keyX = Input.GetAxisRaw("Horizontal");
        float keyY = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(keyX, 0f, keyY);
        moveDirection = transform.TransformDirection(moveDirection).normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (isgrounded)
        {
            if (isJumping)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 250f);
            }
        }

        anim.SetFloat("DirX", keyX);
        anim.SetFloat("DirY", keyY);
    }

    void Rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * angleSpeed * Time.deltaTime);

        angleY += mouseY * angleSpeed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60f, 90f);
        playerCam.localRotation = Quaternion.Euler(-angleY, playerCam.localRotation.y, playerCam.localRotation.z);
    }
}
