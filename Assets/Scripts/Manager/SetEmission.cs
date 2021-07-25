using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEmission : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    Material material;
    public float delay = 0.5f;

    public void FX()
    {
#if UNITY_EDITOR
        Debug.Log("FX time");
#endif
        if (meshRenderers != null)
        {
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.material.EnableKeyword("_EMISSION");
            }
        }
        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.EnableKeyword("_EMISSION");

        StartCoroutine(FXOffDelay(delay));
    } 

    IEnumerator FXOffDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (meshRenderers != null)
        {
            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.material.DisableKeyword("_EMISSION");
            }
        }
        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.DisableKeyword("_EMISSION");
    }
}
