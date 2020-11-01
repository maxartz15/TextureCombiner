using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class AddOptions : Options
	{
	    [SerializeField]
	    private Slider inputFieldR = null;
	    [SerializeField]
	    private Slider inputFieldG = null;
	    [SerializeField]
	    private Slider inputFieldB = null;
	    [SerializeField]
	    private Slider inputFieldA = null;
	
	    private void OnEnable()
	    {
	        inputFieldR.value = PlayerPrefs.GetFloat("addR", 0);
	        inputFieldG.value = PlayerPrefs.GetFloat("addG", 0);
	        inputFieldB.value = PlayerPrefs.GetFloat("addB", 0);
	        inputFieldA.value = PlayerPrefs.GetFloat("addA", 0);
	    }
	
	    protected override void OnDisable()
	    {
	        PlayerPrefs.SetFloat("addR", inputFieldR.value);
	        PlayerPrefs.SetFloat("addG", inputFieldG.value);
	        PlayerPrefs.SetFloat("addB", inputFieldB.value);
	        PlayerPrefs.SetFloat("addA", inputFieldA.value);

			base.OnDisable();
	    }
	
	    public Vector4 GetAddValues()
	    {
	        return new Vector4(
	            inputFieldR.value,
	            inputFieldG.value,
	            inputFieldB.value,
	            inputFieldA.value
	            );
	    }
	}
}