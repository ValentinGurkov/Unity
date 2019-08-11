using UnityEngine;

public class Waypoint : MonoBehaviour {
    private const int gridSize = 10;
    private Vector2Int gridPos;

    public int GridSize {
        get {
            return gridSize;
        }
    }

    public Vector3Int GridPos {
        get {
            return new Vector3Int(Mathf.RoundToInt(transform.position.x / gridSize) * gridSize, 0, Mathf.RoundToInt(transform.position.z / gridSize) * gridSize);
        }
    }

    public void SetColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}