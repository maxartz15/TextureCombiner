using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class SaveOptions : Options
	{
	    [SerializeField]
	    private InputField inputFieldName = null;
	
	    private void OnEnable()
	    {
	        inputFieldName.text = PlayerPrefs.GetString("outputName", "_RMAH");
	    }
	
	    protected override void OnDisable()
	    {
	        PlayerPrefs.SetString("outputName", inputFieldName.text);

			base.OnDisable();
	    }
	
	    public string GetOutputName()
	    {
	        return inputFieldName.text + ".png";
	    }
	}
}