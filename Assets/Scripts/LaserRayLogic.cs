using UnityEngine;
using System.Collections;

public class LaserRayLogic : MonoBehaviour 
{
	//100-255
	private float delayTime = 2f;
	private SpriteRenderer spriteRenderer;
	private Color32 startColor = new Color32(255, 255, 255, 80);
	private Color32 endColor = new Color32(255, 255, 255, 255);
	private GameObject parent;

	public GameObject Parent
	{
		set{this.parent = value;}
	}

	void Start()
	{
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.color = startColor;
		StartCoroutine (LaserHandler ());
	}

	private IEnumerator LaserHandler()
	{
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds(delayTime);
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		spriteRenderer.color = endColor;
		yield return new WaitForSeconds(delayTime);
		parent.GetComponent<LaserShooterLogic> ().IsFiring = false;
		GameManager.score += 1;
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerLogic> ().KillCube ();
			GameManager.score -= 1;
		}
	}
}
