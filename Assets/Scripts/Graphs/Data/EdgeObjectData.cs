using System;
using UnityEngine;

namespace GraphProject.Graphs.Data
{
    /// <summary>
    /// Структура для хранения данных об объекте ребра
    /// </summary>
    [Serializable]
    public struct EdgeObjectData
    {
        [field: SerializeField] public int FirstVertexID { get; private set; }
        [field: SerializeField]public int SecondVertexID { get; private set; }

        public EdgeObjectData(int firstID, int secondID)
        {
            FirstVertexID = firstID;
            SecondVertexID = secondID;
        }
    }
}