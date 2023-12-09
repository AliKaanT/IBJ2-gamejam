using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[DefaultExecutionOrder(-30), SerializeField]
public class DjkstraManager : MonoBehaviour
{
    private int Size;

    public Transform NPCFollowPoints;

    public Transform[] allpoints;

    public float[,] graph;

    public static DjkstraManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Transform Run(Transform src, Transform dest)
    {

        if (Instance == null)
        {
            Instance = this;
        }

        Size = NPCFollowPoints.childCount + 2;
        allpoints = new Transform[Size];
        allpoints[0] = src;
        int count = 1;
        foreach (Transform child in NPCFollowPoints)
        {
            allpoints[count] = child;
            count++;
        }

        allpoints[Size - 1] = dest;


        graph = new float[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i == j)
                {
                    graph[i, j] = 0;
                }
                else
                {
                    Vector2 point1 = allpoints[i].position;
                    Vector2 point2 = allpoints[j].position;

                    RaycastHit2D hit = Physics2D.Linecast(point1, point2, LayerMask.GetMask("Wall"));
                    if (hit.collider == null)
                        graph[i, j] = (int)Vector3.Distance(allpoints[i].position, allpoints[j].position);
                    else
                        graph[i, j] = 0;
                }
            }
        }

        List<int> path = Dijkstra(0, Size - 1); // Assuming source node is 0

        return allpoints[path[1]];
    }


    // A utility function to find the vertex with the minimum distance value,
    // from the set of vertices not yet included in the shortest path tree
    private int MinDistance(float[] dist, bool[] sptSet)
    {
        float min = float.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < Size; v++)
        {
            if (sptSet[v] == false && dist[v] <= min)
            {
                min = dist[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    // // Example usage in Unity (you can call this function wherever needed)
    // public Vector3 Run(Transform me, int dest)
    // {
    //     graph[0, 0] = 0;

    //     Transform[] newWaypoints = new Transform[waypoints.Length + 1];
    //     newWaypoints[0] = me;
    //     for (int i = 1; i < newWaypoints.Length; i++)
    //     {
    //         newWaypoints[i] = waypoints[i - 1];
    //     }

    //     for (int i = 1; i < waypoints.Length; i++)
    //     {
    //         Vector2 point1 = me.position;
    //         Vector2 point2 = waypoints[i].position;

    //         RaycastHit2D hit = Physics2D.Linecast(point1, point2, LayerMask.GetMask("Wall"));
    //         if (hit.collider == null)
    //             graph[0, 1] = (int)Vector3.Distance(point2, point1);
    //         else
    //             graph[0, 1] = 0;
    //     }

    //     List<int> path = Dijkstra(0, dest); // Assuming source node is 0
    //     Debug.Log(path.Count);
    //     return waypoints[path[1]].position;
    // }

    public List<int> Dijkstra(int src, int dest)
    {
        float[] dist = new float[Size]; // The output array dist[i] holds the shortest distance from src to i

        bool[] sptSet = new bool[Size]; // sptSet[i] will be true if vertex i is included in the shortest
                                        // path tree or the shortest distance from src to i is finalized

        int[] parent = new int[Size]; // To store the shortest path tree

        // Initialize all distances as INFINITE and sptSet[] as false
        for (int i = 0; i < Size; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
        }

        // Distance of source vertex from itself is always 0
        dist[src] = 0;

        // Find shortest path for all vertices
        for (int count = 0; count < Size - 1; count++)
        {
            int u = MinDistance(dist, sptSet);

            sptSet[u] = true;

            for (int v = 0; v < Size; v++)
            {
                if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue &&
                    dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    parent[v] = u;
                }
            }
        }

        // Trace the path from destination to source
        List<int> path = new List<int>();
        int currentNode = dest;
        while (currentNode != src)
        {
            path.Add(currentNode);
            currentNode = parent[currentNode];
        }
        path.Add(src);
        path.Reverse(); // Reverse the list to get the path from source to destination

        return path;
    }

    private void PrintPath(int[] dist)
    {

        string path = "";

        for (int i = 0; i < dist.Length; i++)
        {
            path += dist[i] + ", ";
        }

        Debug.Log(path);
    }
}