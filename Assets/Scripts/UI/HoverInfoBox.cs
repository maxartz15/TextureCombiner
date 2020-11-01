using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class HoverInfoBox : MonoBehaviour
	{
	    public static HoverInfoBox s_hoverInfoBox { get; private set; } = null;
	
	    [SerializeField]
	    private RectTransform m_infoBoxParent = null;
	    [SerializeField]
	    private Text m_text = null;
	    [SerializeField]
	    private Vector2 m_offset = new Vector2(0, 0);
	
	    private void Awake()
	    {
	        if(s_hoverInfoBox)
	        {
	            DestroyImmediate(gameObject);
	        }
	
	        s_hoverInfoBox = this;
	    }

        private void OnDisable()
        {
            Hide();
        }

		public void Show(string a_text, Vector2 a_position)
	    {
			if(!enabled)
			{
				return;
			}

			// Set text.
			m_text.text = a_text;
			m_text.SetLayoutDirty();

			// Flip when above 50% of the screen.
			if(Input.mousePosition.y < (Screen.height / 2))
			{
				a_position += new Vector2(m_offset.x - (m_infoBoxParent.rect.width / 2), m_offset.y);
			}
			else
			{
				a_position += new Vector2(m_offset.x - (m_infoBoxParent.rect.width / 2), -m_offset.y - m_infoBoxParent.rect.height);
			}

			// Clamp borders.
			if(a_position.x < 0)
			{
				a_position.x = 0;
			}
			else if (a_position.x + m_infoBoxParent.rect.width > Screen.width)
            {
                a_position.x = Screen.width - m_infoBoxParent.rect.width;
            }

            if (a_position.y < 0)
			{
				a_position.y = 0;
			}
			else if (a_position.y + m_infoBoxParent.rect.height > Screen.height)
            {
                a_position.y = Screen.height - m_infoBoxParent.rect.height;
            }

            // Enable object.
            m_infoBoxParent.gameObject.SetActive(true);
			m_infoBoxParent.position = a_position;
	    }
	
	    public void Hide()
	    {
            m_infoBoxParent.gameObject.SetActive(false);
	    }
	}
}