using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class CombineOptions : Options
	{
	    [SerializeField]
	    private Toggle autoCombineToggle = null;
	
	    private void OnEnable()
	    {
	        autoCombineToggle.isOn = PlayerPrefs.GetInt("autoCombine", 1) != 0;
	    }
	
	    protected override void OnDisable()
	    {
			PlayerPrefs.SetInt("autoCombine", autoCombineToggle.isOn ? 1 : 0);

			base.OnDisable();
		}
	
	    public bool GetAutoCombine()
	    {
	        return autoCombineToggle.isOn;
	    }
	}
}