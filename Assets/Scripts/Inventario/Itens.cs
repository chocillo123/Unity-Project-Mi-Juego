using System.Collections;
using System;
using UnityEngine;
using static InventarioManager;

public class Itens : MonoBehaviour
{
    public RaycatsInteract interact;
    public InventarioManager inventarioManager;
    public ItensRecogidos itensRecogidos;
    public string nombre;
    public int id;
    public int idRecogido ;
    public int cantidad;
    public Iten iten;

    [Serializable]
    public enum Iten
    {
        acumulable,
        noAcumulable
    }



    void Start()
    {
        
        for (int i = 0; i < itensRecogidos.recogidos.Count; i++)
        {
            if (itensRecogidos.recogidos[i].id == idRecogido)
            {
                Destroy(gameObject);
            }
        }
        
    }

    
    public void AddIten()
    {
        if (inventarioManager.slots < 16)
        {
            if (iten == Iten.acumulable)
            {
                interact.interacting = false;
                inventarioManager.AgregarAlgoAlInventario(name, id, cantidad, InventarioManager.ObjetoInventarioId.Iten.acumulable);
                itensRecogidos.AgregarRecogido(idRecogido);

                Destroy(gameObject, 0.5f);
            }
            else if (iten == Iten.noAcumulable)
            {
                interact.interacting = false;
                inventarioManager.AgregarAlgoAlInventario(name, id, cantidad, InventarioManager.ObjetoInventarioId.Iten.noAcumulable);

                itensRecogidos.AgregarRecogido(idRecogido);
                Destroy(gameObject, 0.5f);
            }
            
        }
        
    }

}
