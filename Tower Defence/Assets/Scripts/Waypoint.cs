using UnityEngine;

public class Waypoint : MonoBehaviour {
    private const int gridSize = 10;

    public int GridSize {
        get {
            return gridSize;
        }
    }

    public Vector2Int GridPos {
        get {
            return new Vector2Int(Mathf.RoundToInt(transform.position.x / gridSize), Mathf.RoundToInt(transform.position.z / gridSize));
        }
    }

    public void SetColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}