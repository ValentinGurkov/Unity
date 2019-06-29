using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour {
    [SerializeField] private Terminal connectedToTerminal;

    // TODO calculate these two if possible
    [SerializeField] private int charactersWide = 40;

    [SerializeField] private int charactersHigh = 14;

    private Text screenText;

    private void Start() {
        screenText = GetComponentInChildren<Text>();
        WarnIfTerminalNotConneced();
    }

    private void WarnIfTerminalNotConneced() {
        if (!connectedToTerminal) {
            Debug.LogWarning("Display not connected to a terminal");
        }
    }

    // Akin to monitor refresh
    private void Update() {
        if (connectedToTerminal) {
            screenText.text = connectedToTerminal.GetDisplayBuffer(charactersWide, charactersHigh);
        }
    }
}