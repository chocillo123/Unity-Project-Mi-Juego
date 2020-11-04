using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class InventarioManager : MonoBehaviour {

    [Serializable]
    public struct ObjetoInventarioId
    {
        public string nombre;
        public int id;
        public int cantidad;
        public  Iten iten;

        [Serializable]
        public enum Iten
        {
            acumulable,
            noAcumulable
        }
     
        public ObjetoInventarioId(string nombre, int id, int cantidad, Iten iten)
        {
            this.nombre = nombre;
            this.id = id;
            this.cantidad = cantidad;
            this.iten = iten;

        }

    }


    public List <Puerta> puerta;
    [Space(15)]
    public InventarioBaseDatos baseDatos;
    [Space(15)]    
    public int slots;
    [Space(15)]  
    public AudioClip usar;
    AudioSource source;
    [Space(15)]
    public List<ObjetoInventarioId> inventario;

    public void AgregarAlgoAlInventario(string nombre, int id, int cantidad, ObjetoInventarioId.Iten iten)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == id && inventario[i].iten == ObjetoInventarioId.Iten.acumulable)
            {
                inventario[i] = new ObjetoInventarioId(inventario[i].nombre, inventario[i].id, inventario[i].cantidad + cantidad, inventario[i].iten);
                ActualizarInventario();
                slots = inventario.Count;
                return;
            }
        }
        inventario.Add(new ObjetoInventarioId(nombre, id, cantidad, iten));
        ActualizarInventario();
        slots = inventario.Count;
    }

    public void EliminarAlgoDeInventario(string nombre, int id, int cantidad, ObjetoInventarioId.Iten iten)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == id)
            {
                
                inventario[i] = new ObjetoInventarioId(inventario[i].nombre, inventario[i].id, inventario[i].cantidad - cantidad, inventario[i].iten);
                if (inventario[i].cantidad <= 0)
                    inventario.Remove(inventario[i]);
                ActualizarInventario();
                slots = inventario.Count;
                return;
            }
        }
        Debug.LogError("No existe el objeto a eliminar");
    }

    public void IntercambiarPuestos(int i1 , int i2)
    {
        ObjetoInventarioId i = inventario[i1];
        inventario[i1] = inventario[i2];
        inventario[i2] = i;
        ActualizarInventario();
    }


    void Awake()
    {
        
    }      

    public void Start()
    {       
        ActualizarInventario();
        slots = inventario.Count;
        source = GetComponent<AudioSource>();
    }

    public void Save()
    {
        FileStream fs = new FileStream("save.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, inventario);
        fs.Close();
    }

    public void Load()
    {
        using (Stream stream = File.Open("save.dat", FileMode.Open))
        {
            var bformatter = new BinaryFormatter();

            inventario = (List<ObjetoInventarioId>)bformatter.Deserialize(stream);
        }
    }


    public InventarioObjetoInterface prefab;
    public Transform inventarioUI;
    List<InventarioObjetoInterface> pool = new List<InventarioObjetoInterface>();

    public void ActualizarInventario()
    {
        print("InventarioActualizado");
        for (int i = 0; i < pool.Count; i++)
        {
            if (i < inventario.Count)
            {
                ObjetoInventarioId o = inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() => gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
            }
        }
        if (inventario.Count > pool.Count)
        {
            for (int i = pool.Count; i < inventario.Count; i++)
            {
                InventarioObjetoInterface oi = Instantiate(prefab, inventarioUI);
                pool.Add(oi);

                oi.transform.position = Vector3.zero; // 
                oi.transform.localScale = Vector3.one;

                ObjetoInventarioId o = inventario[i];
                pool[i].sprite.sprite = baseDatos.baseDatos[o.id].sprite;
                pool[i].cantidad.text = o.cantidad.ToString();
                pool[i].id = i;
                pool[i].manager = this;

                pool[i].boton.onClick.RemoveAllListeners();
                pool[i].boton.onClick.AddListener(() => gameObject.SendMessage(baseDatos.baseDatos[o.id].funcion, SendMessageOptions.DontRequireReceiver));

                pool[i].gameObject.SetActive(true);
            }
        }
    }


    
    public void LlaveLeon()
    {
        for (int p = 0; p < puerta.Count; p++)
        {
            for (int i = 0; i < inventario.Count; i++)
            {
                if (puerta[p].id == inventario[i].id)
                {
                    puerta[p].tieneLaLlave = true;
                    puerta.Remove(puerta[p]);
                    EliminarAlgoDeInventario("Llave leon", 0, 1, ObjetoInventarioId.Iten.noAcumulable);
                }
            }

            
        }
    }

    public void LlaveTigre()
    {
        for (int p = 0; p < puerta.Count; p++)
        {

            for (int i = 0; i < inventario.Count; i++)
            {
                if (puerta[p].id == inventario[i].id)
                {
                    puerta[p].tieneLaLlave = true;
                    puerta.Remove(puerta[p]);
                    EliminarAlgoDeInventario("Llave tigre", 1, 1, ObjetoInventarioId.Iten.noAcumulable);
                }
            }
            
        }
    }


}
