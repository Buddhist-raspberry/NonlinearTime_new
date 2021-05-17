using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Attribute")]
    public GameObject hpBarPrefab;
    public float healthBarYOffset = 2.0f;
    public float maxHealth;
    public float currentHealth;
    private GameObject hpBar;
    [Header("Color")]
    [ColorUsageAttribute(true, true)]
    public Color HighColor;
    [ColorUsageAttribute(true, true)]
    public Color MedColor;
    [ColorUsageAttribute(true, true)]
    public Color LowColor;
    void Start()
    {
        Vector3 offsetTransform = new Vector3(0, healthBarYOffset, 0);
        hpBar = Instantiate(hpBarPrefab, transform.position + offsetTransform, transform.rotation);
        hpBar.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.transform.LookAt(Camera.main.transform);
        Image hpFillColor = GetComponentsInChildren<Image>()[1];
        if (currentHealth > 0.8f * maxHealth)
        {
            hpFillColor.color = HighColor;
        }
        else if (currentHealth > 0.3f * maxHealth)
        {
            hpFillColor.color = MedColor;
        }
        else
        {
            hpFillColor.color = LowColor;
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Slider hpFillSlider = GetComponentsInChildren<Slider>()[0];
        hpFillSlider.value = (float)(currentHealth / maxHealth);
    }
}
