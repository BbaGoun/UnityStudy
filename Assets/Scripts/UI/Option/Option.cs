using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject optionHead;
    public Slider bgmSlider;
    public Slider fxSlider;
    public AudioSource bgm;
    public AudioSource fx;

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            OptionToggle();
        }
    }

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(delegate { UpdateBgm(); });
        fxSlider.onValueChanged.AddListener(delegate { UpdateFx(); });
    }

    void UpdateBgm()
    {
        bgm.volume = bgmSlider.value;
    }

    void UpdateFx()
    {
        fx.volume = fxSlider.value;
    }
    public void OptionToggle()
    {
        optionHead.SetActive(!optionHead.activeSelf);
    }

    public void PauseToggle()
    {
        Time.timeScale = 1 - Time.timeScale;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        Debug.Log("Exit!");
#endif
        Application.Quit();
    }
}
