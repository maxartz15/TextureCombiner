using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextureCombiner
{
	public abstract class Options : MonoBehaviour
	{
	    protected virtual void OnDisable()
	    {
	        PlayerPrefs.Save();
	    }
	}
}
