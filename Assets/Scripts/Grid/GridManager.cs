using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

/*
 * Script that makes the grid upon which towers are placed
 * Also used to get and check the Nodes in the grid
 * Handles both grid preview for when trying to build a tower
 * as well as editor options to visualize the grid and which nodes are occupied
 * Also manages centering the tilemap under which the path is built at a specific position on the grid
 * through editor variables
 */
public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    [SerializeField]private Vector2Int gridSize;
    [SerializeField]private Vector2Int gridAlignmentPosition;
    [SerializeField]private Tilemap tilemap;
    [SerializeField]private int cellSize;
    [SerializeField] private GameObject gridPreviewPrefab;
    private Node[,] _grid;
    
    
    public static GridManager Instance => _instance;
    
    #region Editor Testing Variables
    public GameObject _gridVisualizerPrefab;
    #endregion
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        
        #if UNITY_EDITOR
        if(pathVisualizers.Count > 0) ClearVisuals();
        #endif
        GenerateGrid();
        PopulatePath();
    }

    private void Start()
    {
        EventBus<ToggleHoverEvent>.OnEvent += HandlePreview;
    }

    private void OnDestroy()
    {
        EventBus<ToggleHoverEvent>.OnEvent -= HandlePreview;
    }

    void GenerateGrid()
    {
        _grid = new Node[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 worldPos = new Vector3(x * cellSize, 0, y * cellSize);
                _grid[x, y] = new Node(worldPos);
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
        int x = Mathf.RoundToInt(worldPos.x / cellSize);
        int y = Mathf.RoundToInt(worldPos.z / cellSize);
        
        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
        {
            return _grid[x, y];
        }
        return null;
    }

    void AlignTilemapToGrid()
    {
        tilemap.transform.position = _grid[gridAlignmentPosition.x, gridAlignmentPosition.y].GridPosition + Vector3.up * 0.5f;
    }
    
    List<GameObject> previewVisualizers = new List<GameObject>();
    private bool isPreviewing;
    private void HandlePreview(ToggleHoverEvent e)
    {
        if (e.hoverValue)
        {
            PreviewPlacement();
        }
        else
        {
            ClearPreview();
        }
    }
    
    public void PreviewPlacement()
    {
        if(isPreviewing || gridPreviewPrefab == null) return;
        isPreviewing = true;
        if(_grid == null) GenerateGrid();
        foreach (Node n in _grid)
        {
            if (n.placedObject != null) continue;
            GameObject visual = Instantiate(gridPreviewPrefab, n.GridPosition, Quaternion.identity);
            visual.transform.localScale = new Vector3(cellSize / 1.15f, cellSize / 1.15f, cellSize / 1.15f);
            visual.transform.parent = tilemap.transform;
            previewVisualizers.Add(visual);
        }
    }
    
    public void ClearPreview()
    {
        foreach (GameObject go in previewVisualizers)
        {
            Destroy(go);
        }
        previewVisualizers.Clear();
        isPreviewing = false;
    }
    
    //Testing
#if UNITY_EDITOR
    List<GameObject> pathVisualizers = new List<GameObject>();
    
    [Button("Generate Grid Visuals")]
    public void VisualisePath()
    {
        if(_grid == null) GenerateGrid();
        PopulatePath();
        foreach (Node n in _grid)
        {
            GameObject visual = Instantiate(_gridVisualizerPrefab, n.GridPosition, Quaternion.identity);
            visual.transform.localScale = new Vector3(cellSize / 1.15f, cellSize / 1.15f, cellSize / 1.15f);
            
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
        _grid = null;
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
