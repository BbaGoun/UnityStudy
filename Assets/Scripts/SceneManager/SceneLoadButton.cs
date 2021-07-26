using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    Button button;
    SceneController sceneController;

    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("Sibal");
#endif
        sceneController = SceneController.Instance;
        button.onClick.AddListener(delegate { sceneController.LoadScene(0); });
    }
}
