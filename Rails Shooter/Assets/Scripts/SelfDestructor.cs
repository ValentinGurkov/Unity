using UnityEngine;

public class SelfDestructor : MonoBehaviour {

    // Start is called before the first frame update
    private void Start() {
        Destroy(gameObject, 5f);
    }
}