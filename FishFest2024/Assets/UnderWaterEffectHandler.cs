using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterEffectHandler : MonoBehaviour
{
    [SerializeField]
    private FullScreenPassRendererFeature underWaterEffect;
    private Material underWaterEffectMaterial;

    private void Start()
    {
        underWaterEffectMaterial = underWaterEffect.passMaterial;
        underWaterEffect.SetActive(true);
    }

    private void Update()
    {
        underWaterEffectMaterial.SetFloat("_UnscaledTime", Time.unscaledTime);
    }

    public void DisableEffect()
    {
        underWaterEffect.SetActive(false);
    }

    public void EnableEffect()
    {
        underWaterEffect.SetActive(true);
    }
}
