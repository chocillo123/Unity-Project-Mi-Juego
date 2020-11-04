using UnityEngine.UI;
using UnityEngine;

public class RaycatsInteract : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask mask;
    public float distance;
    public bool interacting;

    [Header("Images")]
    public Image interactImage;
    public Image AimImage;

    [Header("Sounds")]
    AudioSource source;
    public AudioClip interactAudioItens;



    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        var P = transform.parent.position;
        var D = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(P, D * distance, Color.red);

        if (Physics.Raycast(P,D,out hit,distance, mask))
        {
            if (hit.collider.CompareTag("Itens") && interacting == false)
            {
                AimImage.enabled = false;
                interactImage.enabled = true;
                Debug.Log("tocado");

                if (Input.GetKeyDown(KeyCode.F) && interacting == false)
                {
                    interacting = true;
                    interactImage.enabled = false;
                    hit.collider.GetComponent<Itens>().AddIten();
                    source.PlayOneShot(interactAudioItens);
                }
            }

            if (hit.collider.CompareTag("Puerta") && interacting == false)
            {
                AimImage.enabled = false;
                interactImage.enabled = true;
                Debug.Log("tocado");

                if (Input.GetKeyDown(KeyCode.F) && interacting == false)
                {
                    interacting = true;
                    interactImage.enabled = false;
                    GeneralPuerta.eventoPuerta(hit.collider.GetComponent<Puerta>().id);
                }
            }
           
        }
        else
        {
            AimImage.enabled = true;

            interactImage.enabled = false;
        }

    }

}
