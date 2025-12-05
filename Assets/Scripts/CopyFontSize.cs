using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CopyFontSize : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToCopyFont;
    void Start()
    {
        GetComponent<TextMeshProUGUI>().fontSize = textToCopyFont.fontSize;
    }

}
