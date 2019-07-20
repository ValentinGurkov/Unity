using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private int numOfLevels;
    private int currentLevelIndex;
    private int nextSceneIndex;
    private bool isTransitioning;

    [SerializeField] private bool disableCollision = false;
    [SerializeField] private float rcsThrust = 250f;
    [SerializeField] private float mainThrust = 1350f;
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip sucess;
    [SerializeField] private AudioClip death;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem sucessParticles;
    [SerializeField] private ParticleSystem deathParticles;

    // Start is called before the first frame update
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        numOfLevels = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    private void Update() {
        if (!isTransitioning) {
            Rotate();
            Thrust();
            if (Debug.isDebugBuild) {
                RespondToDebugKeys();
            }
        }
    }

    private void Rotate() {
        float rotation = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {
            RotateManually(rotation);
        } else if (Input.GetKey(KeyCode.D)) {
            RotateManually(-rotation);
        }
    }

    private void RotateManually(float rotation) {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation);
        rigidBody.freezeRotation = false;
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            float thrust = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * thrust);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
            if (mainEngineParticles.isStopped) {
                mainEngineParticles.Play();
            }
        } else {
            audioSource.Stop();
            if (mainEngineParticles.isPlaying) {
                mainEngineParticles.Stop();
            }
        }
    }

    private void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            disableCollision = !disableCollision;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (isTransitioning || disableCollision) {
            return;
        }

        switch (collision.gameObject.tag) {
            case "Friendly":
                break;

            case "Finish":
                FinishLevel();
                break;

            default:
                Die();
                break;
        }
    }

    private void FinishLevel() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(sucess);
        sucessParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void Die() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("StartOver", levelLoadDelay);
    }

    private void LoadNextLevel() {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentLevelIndex + 1 < numOfLevels ? currentLevelIndex + 1 : 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void StartOver() {
        SceneManager.LoadScene(0);
    }
}