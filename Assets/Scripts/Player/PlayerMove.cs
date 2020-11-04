using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Camera Setting")]
    public float sensibility;
    public float mimRotY;
    public float maxRotY;
    float rotX;
    Vector2 inputRot;
    public Transform cam;

    [Header("Speed Setting")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    [HideInInspector]public Rigidbody rb;
    Vector2 inputMov;
    float actualSpeed;

    [Header("Crouch Setting")]
    public CapsuleCollider capsuleCollider;
    public bool isCrouch;
    public bool NoCrouch;
    public float rayDistance;
    Vector3 normalScale;
    Vector3 crouchScale;
    Vector3 normalCenterScale;
    Vector3 crouchCenterScale;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotX = cam.eulerAngles.x;
        normalScale.y = capsuleCollider.height;
        crouchScale.y = normalScale.y;
        crouchScale.y = normalScale.y * 0.50f;
        normalCenterScale.y = 0f;
        crouchCenterScale.y = 0.5f;
    }

    void Update()
    {
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        inputRot.x = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        actualSpeed = Input.GetKey(KeyCode.LeftShift) && isCrouch == false ? runSpeed : walkSpeed;

        if (Input.GetKeyDown(KeyCode.C) && NoCrouch == false)
        {
            isCrouch = !isCrouch;
        }
        
        
        if(isCrouch == true)
        {
            actualSpeed = crouchSpeed;
            raycastAgachar();
        }


        transform.rotation *= Quaternion.Euler(0, inputRot.x, 0);

        rotX -= inputRot.y;
        rotX = Mathf.Clamp(rotX, mimRotY, maxRotY);
        cam.localRotation = Quaternion.Euler(rotX, 0, 0);

    }

    void FixedUpdate()
    {      

        rb.velocity = transform.forward * actualSpeed * inputMov.y + transform.right * actualSpeed * inputMov.x + new Vector3(0f, rb.velocity.y, 0f);

        capsuleCollider.height = Mathf.Lerp(capsuleCollider.height, isCrouch ? crouchScale.y : normalScale.y, 0.2f);

        capsuleCollider.center = Vector3.Lerp(capsuleCollider.center, isCrouch ? crouchCenterScale : normalCenterScale, 0.1f);
       
    }

    public void raycastAgachar()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.up);
        Debug.DrawRay(transform.position, Vector3.up * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider)
            {
                NoCrouch = true;
            }

        }
        else
        {
            NoCrouch = false;
        }

    }

}