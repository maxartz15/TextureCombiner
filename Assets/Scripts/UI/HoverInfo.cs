using UnityEngine;
using UnityEngine.EventSystems;

namespace TextureCombiner
{
	public class HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
	    [SerializeField, Multiline]
	    private string m_text = "";

		public void OnPointerEnter(PointerEventData eventData)
	    {
			HoverInfoBox.s_hoverInfoBox.Show(m_text, eventData.position);
		}
	
	    public void OnPointerExit(PointerEventData eventData)
	    {
			HoverInfoBox.s_hoverInfoBox.Hide();
		}
	}
}