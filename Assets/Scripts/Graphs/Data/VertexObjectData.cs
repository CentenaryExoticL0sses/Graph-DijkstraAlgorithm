using System;
using UnityEngine;

namespace GraphProject.Graphs.Data
{
    /// <summary>
    /// Структура для хранения даннх об объекте вершины
    /// </summary>
    [Serializable]
    public struct VertexObjectData
    {
        [field: SerializeField]
        public int ID { get; private set; }
        [field: SerializeField]
        public Vector2 Position { get; private set; }

        public VertexObjectData(int id, Vector2 position)
        {
            ID = id;
            Position = position;
        }
    }
}