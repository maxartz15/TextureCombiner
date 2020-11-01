using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TextureCombiner
{
	[RequireComponent(typeof(Button))]
	public class ButtonHandlerWeb : MonoBehaviour, IPointerDownHandler
	{
	    public enum ButtonType
	    {
	        Load,
	        Save
	    }
	
	#pragma warning disable CS0414
	    [SerializeField]
	    private ButtonType m_buttonType = ButtonType.Load;
	#pragma warning restore CS0414
	
	#if UNITY_WEBGL
	    private TextureCombiner m_textureCombiner = null;
	    private Button m_button = null;
	
	    private void Awake()
	    {
	        m_textureCombiner = FindObjectOfType<TextureCombiner>();
	        m_button = this.GetComponent<Button>();
	    }
	#endif
	
	    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
	    {
	#if UNITY_WEBGL
	        m_button.onClick.RemoveAllListeners();
	
	        switch (m_buttonType)
	        {
	            case ButtonType.Load:
	                m_textureCombiner.Load();
	                break;
	            case ButtonType.Save:
	                m_textureCombiner.Save();
	                break;
	            default:
	                break;
	        }
	#endif
	    }
	}
}