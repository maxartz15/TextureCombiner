using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	[RequireComponent(typeof(RectTransform)), ExecuteInEditMode]
	public class FitRect : MonoBehaviour
	{
		public bool m_fitHeight = false;
		public bool m_fitWidth = false;
		public RectTransform m_rectToFit = null;

		private RectTransform m_rectTransform = null;
		private Rect m_prevRect = new Rect();

		private void Awake()
		{
			m_rectTransform = GetComponent<RectTransform>();
		}

		private void Update()
		{
			if(!m_rectToFit)
			{
				return;
			}

			if(m_prevRect != m_rectToFit.rect)
			{
				m_prevRect = m_rectToFit.rect;

				m_rectTransform.sizeDelta = new Vector2(
                        m_fitWidth ? m_rectToFit.rect.width : m_rectTransform.rect.width,
                        m_fitHeight ? m_rectToFit.rect.height : m_rectToFit.rect.height
                    );
			}
		}
    }
}