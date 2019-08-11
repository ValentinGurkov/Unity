using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {
    [SerializeField] private Waypoint startWaypoint, endWaypoint;
    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    private void Start() {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
    }

    private void ExploreNeighbours() {
        foreach (Vector2Int direction in directions) {
            Vector2Int explorationCoordinates = startWaypoint.GridPos + direction;
            if (grid.ContainsKey(explorationCoordinates)) {
                grid[explorationCoordinates].SetColor(Color.blue);
            }
        }
    }

    private void LoadBlocks() {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints) {
            if (!grid.ContainsKey(waypoint.GridPos)) {
                grid.Add(waypoint.GridPos, waypoint);
            } else {
                Debug.LogWarning("Skipping overlapping block: " + waypoint);
            }
        }
    }

    private void ColorStartAndEnd() {
        startWaypoint.SetColor(Color.green);
        endWaypoint.SetColor(Color.red);
    }
}