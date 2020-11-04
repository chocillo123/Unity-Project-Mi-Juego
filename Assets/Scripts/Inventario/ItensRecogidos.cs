using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public struct RecogidosId
{

    public int id;


    public RecogidosId(int id)
    {
        this.id = id;
    }

}


[Serializable]
public class ItensRecogidos : MonoBehaviour
{   

    public List<RecogidosId> recogidos;
    public InventarioManager manager;


    public void AgregarRecogido(int id)
    {
        recogidos.Add(new RecogidosId(id));
    }


    public void Start()
    {
        
    }


    public void Save()
    {
        FileStream fs = new FileStream("save1.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, recogidos);
        fs.Close();
    }

    public void Load()
    {
        using (Stream stream = File.Open("save1.dat", FileMode.Open))
        {
            var bformatter = new BinaryFormatter();

            recogidos = (List<RecogidosId>)bformatter.Deserialize(stream);
        }
    }


}
