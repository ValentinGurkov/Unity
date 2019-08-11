using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {
    [SerializeField] private Waypoint startWaypoint, endWaypoint;
    private Dictionary<Vector3Int, Waypoint> grid = new Dictionary<Vector3Int, Waypoint>();

    // Start is called before the first frame update
    private void Start() {
        LoadBlocks();
        ColorStartAndEnd();
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