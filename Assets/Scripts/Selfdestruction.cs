using UnityEngine;
using System.Collections;

public class Selfdestruction : MonoBehaviour 
{
	void Start()
	{
		Destroy (this.gameObject, 3f);
	}
}
