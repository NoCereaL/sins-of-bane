using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverSound : MonoBehaviour, IPointerEnterHandler
{

    public AudioSource hoverAudio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverAudio.Play();
    }
}
