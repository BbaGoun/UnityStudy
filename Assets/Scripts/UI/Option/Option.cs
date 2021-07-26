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
    AudioSource playerFx;
    AudioSource enemyFx;

    private void Awake()
    {
        playerFx = AudioManager.Instance.source;
        enemyFx = EnemyAudioManager.Instance.source;
    }

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
        if (bgm.volume <= 0)
            bgm.enabled = false;
        else
            bgm.enabled = true;
    }

    void UpdateFx()
    {
        playerFx.volume = fxSlider.value;
        if (playerFx.volume <= 0)
            playerFx.enabled = false;
        else
            playerFx.enabled = true;

        enemyFx.volume = fxSlider.value;
        if (enemyFx.volume <= 0)
            enemyFx.enabled = false;
        else
            enemyFx.enabled = true;
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
