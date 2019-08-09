using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [Tooltip("In seconds")] [SerializeField] private GameObject deathFX;
    [Tooltip("FX prefab on player")] [SerializeField] private float levelReloadDelay = 1.5f;

    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        deathFX.SetActive(true);
    }

    private void StartDeathSequence() {
        SendMessage("OnPlayerDeath");
        Invoke("ReloadScene", levelReloadDelay);
    }

    private void ReloadScene() { // used in string reference
        SceneManager.LoadScene(1);
    }
}