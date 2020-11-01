using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleInvoke : MonoBehaviour
	{
	    private Toggle m_toggle = null;
	
	    private void Awake()
	    {
	        m_toggle = GetComponent<Toggle>();
	    }
	
	    private void OnEnable()
	    {
	        m_toggle.onValueChanged.Invoke(m_toggle.isOn);
	    }
	}
}