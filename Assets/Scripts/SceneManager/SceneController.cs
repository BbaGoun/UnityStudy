using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(SceneController)) as SceneController;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public Image fader;

    private void Awake()
    {
        Instance = this;

        fader.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
        fader.gameObject.SetActive(false);
    }

    public IEnumerator FadeScene(int index)
    {
        fader.gameObject.SetActive(true);

        for(float t = 0; t<1; t+= Time.deltaTime)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        while(!asyncOperation.isDone)
            yield return null;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        fader.gameObject.SetActive(false);
    }

    public void LoadScene(int index)
    {
        instance.StartCoroutine(instance.FadeScene(index));
    }

    public void Exit()
    {
#if UNITY_EDITOR
        Debug.Log("Exit!");
#endif
        Application.Quit();
    }
}
