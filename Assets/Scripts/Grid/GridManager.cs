using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    [SerializeField]private Tilemap tilemap;
    [SerializeField]private Vector2Int gridSize;
    private Node[,] grid;
    private int _cellSize;
    
    public static GridManager Instance => instance;
    public Tilemap Tilemap => tilemap;
    
    #region Editor Testing Variables
    [SerializeField] private GameObject _gridVisualizerPrefab;
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
        
        if(pathVisualizers.Count > 0) ClearVisuals();
        GenerateGrid();
        PopulatePath();
    }

    void GenerateGrid()
    {
        grid = new Node[gridSize.x, gridSize.y];
        _cellSize = Mathf.RoundToInt(tilemap.cellSize.x);
        //Debug.Log(gridSize.y);
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
        foreach (Transform child in tilemap.transform)
        {
            GetNode(child.position).placedObject = child.gameObject;
        }
    }
    
    public Node GetNode(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / _cellSize);
        int y = Mathf.RoundToInt(worldPos.z / _cellSize);

        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
        {
            return grid[x, y];
        }
        Debug.LogWarning("No node found at given position.");
        return null;
    }
    
    //Testing
    List<GameObject> pathVisualizers = new List<GameObject>();
    
    [Button("Generate Grid Visuals")]
    public void VisualisePath()
    {
        if(grid == null) GenerateGrid();
        foreach (Node n in grid)
        {
            GameObject visual = Instantiate(_gridVisualizerPrefab, n.GridPosition, Quaternion.identity);
            visual.transform.localScale = new Vector3(_cellSize, _cellSize, _cellSize);
            pathVisualizers.Add(visual);
        }
    }

    [Button("Clear Grid Visuals")]
    public void ClearVisuals()
    {
        grid = null;
        foreach (GameObject go in pathVisualizers)
        {
            Destroy(go);
        }
    }
}
