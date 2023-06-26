using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimationOnOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Text m_Text;
    private Color m_InitialColor;

    private int m_InitialFontSize;

    private Shadow m_TextShadow;
    private Color m_InitialTextShadowColor;

    private void Start()
    {
        m_Text = gameObject.GetComponentInChildren<Text>();
        m_TextShadow = gameObject.GetComponentInChildren<Shadow>();
        m_InitialColor = m_Text.color;
        m_InitialFontSize = m_Text.fontSize;
        m_InitialTextShadowColor = m_TextShadow.effectColor;
    }

    private void ResetButtonStyle()
    {
        m_Text.color = m_InitialColor;
        m_TextShadow.effectColor = m_InitialTextShadowColor;
        m_Text.fontSize = m_InitialFontSize;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        m_Text.color = Color.yellow;
        m_TextShadow.effectColor = Color.blue;
        m_Text.fontSize = m_InitialFontSize + 5;
    }

    public void OnPointerExit(PointerEventData data)
    {
        ResetButtonStyle();
    }

    public void OnPointerClick(PointerEventData data)
    {
        ResetButtonStyle();
    }

}
