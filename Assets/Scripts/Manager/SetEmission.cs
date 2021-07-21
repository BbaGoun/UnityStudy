using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEmission : MonoBehaviour
{
    public SkinnedMeshRenderer skinMeshRenderer;
    Material material;

    bool isHitted = false;

    public float duration = 1.0f;
    float timeLeft = 0f;

    private void Start() 
    {
        material = skinMeshRenderer.material;
    }

    public void FX()
    {
        Debug.Log("FX time");
        isHitted = true;
        material.EnableKeyword("_EMISSION");
    } 

    private void Update() 
    {
        if(isHitted)
        {
            if(timeLeft < duration)
            {
                timeLeft += Time.deltaTime;
            }
            else
            {
                material.DisableKeyword("_EMISSION");
                isHitted = false;
                timeLeft = 0f;
            }
        }
    }
}
