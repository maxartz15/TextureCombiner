using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class SizeOptions : Options
	{
	    public int width = 512;
	    public int height = 512;
	
	    [SerializeField]
	    private InputField inputFieldWidth = null;
	    [SerializeField]
	    private InputField inputFieldHeight = null;
	
	    private void OnEnable()
	    {
	        width = PlayerPrefs.GetInt("width", 512);
	        height = PlayerPrefs.GetInt("height", 512);
	
	        inputFieldWidth.text = width.ToString();
	        inputFieldHeight.text = height.ToString();
	
	        inputFieldWidth.onValueChanged.AddListener(UpdateWidth);
	        inputFieldHeight.onValueChanged.AddListener(UpdateHeight);
	    }
	
	    protected override void OnDisable()
	    {
	        PlayerPrefs.SetInt("width", width);
	        PlayerPrefs.SetInt("height", height);

			base.OnDisable();
	    }
	
	    private void UpdateWidth(string _width)
	    {
	        int.TryParse(_width, out width);
	    }
	
	    private void UpdateHeight(string _height)
	    {
	        int.TryParse(_height, out height);
	    }
	}
}