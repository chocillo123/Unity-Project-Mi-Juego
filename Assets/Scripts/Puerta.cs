using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public InventarioManager inventarioManager;
    public RaycatsInteract interact;
    public Transform puerta;
    public int id;
    public float angle = 90f;
    public bool abierta;
    public string nombreLlave;
    public bool tieneLaLlave;
    float targetValue;
    float currentValue;
    public float easing = 0.02f;

    [Header("Sounds")]
    AudioSource source;
    public AudioClip ClipAbrir;
    public AudioClip ClipCerrar;
    public AudioClip ClipCerradaConLlave;
    public AudioClip ClipAbrirPrimeraVez;



    void Start()
    {
        source = GetComponent<AudioSource>();

        if (tieneLaLlave == true)
        {
            GeneralPuerta.eventoPuerta += AbrirOCerrarPuerta;
        }
        else
        {
            GeneralPuerta.eventoPuerta += AbrirOCerrarPuertaConLlave;
        }
        
    }

    void Update()
    {
        currentValue += (targetValue - currentValue) * easing;
        puerta.transform.rotation = Quaternion.identity;
        puerta.transform.Rotate(0f, currentValue, 0f);
    }


    public void AbrirOCerrarPuerta(int puertaAabrir)
    {
        abierta = !abierta;

        if (id == puertaAabrir)
        {
            if (abierta)
            {
                targetValue = angle;
                currentValue = 0f;
                source.PlayOneShot(ClipAbrir);
            }
            else
            {
                currentValue = angle;
                targetValue = 0f;
                source.PlayOneShot(ClipCerrar);
            }

            interact.interacting = false;
        }
        
    }

    public void AbrirOCerrarPuertaConLlave(int puertaAabrir)
    {
        abierta = !abierta;        

        if (id == puertaAabrir)
        {
            if (tieneLaLlave == false)
            {
                source.PlayOneShot(ClipCerradaConLlave);
                abierta = !abierta;
            }
            else if (tieneLaLlave == true)
            {
                source.PlayOneShot(ClipAbrirPrimeraVez);
                abierta = !abierta;
                GeneralPuerta.eventoPuerta -= AbrirOCerrarPuertaConLlave;
                GeneralPuerta.eventoPuerta += AbrirOCerrarPuerta;
            }

            interact.interacting = false;
        }

    }

}
