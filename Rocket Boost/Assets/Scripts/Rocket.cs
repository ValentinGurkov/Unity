using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField] private float rcsThrust = 250f;
    [SerializeField] private float mainThrust = 1350f;

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
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
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
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;

            default:
                state = State.Dying;
                Invoke("StartOver", 1f);
                break;
        }
    }

    private void StartOver() {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene() {
        SceneManager.LoadScene(1);
    }
}