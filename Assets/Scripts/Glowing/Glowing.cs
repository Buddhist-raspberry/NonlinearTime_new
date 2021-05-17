using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Glowing : MonoBehaviour
{
    ChronosBehaviour m_ChronosBehaviour;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_ChronosBehaviour = GetComponent<ChronosBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        SetGlowing(m_ChronosBehaviour.GetLocalTimeScale());
    }

    protected abstract void SetGlowing(float timeScale);

    public abstract void Selected();
    public abstract void Unselected();
}
