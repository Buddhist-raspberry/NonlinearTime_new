using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlowing : Glowing
{
    SkinnedMeshRenderer[] renderers;
    [ColorUsageAttribute(true,true)]
    public Color SpeedUpColor = new Color(0,2,32f/255,0f);
    [ColorUsageAttribute(true,true)]
    public Color SpeedDownColor= new Color(1,0f/255,0,0);
    [ColorUsageAttribute(true,true)]
    public Color InvertColor= new Color(18f/255,186f/255,1,0);
    [ColorUsageAttribute(true,true)]
    public Color SelectedColor= new Color(1,218f/255,37f/255,0);

    protected override void Start(){
        base.Start();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    protected override void SetGlowing(float timeScale)
    {
        if (timeScale <= 0)
            foreach(SkinnedMeshRenderer renderer in renderers){
                renderer.material.SetFloat("_RimIntensity", 1);
                renderer.material.SetColor("_RimColor", InvertColor);
            }
        else if (timeScale > 0 && timeScale < 1)
            foreach(SkinnedMeshRenderer renderer in renderers){
                float intensity = timeScale;
                renderer.material.SetFloat("_RimIntensity", intensity);
                renderer.material.SetColor("_RimColor", SpeedDownColor);
            }
        else if (timeScale > 1 )
            foreach(SkinnedMeshRenderer renderer in renderers){
                float intensity = (timeScale-1)/10.0f;
                intensity = Mathf.Min(intensity, 1);
                renderer.material.SetFloat("_RimIntensity", intensity);
                renderer.material.SetColor("_RimColor", SpeedUpColor);
            }
        else if (timeScale == 1)
            foreach(SkinnedMeshRenderer renderer in renderers){
                renderer.material.SetFloat("_RimIntensity", 0);
                renderer.material.SetColor("_RimColor", SpeedUpColor);
            }
    }

    public override void Selected()
    {
        foreach(SkinnedMeshRenderer renderer in renderers){
            renderer.material.SetFloat("_Edge", 0.6f);
            renderer.material.SetColor("_EdgeColor", SelectedColor);
        }
    }
    public override void Unselected()
    {
        foreach(SkinnedMeshRenderer renderer in renderers){
            renderer.material.SetFloat("_Edge", 1);
            renderer.material.SetColor("_EdgeColor", SelectedColor);
        }
    }

    public void SetNormal(){
        SetGlowing(1.0f);
    }
}
