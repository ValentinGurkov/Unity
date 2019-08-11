using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private List<Waypoint> path;

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(MoveAcrossWaypoints());
    }

    private IEnumerator<WaitForSeconds> MoveAcrossWaypoints() {
        foreach (Waypoint waypoint in path) {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}