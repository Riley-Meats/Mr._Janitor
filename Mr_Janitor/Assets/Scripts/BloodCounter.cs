using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodCounter : MonoBehaviour
{
    public static BloodCounter instance { get; private set; }

    public Image mask;
    float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.height;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * value);
    }

    void Awake()
    {
        instance = this;
    }
}
