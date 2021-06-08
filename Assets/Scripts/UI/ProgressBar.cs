using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    public float max = 0f;
    public float current = 0f;
    public Image mask = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetFillAmount();
    }

    void GetFillAmount()
    {
        float fill = current / max;

        mask.fillAmount = fill;
    }
}
