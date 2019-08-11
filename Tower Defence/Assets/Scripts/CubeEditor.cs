using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {
    private TextMesh textMesh;
    private Waypoint waypoint;

    private void Awake() {
        waypoint = GetComponent<Waypoint>();
        textMesh = GetComponentInChildren<TextMesh>();
    }

    private void Update() {
        SnapToGrid();
        UpdateLabels();
    }

    private void SnapToGrid() {
        transform.position = new Vector3Int(waypoint.GridPos.x * waypoint.GridSize, 0, waypoint.GridPos.y * waypoint.GridSize);
    }

    private void UpdateLabels() {
        string labelText = waypoint.GridPos.x + "," + waypoint.GridPos.y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}