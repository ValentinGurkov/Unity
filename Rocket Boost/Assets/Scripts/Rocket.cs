using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField] private float rcsThrust = 250f;
    [SerializeField] private float mainThrust = 1350f;
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip sucess;
    [SerializeField] private AudioClip death;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem sucessParticles;
    [SerializeField] private ParticleSystem deathParticles;

    private enum State { Alive, Dying, Transcending };

    private State state = State.Alive;

    // Start is called before the first frame update
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update() {
        if (state == State.Alive) {
            Rotate();
            Thrust();
        }
    }

    private void Rotate() {
        rigidBody.freezeRotation = true;
        float rotation = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotation);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotation);
        }
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

    private void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) {
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
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(sucess);
        sucessParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void Die() {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("StartOver", levelLoadDelay);
    }

    private void LoadNextScene() {
        SceneManager.LoadScene(1);
    }

    private void StartOver() {
        SceneManager.LoadScene(0);
    }
}