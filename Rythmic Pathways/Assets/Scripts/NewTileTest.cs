using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTileTest : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public float cellSize;
    public float cellSizeY;
    public Color color1;
    public Color color2;
    public GameObject cubeFab;

    private GameObject player;
    private List<GameObject> createdCubes = new List<GameObject>();
    private Vector2Int lastGridCenter;
    private int regenerationThreshold = 2; // Adjust based on desired sensitivity

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastGridCenter = GridPosition(player.transform.position);
        GeneratePlayablePattern();
    }

    private void Update()
    {
        Vector2Int playerGridPos = GridPosition(player.transform.position);
        if (Vector2Int.Distance(playerGridPos, lastGridCenter) > regenerationThreshold)
        {
            lastGridCenter = playerGridPos;
            GeneratePlayablePattern();
        }
    }

    private Vector2Int GridPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / cellSize), Mathf.FloorToInt(position.z / cellSize));
    }

    private void GeneratePlayablePattern()
    {
        ClearGrid();
        lastGridCenter = GridPosition(player.transform.position);

        for (int x = -gridSizeX / 2; x <= gridSizeX / 2; x++)
        {
            for (int y = -gridSizeY / 2; y <= gridSizeY / 2; y++)
            {
                CreateCubeAtPosition(lastGridCenter.x + x, lastGridCenter.y + y);
            }
        }
    }

    private void ClearGrid()
    {
        foreach (GameObject cube in createdCubes)
        {
            Destroy(cube);
        }
        createdCubes.Clear();
    }

    private void CreateCubeAtPosition(int x, int y)
    {
        if (!TileAlreadyExists(x, y))
        {
            Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize) + transform.position - new Vector3(gridSizeX * cellSize / 2, 0, gridSizeY * cellSize / 2);
            GameObject cube = Instantiate(cubeFab, cellPosition, Quaternion.identity, transform);
            Renderer renderer = cube.GetComponent<Renderer>();
            renderer.material.color = (x + y) % 2 == 0 ? color1 : color2;
            createdCubes.Add(cube);
        }
    }

    private bool TileAlreadyExists(int x, int y)
    {
        Vector3 targetPosition = new Vector3(x * cellSize, 0, y * cellSize) + transform.position - new Vector3(gridSizeX * cellSize / 2, 0, gridSizeY * cellSize / 2);
        foreach (GameObject cube in createdCubes)
        {
            if (Vector3.Distance(cube.transform.position, targetPosition) < 1) // Use a small threshold to avoid floating point precision issues
            {
                return true;
            }
        }
        return false;
    }
}
