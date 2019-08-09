using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] private float speed = 20f;

    [Tooltip("In m")] [SerializeField] private float xRange = 15f;
    [Tooltip("In m")] [SerializeField] private float yRange = 10f;

    [Header("Screen Position")]
    [SerializeField] private float positionPitchFactor = -5f;

    [SerializeField] private float positionYawFactor = 5f;

    [Header("Control Throw")]
    [SerializeField] private float controlPitchFactor = -20f;

    [SerializeField] private float controlRollFactor = -20f;

    private float xThrow, yThrow;
    private bool isControlEnabled = true;
    private Rigidbody rigidBody;
    private BoxCollider[] boxColliders;

    // Use this for initialization
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        boxColliders = gameObject.GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    private void Update() {
        if (isControlEnabled) {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void OnPlayerDeath() { // called by string reference
        xRange = 100f;
        yRange = 100f;
        rigidBody.isKinematic = false;
        foreach (BoxCollider boxCollider in boxColliders) {
            boxCollider.isTrigger = false;
        }
        isControlEnabled = false;
    }
}