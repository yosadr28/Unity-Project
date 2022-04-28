using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControk : MonoBehaviour {

	public Image H1;
	public Image H2;
	public Image H3;

	public AudioSource rocket;
	public Text dialog;
	public bool paused = true;
	public Image control;
	private int timerRestart = 200;
	public Image failed;
	public GameObject spawnP;
	public GameObject player;
	private int health = 3;
	public GameObject targetCamera;
	public Meluncur luncur;
	public GameObject Roket;
	public GameObject playerturun;
	public GameObject parkir;
	private int timerReload = 100;
	private bool tembak;
	public GameObject fireRocket;
	public GameObject posPesawat;
	public Rigidbody2D Misile;
	public GameObject posMisile;
	public GameObject posMisile2;
	public Text T_Plane;
	public GameObject pesawat;
	private bool terbang = false;
	public Camera mainCamera;
	public int speed = 4;
	public int jumpHeight = 8;
	private int moveDir = 0;
	private int moveDirH = 0;
	private bool jump = false;
	private bool isGrounded = true;
	public Rigidbody2D r2d;
	private SpriteRenderer sec;
	Vector3 cameraPos;
	BoxCollider2D mainCollider;
	Transform t;

	// Use this for initialization
	void Start () {
		rocket.enabled = false;
		failed.enabled = false;
		transform.position = spawnP.transform.position;
		r2d = GetComponent<Rigidbody2D>();
		sec = GetComponent<SpriteRenderer>();
		mainCollider = GetComponent<BoxCollider2D>();
		r2d.freezeRotation = true;
		r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		GameObject g = GameObject.FindGameObjectWithTag("Roket");
		r2d.gravityScale = 1;
		luncur = g.GetComponent<Meluncur>();
	}
	
	// Update is called once per frame
	void Update () {
		if(health >= 3)
        {
			H3.enabled = true;
			H2.enabled = true;
			H1.enabled = true;
		} else
		if (health == 2)
		{
			H3.enabled = false;
			H2.enabled = true;
			H1.enabled = true;
		}
		else
		if (health == 1)
		{
			H3.enabled = false;
			H2.enabled = false;
			H1.enabled = true;
		}
		else
		{
			H3.enabled = false;
			H2.enabled = false;
			H1.enabled = false;
		}

		if (!paused)
		{
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				moveDir = 1;
				sec.flipX = false;
			}
			else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				moveDir = -1;
				sec.flipX = true;
			}
			else
			{
				moveDir = 0;
			}

			if (moveDir != 0)
			{
				control.enabled = false;
			}

			if (terbang)
			{
				if (timerReload < 1)
				{
					timerReload = 0;
					tembak = true;
				}
				else { tembak = false; }
				if (timerReload > 0)
				{
					tembak = false;
					timerReload--;
				}
				fireRocket.SetActive(true);
				if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				{
					moveDirH = 1;
				}
				else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				{
					moveDirH = -1;
				}
				else
				{
					moveDirH = 0;
					moveDir = 0;
				}

				if (Input.GetKeyDown(KeyCode.E) && tembak)
				{
					Vector3 Misilespawned = posMisile.transform.position;
					Instantiate(Misile, Misilespawned, transform.rotation);
					timerReload = 100;
				}

			}
			else
			{
				fireRocket.SetActive(false);
			}



			if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
			{
				jump = true;
			}

			Jump();

			if (terbang)
			{
				rocket.enabled = true;
				transform.position = posPesawat.transform.position;
				isGrounded = false;
				jump = false;
				r2d.gravityScale = 0f;
				pesawat.transform.parent = this.transform;
				if (r2d.position.y < -2)
				{
					transform.position = new Vector2(transform.position.x, -2);
				}
				if (r2d.position.y > 7)
				{
					transform.position = new Vector2(transform.position.x, 7);
				}
			}

			if (r2d.transform.position.y < -18 || health < 1)
			{
				failed.enabled = true;
				dialog.text = "We can restart our journey because it's a game";
				timerRestart--;
				if (timerRestart < 1)
				{
					timerRestart = 0;
				}
				if (timerRestart == 0)
				{
					SceneManager.LoadScene(0);
				}
			}
		}

        if (r2d.transform.position.x < 5)
        {
			dialog.text = "Let's Begin Our Journey Memories~";
        } else if (r2d.transform.position.x < 20)
		{
			dialog.text = "Sometimes, it wasn't easy to doing a normal life";
		}
		else if (r2d.transform.position.x < 40)
		{
			dialog.text = "Specially on this corona time";
		}
		else if (r2d.transform.position.x < 60)
		{
			dialog.text = "Specially on this corona time";
		}
		else if (r2d.transform.position.x < 80)
		{
			dialog.text = "Many things that had happen";
		}
		else if (r2d.transform.position.x < 100)
		{
			dialog.text = "But you keep trying to finish it";
		}
		else if (r2d.transform.position.x < 120)
		{
			dialog.text = "That's the spirit...";
		}
		else if (r2d.transform.position.x < 145)
		{
			dialog.text = "and I will give you a present....";
		}
		else if (r2d.transform.position.x < 170)
		{
			dialog.text = "if you finish this game, there will be present for you";
		} else
        {
			dialog.text = " ";
		}

	}

	public void Jump()
    {
		if (jump == true && isGrounded == true && !terbang)
		{
			r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
			isGrounded = false;
			jump = false;
		} 
    }

	void FixedUpdate()
	{
		Bounds colliderBounds = mainCollider.bounds;
		float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
		Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
		isGrounded = false;
		if (colliders.Length > 0)
		{
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i] != mainCollider)
				{
					isGrounded = true;
					break;
				}
			}
		}

		if (terbang)
		{
			r2d.transform.position = transform.position + new Vector3(5 * Time.deltaTime, moveDirH * 5 * Time.deltaTime,0);
		}

		if (!terbang) { 
		r2d.velocity = new Vector2((moveDir) * speed, r2d.velocity.y);
		}

		Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
		Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Pesawat" && isGrounded)
		{
			T_Plane.text = "Press E to Use The Gun In Ship....";
			//if (Input.GetKeyDown(KeyCode.E)) { 
			terbang = true;
			Debug.Log("Naik Pesawat");
			//}
		}
		else
		{
			T_Plane.text = "";
		}

		if (col.gameObject.tag == "Park" && terbang)
		{
			r2d.gravityScale = 1;
			T_Plane.text = "Plane is Parked";
			terbang = false;
			pesawat.transform.SetParent(null);
			pesawat.transform.position = parkir.transform.position;
			r2d.transform.position = playerturun.transform.position;
			Debug.Log("Turun Dari Pesawat");
			rocket.enabled = false;
		}
	}

	void OnColliderEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Park" && terbang)
		{
			r2d.gravityScale = 1;
			T_Plane.text = "Plane is Parked";
			terbang = false;
			pesawat.transform.SetParent(null);
			pesawat.transform.position = parkir.transform.position*Time.deltaTime;
			r2d.transform.position = playerturun.transform.position * Time.deltaTime;
			Debug.Log("Turun Dari Pesawat");
		}

		

	}

	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "Plane")
		{
			T_Plane.text = "Launching The Rocket";
			dialog.text = "This is my present for you......";
			moveDir = 0;
			moveDirH = 0;
			mainCamera.transform.SetParent(null);
			mainCamera.transform.position = targetCamera.transform.position;
			mainCamera.orthographicSize = 10;
			luncur.launch = true;
			paused = true;

		}

		if (col.gameObject.tag == "Enemy")
		{
			health--;				
			print("sisa nyawa" + health);
			if (health < 1)
			{
				mainCamera.transform.SetParent(null);

				//Destroy(player);
				
			}

		}

	}

	void OnTriggerExit2D(Collider2D col)
    {
		T_Plane.text = "";
	}

}
