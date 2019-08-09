using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int scorePerHit = 12;
    [SerializeField] private int healthPoints = 10;
    [SerializeField] private GameObject deathFX;
    [SerializeField] private Transform parent;
    private ScoreBoard scoreBoard;

    // Start is called before the first frame update
    private void Start() {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider() {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        print("hit!");
        print(healthPoints);
        ProcessHit();
        if (healthPoints < 1) {
            KillEnemy();
        }
    }

    private void ProcessHit() {
        healthPoints--;
        scoreBoard.ScoreHit(scorePerHit);
    }

    private void KillEnemy() {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}