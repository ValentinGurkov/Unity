using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parent;
    private ScoreBoard scoreBoard;

    // Start is called before the first frame update
    private void Start() {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
        scoreBoard.ScoreHit();
    }
}