using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGlowing : Glowing
{
   MeshRenderer m_renderer;
    [ColorUsageAttribute(true,true)]
    public Color SpeedUpColor = new Color(1,0,0,0);
    [ColorUsageAttribute(true,true)]
    public Color SpeedDownColor= new Color(0,2,32f/255,0f);
    [ColorUsageAttribute(true,true)]
    public Color InvertColor= new Color(18f/255,186f/255,1,0);
    [ColorUsageAttribute(true,true)]
    public Color SelectedColor= new Color(1,218f/255,37f/255,0);

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

        m_renderer.material.SetFloat("_RimIntensity", 1);
        m_renderer.material.SetColor("_RimColor", InvertColor);
    }
    public override void Unselected()
    {
        m_renderer.material.SetFloat("_RimIntensity", 0);
        m_renderer.material.SetColor("_RimColor", SpeedUpColor);
    }
}
