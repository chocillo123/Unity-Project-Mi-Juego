using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class InventarioBaseDatos : ScriptableObject
{
    [System.Serializable]
    public struct ObjetoInventario
    {
        public string nombre;
        public Sprite sprite;
        public Iten iten;

        public enum Iten
        {
            acumulable,
            noAcumulable
        }

        public string funcion;
       
    }
    

    public ObjetoInventario[] baseDatos;

}

