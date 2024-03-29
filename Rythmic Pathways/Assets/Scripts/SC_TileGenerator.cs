using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TileGenerator : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeY;
    public float cellSize;

    public float cellSizeY;
    public bool cubeColor;
    public Color color1;
    public Color color2;

    public GameObject cubeFab;
    GameObject player;
    List<GameObject> createdCubes = new List<GameObject>(); // Initialize the list

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GeneratePlayablePattern();
        cubeColor = false;
    }

    // New method for generating a playable pattern

        private void GeneratePlayablePattern()
        {
            transform.position = player.transform.position;
            // Get player's starting grid position
            int startX = ((int)player.transform.position.x);
            int startY = ((int)player.transform.position.z);

            // Immediately create a cube at the player's position
            CreateCubeAtPosition(startX, startY);

            // Set the current position to the player's starting position
            int currentX = startX;
            int currentY = startY;

            // Directions we can move in: right, left, up
            Vector2Int[] directions = new Vector2Int[] {
            new Vector2Int(1, 0), // right
            new Vector2Int(-1, 0), // left
            new Vector2Int(0, 1)  // up
        };

            // Continue generating the path until we reach the top row
            while (currentY < gridSizeY - 1)
            {
                Vector2Int direction = directions[Random.Range(0, directions.Length)];
                int nextX = currentX + direction.x;
                int nextY = currentY + direction.y;

                // Check bounds and if tile already exists
                if (nextX >= 0 && nextX < gridSizeX + player.transform.position.x && nextY >= 0 && nextY < gridSizeY + player.transform.position.z && !TileAlreadyExists(nextX, nextY))
                {
                    CreateCubeAtPosition(nextX, nextY);
                    currentX = nextX;
                    currentY = nextY;
                }
            }
        }



        private bool TileAlreadyExists(int x, int y)
        {
            Vector3 targetPosition = new Vector3(x * cellSize, 0, y * cellSize);
            foreach (GameObject cube in createdCubes)
            {
                if (cube.transform.position == targetPosition)
                    return true;
            }
            return false;
        }
    


    private void CreateCubeAtPosition(int x, int y)
    {
        Vector3 cellPosition = new Vector3(x * cellSize, 0f, y * cellSize);
        GameObject cube = GameObject.Instantiate(cubeFab);
        Renderer renderer = cube.GetComponent<Renderer>();

        if (cubeColor == false)
        {
            renderer.material.color = color1;
            cubeColor = true;
        }
        else
        {
            renderer.material.color = color2;
            cubeColor = false;
        }

        cube.transform.position = cellPosition;
        cube.transform.localScale = new Vector3(cellSize, cellSizeY, cellSize);
        cube.transform.parent = transform;

        createdCubes.Add(cube); // Keep track of created cubes
    }

    public void DestroyGrid()
    {
        player.GetComponent<SC_CHARACTER_MOVE>().moveDirection = Vector3.zero;
        // Destroy all cubes
        foreach (GameObject cube in createdCubes)
        {
            Destroy(cube);
        }
        createdCubes.Clear(); // Clear the list of references to the destroyed cubes

        GeneratePlayablePattern(); // Generate a new pattern
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            DestroyGrid();
        }
    }
}
