using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGlowing : Glowing
{
    TrailRenderer t_renderer;
    MeshRenderer[] m_renderers;
    [ColorUsageAttribute(true, true)]
    public Color SpeedUpColor;
    [ColorUsageAttribute(true, true)]
    public Color SpeedDownColor;
    [ColorUsageAttribute(true, true)]
    public Color InvertColor;
    [ColorUsageAttribute(true, true)]
    public Color SelectedColor;
    private bool selected;

    protected override void Start()
    {
        base.Start();
        t_renderer = GetComponent<TrailRenderer>();
        m_renderers = GetComponentsInChildren<MeshRenderer>();
        t_renderer.materials[0].SetFloat("_Edge", 1f);
        selected = false;
    }
    protected override void SetGlowing(float timeScale)
    {
        if (!selected)
        {
            if (timeScale <= 0)
            {
                t_renderer.materials[0].SetFloat("_RimIntensity", 1);
                t_renderer.materials[0].SetColor("_RimColor", InvertColor);
                t_renderer.materials[0].SetColor("_EdgeColor", InvertColor);

                foreach (MeshRenderer renderer in m_renderers)
                {
                    renderer.material.SetFloat("_RimIntensity", 1);
                    renderer.material.SetColor("_RimColor", InvertColor);
                }
            }
            else if (timeScale > 0 && timeScale < 1)
            {
                float intensity = timeScale;
                t_renderer.materials[0].SetFloat("_RimIntensity", intensity);
                t_renderer.materials[0].SetColor("_RimColor", SpeedDownColor);
                t_renderer.materials[0].SetColor("_EdgeColor", SpeedDownColor);

                foreach (MeshRenderer renderer in m_renderers)
                {
                    // float intensity = timeScale;
                    renderer.material.SetFloat("_RimIntensity", intensity);
                    renderer.material.SetColor("_RimColor", SpeedDownColor);
                }
            }
            else if (timeScale > 1)
            {
                float intensity = (timeScale - 1) / 10.0f;
                intensity = Mathf.Min(intensity, 1);
                t_renderer.materials[0].SetFloat("_RimIntensity", intensity);
                t_renderer.materials[0].SetColor("_RimColor", SpeedUpColor);
                t_renderer.materials[0].SetColor("_EdgeColor", SpeedUpColor);

                foreach (MeshRenderer renderer in m_renderers)
                {
                    // float intensity = (timeScale - 1) / 10.0f;
                    intensity = Mathf.Min(intensity, 1);
                    renderer.material.SetFloat("_RimIntensity", intensity);
                    renderer.material.SetColor("_RimColor", SpeedUpColor);
                }
            }
            else if (timeScale == 1)
            {
                t_renderer.materials[0].SetFloat("_RimIntensity", 0);
                t_renderer.materials[0].SetColor("_RimColor", SpeedUpColor);
                t_renderer.materials[0].SetColor("_EdgeColor", SpeedUpColor);

                foreach (MeshRenderer renderer in m_renderers)
                {
                    renderer.material.SetFloat("_RimIntensity", 0);
                    renderer.material.SetColor("_RimColor", SpeedUpColor);
                }
            }
        }
        else
        {
            t_renderer.materials[0].SetColor("_EdgeColor", SelectedColor);
        }
    }

    public override void Selected()
    {
        selected = true;

        foreach (MeshRenderer renderer in m_renderers)
        {
            renderer.material.SetFloat("_Edge", 0.6f);
            renderer.material.SetColor("_EdgeColor", SelectedColor);
        }
    }
    public override void Unselected()
    {
        selected = false;

        foreach (MeshRenderer renderer in m_renderers)
        {
            renderer.material.SetFloat("_Edge", 1);
            renderer.material.SetColor("_EdgeColor", SelectedColor);
        }
    }
}
