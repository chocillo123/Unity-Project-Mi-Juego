using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnAround : MonoBehaviour
{
    public PlayerMove playerMove;
    [Space(15)]
    public Transform player;
    [Space(15)]
    public float speedRot;
    [Space(15)]
    public bool rot;
    [Space(15)]
    public float tempRot;
    [Space(15)]
    Vector3 targetRotation;


    void Start()
    {

    }

    void Update()
    {
        targetRotation = new Vector3(0f,transform.eulerAngles.y,0f);
        targetRotation.y = targetRotation.y + 180f;

        if (Input.GetKeyDown(KeyCode.V) && rot == false && playerMove.isCrouch == false)
        {
            StartCoroutine(LerpFunction(Quaternion.Euler(targetRotation), speedRot));
            rot = true;
           
            Invoke("QuitarGirar", tempRot);
        }

    }

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float tiempo = 0;
        Quaternion startValue = transform.rotation;
        

        while (tiempo < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, tiempo / duration);
            tiempo += Time.deltaTime;
            yield return null;
        }


    }

    public void QuitarGirar()
    {
        rot = false;
    }

}
