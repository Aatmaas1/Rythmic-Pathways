using UnityEngine;
using System.Collections.Generic;

public class DynamicPathGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Assign your tile prefab here in the Inspector
    public float tileLength = 1f; // The length of one side of your tile
    public int pathAhead = 5; // How many tiles ahead of the player to generate

    private GameObject player;
    private Vector2Int playerGridPositionLastFrame;
    private HashSet<Vector2Int> existingTiles = new HashSet<Vector2Int>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player GameObject not found. Make sure your player is tagged as 'Player'.");
            return;
        }

        playerGridPositionLastFrame = GridPosition(player.transform.position);
        GenerateInitialPath(playerGridPositionLastFrame);
    }

    void Update()
    {
        Vector2Int currentGridPos = GridPosition(player.transform.position);

        if (currentGridPos != playerGridPositionLastFrame)
        {
            GeneratePathInDirection(currentGridPos - playerGridPositionLastFrame);
            playerGridPositionLastFrame = currentGridPos;
        }
    }

    Vector2Int GridPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x / tileLength), Mathf.RoundToInt(position.z / tileLength));
    }

    void GenerateInitialPath(Vector2Int startGridPos)
    {
        for (int i = 0; i < pathAhead; i++)
        {
            Vector2Int tilePos = new Vector2Int(startGridPos.x, startGridPos.y + i);
            CreateTileAtGridPosition(tilePos);
        }
    }

    void GeneratePathInDirection(Vector2Int direction)
    {
        // Simplified example: Generate path directly in the movement direction
        for (int i = 0; i < pathAhead; i++)
        {
            Vector2Int nextTilePos = playerGridPositionLastFrame + direction * i;
            CreateTileAtGridPosition(nextTilePos);
        }
    }

    void CreateTileAtGridPosition(Vector2Int gridPosition)
    {
        if (existingTiles.Contains(gridPosition))
            return; // Tile already exists

        Vector3 worldPosition = new Vector3(gridPosition.x * tileLength, 0, gridPosition.y * tileLength);
        Instantiate(tilePrefab, worldPosition, Quaternion.identity, transform);
        existingTiles.Add(gridPosition);
    }
}
