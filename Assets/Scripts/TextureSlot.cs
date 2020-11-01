using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextureCombiner
{
	public class TextureSlot : MonoBehaviour
	{
	    [SerializeField]
	    private Toggle[] sourceToggles = null;
	    [SerializeField]
	    private Toggle[] targetToggles = null;
	
	    public Texture2D texture
	    {
	        get;
	        private set;
	    }
	
	    [SerializeField]
	    private Button removeButton = null;
	    [SerializeField]
	    private RawImage rawImage = null;
	
	    public void Init(Texture2D tex)
	    {
	        texture = tex;
	        rawImage.texture = texture;
	
	        removeButton.onClick.AddListener(()=>
	        { 
	            Destroy(texture); 
	            Destroy(this.gameObject);
	        });
	    }
	
	    public Channels GetTargetChannels()
	    {
	        return new Channels(
	            targetToggles[0].isOn,
	            targetToggles[1].isOn,
	            targetToggles[2].isOn,
	            targetToggles[3].isOn
	        );
	    }
	
	    public Vector4 GetSourceChannels()
	    {
	        return new Vector4(
	            sourceToggles[0].isOn ? 1.0f : 0.0f,
	            sourceToggles[1].isOn ? 1.0f : 0.0f,
	            sourceToggles[2].isOn ? 1.0f : 0.0f,
	            sourceToggles[3].isOn ? 1.0f : 0.0f
	        );
	    }
	
	    public struct Channels
	    {
	        public Channels(bool _r, bool _g, bool _b, bool _a)
	        {
	            r = _r;
	            g = _g;
	            b = _b;
	            a = _a;
	        }
	
	        public bool r;
	        public bool g;
	        public bool b;
	        public bool a;
	    }
	}
}