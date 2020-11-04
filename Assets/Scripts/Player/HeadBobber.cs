using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{

    private float timer = 0.0f;
    public float bobbingSpeedWalk = 0.14f;
    public float bobbingSpeedRun = 0.18f;
    public float bobbingSpeedCrouch = 0.1f;
    [Space(15)]
    public float bobbingAmount = 0.2f;
    public float midpoint = 0.8f;
    public float actualSpeed;
    public bool isCrouch;


    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        if (Input.GetKeyDown(KeyCode.C) && isCrouch == false)
        {
            isCrouch = true;           
        }
        else if (Input.GetKeyDown(KeyCode.C) && isCrouch == true)
        {
            isCrouch = false;
            
        }

        if (isCrouch == true)
        {
            actualSpeed = bobbingSpeedCrouch;
        }
        else if (isCrouch == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                actualSpeed = bobbingSpeedRun;
            }
            else
            {
                actualSpeed = bobbingSpeedWalk;
            }
            
        }
        


        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + actualSpeed * Time.deltaTime;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }

        Vector3 v3T = transform.localPosition;
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            v3T.y = midpoint + translateChange;
        }
        else
        {
            v3T.y = midpoint;
        }
        transform.localPosition = v3T;

    }

}
