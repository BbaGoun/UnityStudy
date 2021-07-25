using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image hpBar;

    public Transform target;
    public CharacterStat stat;
    public Transform cam;

    public void Init(Transform target, CharacterStat stat)
    {
        this.target = target;
        this.stat = stat;
        cam = Camera.main.transform;
    }

    public void LateUpdate()
    {
        SetHP();
    }

    public void SetHP()
    {
        transform.position = target.position;
        transform.LookAt(new Vector3(cam.position.x, cam.position.y, cam.position.z));
        hpBar.fillAmount = NormalizeHP();
    }

    float NormalizeHP()
    {
        return Mathf.Clamp01(stat.currentHP / (float)stat.maxHP);
    }
}
