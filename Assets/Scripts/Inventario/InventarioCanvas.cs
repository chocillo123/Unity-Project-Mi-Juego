using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCanvas : MonoBehaviour 
{

    public GameObject Inventario;
    [Space(15)]
    public bool InventarioActivo;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I) && InventarioActivo == false)
        {
            activar();
        }
        else if (Input.GetKeyDown(KeyCode.I) && InventarioActivo == true)
        {
            desactivar();
        }

    }

    public void activar()
    {
        InventarioActivo = true;
        Inventario.SetActive(true);        
    }

    public void desactivar()
    {
        InventarioActivo = false;
        Inventario.SetActive(false);       
    }

}
