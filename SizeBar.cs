using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeBar : MonoBehaviour
{
    [SerializeField] private Image _SizeBar;

    public void UpdateSizeBar(float maxSize, float currentSize)
    {
        _SizeBar.fillAmount = currentSize;
    
    }
}
