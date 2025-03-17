using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    [SerializeField]private Vector2Int gridSize;
    [SerializeField]private Tilemap tilemap;
    [SerializeField]private int _cellSize;
    private Node[,] grid;
    
    
    public static GridManager Instance => instance;
    
    #region Editor Testing Variables
    public GameObject _gridVisualizerPrefab;
    #endregion
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        #if UNITY_EDITOR
        if(pathVisualizers.Count > 0) ClearVisuals();
        #endif
        GenerateGrid();
        PopulatePath();
    }

    void GenerateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                //Debug.Log(y);
                Vector3 worldPos = new Vector3(x * _cellSize, 0, y * _cellSize);
                grid[x, y] = new Node(worldPos);
            }
        }
    }

    void PopulatePath()
    {
        AlignTilemapToGrid();
        foreach (Transform child in tilemap.transform)
        {
            Node pathNode = GetNode(child.position);
            if (pathNode != null)
            {
                pathNode.placedObject = child.gameObject;
            }
        }
    }
    
    public Node GetNode(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / _cellSize);
        int y = Mathf.RoundToInt(worldPos.z / _cellSize);

        //Debug.Log(x + ", " + y);
        
        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
        {
            return grid[x, y];
        }
        Debug.LogWarning("No node found at given position.");
        return null;
    }

    void AlignTilemapToGrid()
    {
        tilemap.transform.position = grid[gridSize.x - 1, gridSize.y - 1].GridPosition + Vector3.up * 0.5f;
    }
    
    //Testing
#if UNITY_EDITOR
    List<GameObject> pathVisualizers = new List<GameObject>();
    
    [Button("Generate Grid Visuals")]
    public void VisualisePath()
    {
        if(grid == null) GenerateGrid();
        PopulatePath();
        foreach (Node n in grid)
        {
            GameObject visual = Instantiate(_gridVisualizerPrefab, n.GridPosition, Quaternion.identity);
            visual.transform.localScale = new Vector3(_cellSize / 1.15f, _cellSize / 1.15f, _cellSize / 1.15f);
            if (n.placedObject != null)
            {
                visual.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
            pathVisualizers.Add(visual);
        }
    }

    [Button("Clear Grid Visuals")]
    public void ClearVisuals()
    {
        grid = null;
        foreach (GameObject go in pathVisualizers)
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Called during edit mode");
                DestroyImmediate(go);
            }
            else
            {
                Debug.Log("Called during play mode");
                Destroy(go);
            }
        }

        if (Application.isPlaying)
        {
            GenerateGrid();
            PopulatePath();
        }
    }
#endif
}
