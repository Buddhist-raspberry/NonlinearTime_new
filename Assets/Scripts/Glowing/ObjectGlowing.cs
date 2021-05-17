using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlowing : Glowing
{
    MeshRenderer m_renderer;
    [ColorUsageAttribute(true,true)]
    public Color SpeedUpColor;
    [ColorUsageAttribute(true,true)]
    public Color SpeedDownColor;
    [ColorUsageAttribute(true,true)]
    public Color InvertColor;
    [ColorUsageAttribute(true,true)]
    public Color SelectedColor;

    protected override void Start(){
        base.Start();
        m_renderer = GetComponent<MeshRenderer>();
    }
    protected override void SetGlowing(float timeScale)
    {
        if (timeScale <= 0){
            m_renderer.material.SetFloat("_RimIntensity", 1);
            m_renderer.material.SetColor("_RimColor", InvertColor);
        }
        else if (timeScale > 0 && timeScale < 1){
            float intensity = timeScale;
            m_renderer.material.SetFloat("_RimIntensity", intensity);
            m_renderer.material.SetColor("_RimColor", SpeedDownColor);
        }
        else if (timeScale > 1 ){
            float intensity = (timeScale-1)/10.0f;
            intensity = Mathf.Min(intensity, 1);
            m_renderer.material.SetFloat("_RimIntensity", intensity);
            m_renderer.material.SetColor("_RimColor", SpeedUpColor);
        }
        else if (timeScale == 1){
            m_renderer.material.SetFloat("_RimIntensity", 0);
            m_renderer.material.SetColor("_RimColor", SpeedUpColor);
        }
    }

    public override void Selected()
    {

        m_renderer.material.SetFloat("_Edge", 0.6f);
        m_renderer.material.SetColor("_EdgeColor", SelectedColor);
    }
    public override void Unselected()
    {
        m_renderer.material.SetFloat("_Edge", 1);
        m_renderer.material.SetColor("_EdgeColor", SelectedColor);
    }
}
