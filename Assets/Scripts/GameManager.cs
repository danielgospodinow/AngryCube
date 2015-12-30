using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	private GameObject[] laserShooters;
	public static int score = 0;
	public static bool isAlive;
	private float shootDelay;
	private Vector3 touchInput;
	private GameObject playerCube;
	public GameObject playerCubeI;
	private Rigidbody2D playerCubeRigid;
	private float speed;
	private GameObject canvas;
	private Vector3 startCutePos;
	private Quaternion startCuteRot;

	void Start()
	{
		playerCube = GameObject.FindGameObjectWithTag ("Player");
		canvas = GameObject.Find ("Canvas");
		playerCubeRigid = playerCube.GetComponent<Rigidbody2D> ();
		laserShooters = GameObject.FindGameObjectsWithTag ("Laser");
		shootDelay = 0f;
		speed = 13000f;
		startCutePos = playerCube.transform.position;
		startCuteRot = playerCube.transform.rotation;
		Time.timeScale = 0;
	}

	void Update()
	{
		if (GameManager.isAlive == false)
			return;

		LaserHandler ();

//		if(Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//		{
//			touchInput = Camera.main.ScreenToWorldPoint (Input.touches[0].position);
//			if(touchInput.x > 0)
//			{
//				playerCubeRigid.AddForce(new Vector2(-1,-2) * speed * Time.deltaTime);
//			}
//			else if(touchInput.x <= 0)
//			{
//				playerCubeRigid.AddForce(new Vector2(1,-2) * speed * Time.deltaTime);
//			}
//		}

		if(Input.GetMouseButtonDown(0))
		{
			touchInput = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if(touchInput.x > 0)
			{
				playerCubeRigid.AddForce(new Vector2(-1,-2) * speed * Time.deltaTime);
			}
			else if(touchInput.x <= 0)
			{
				playerCubeRigid.AddForce(new Vector2(1,-2) * speed * Time.deltaTime);
			}
		}

	}

	private void LaserHandler()
	{
		for (int i=0; i < laserShooters.Length; i++) 
		{
			if(laserShooters[i].GetComponent<LaserShooterLogic>().IsFiring)
			{
				return;
			}
		}
		
		if(shootDelay < 2f)
		{
			shootDelay += 1 * Time.deltaTime;
			return;
		}
		
		int randomNumber = Random.Range(0, laserShooters.Length);
		laserShooters[randomNumber].GetComponent<LaserShooterLogic>().Fire();
		shootDelay = 0f;
	}

	public void OnPlayClick()
	{
		Time.timeScale = 1f;
		canvas.transform.FindChild ("Button").gameObject.SetActive (false);
	}

	public void OnReplayClick()
	{
		GameObject newCube =  Instantiate (playerCubeI, startCutePos, startCuteRot) as GameObject;
		this.playerCube = newCube;
		this.playerCubeRigid = playerCube.GetComponent<Rigidbody2D> ();
		Time.timeScale = 1f;
		GameManager.score = 0;
		canvas.transform.FindChild ("ButtonRetry").gameObject.SetActive (false);
	}

	public void ReloadGame()
	{
		StartCoroutine (ReloadGameEnumerator ());
	}

	private IEnumerator ReloadGameEnumerator()
	{
		yield return new WaitForSeconds(2f);

		canvas.transform.FindChild ("ButtonRetry").gameObject.SetActive (true);
		canvas.transform.FindChild ("ButtonRetry").gameObject.transform.FindChild("ScoreText").gameObject.GetComponent<Text>().text = "Score: " + GameManager.score.ToString();

		Time.timeScale = 0f;
	}
}
