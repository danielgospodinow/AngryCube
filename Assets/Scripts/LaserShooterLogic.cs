using UnityEngine;
using System.Collections;

public class LaserShooterLogic : MonoBehaviour 
{
	private bool isFiring;
	public GameObject laserRay;

	public bool IsFiring
	{
		get{ return this.isFiring;}
		set{ this.isFiring = value;}
	}

	void Start()	
	{
		isFiring = false;
	}

	void Update()
	{

	}

	public void Fire()
	{
		GameObject laserRayI = Instantiate(laserRay, this.gameObject.transform.FindChild("LaserDuo").gameObject.transform.position + new Vector3(0,0,1f), Quaternion.Euler(0,0,0)) as GameObject;
		isFiring = true;
		laserRayI.GetComponent<LaserRayLogic> ().Parent = this.gameObject;
	}
}
