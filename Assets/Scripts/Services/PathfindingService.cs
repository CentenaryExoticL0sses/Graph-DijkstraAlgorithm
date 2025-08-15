using System.Collections.Generic;
using GraphProject.Core.Algorithms;
using GraphProject.Core.DataStructures;

namespace GraphProject.Services
{

    /// <summary>
    /// Сервис для поиска кратчайшего пути в графе.
    /// Инкапсулирует логику алгоритма Дейкстры.
    /// </summary>
    public class PathfindingService
    {
        /// <summary>
        /// Находит кратчайший путь между двумя вершинами.
        /// </summary>
        /// <param name="graph">Логический граф для поиска.</param>
        /// <param name="startID">ID начальной вершины.</param>
        /// <param name="finishID">ID конечной вершины.</param>
        /// <returns>Список ID вершин, составляющих путь.</returns>
        public List<int> FindShortestPath(Graph graph, int startID, int finishID)
        {
            Dijkstra dijkstra = new(graph);
            return dijkstra.FindShortestPath(startID, finishID);
        }
    }
}