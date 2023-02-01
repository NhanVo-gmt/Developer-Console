using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SuggestionDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text text;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);
        if (linkIndex >= 0)
        {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            if (int.TryParse(linkInfo.GetLinkID(), out int index))
            {
                Debug.Log(index);
            }
        }
    }
}
