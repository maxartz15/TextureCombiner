using UnityEngine;
using UnityEngine.EventSystems;

namespace TextureCombiner
{
	public class OpenURL : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private string m_url = "";
		[SerializeField]
		private bool m_httpsOnly = true;

		public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
		{
			if(m_httpsOnly && !m_url.StartsWith("https://"))
			{
				m_url = "https://" + m_url;
			}

			Application.OpenURL(m_url);
		}

	}
}