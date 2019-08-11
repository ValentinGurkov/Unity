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
        transform.position = waypoint.GridPos;
    }

    private void UpdateLabels() {
        string labelText = waypoint.GridPos.x / waypoint.GridSize + "," + waypoint.GridPos.z / waypoint.GridSize;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}