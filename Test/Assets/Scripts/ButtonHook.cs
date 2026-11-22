using UnityEngine;
using UnityEngine.UI;



public class ButtonHook : MonoBehaviour
{
    void Start()
    {
        // Finds the surviving GameManager automatically and hooks up the click
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            // Replace "YourMethodName" with the actual public method on your GameManager
            GameManager.Instance.startGame();
        }
        else
        {
            Debug.LogError("GameManager.Instance is missing!");
        }
    }
}

