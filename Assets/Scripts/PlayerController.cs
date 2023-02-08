using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI livesText;
	public GameObject winTextObject;
	public GameObject loseTextObject;
	public GameObject playAgainButton;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;
	private int lives;

	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
		lives = 3;

		SetCountText();
		SetLivesText();

		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);
		loseTextObject.SetActive(false);
		playAgainButton.SetActive(false);
	}

	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}

		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			lives = lives - 1;
			SetLivesText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniGame") && count >= 12)
		{
			SceneManager.LoadScene("Level2");
		}
		else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && count >= 15)
        {
			winTextObject.SetActive(true);
        }
	}

	void SetLivesText()
    {
		livesText.text = "Lives: " + lives.ToString();

		if (lives <= 0)
        {
			loseTextObject.SetActive(true);
			playAgainButton.SetActive(true);
			//Destroy(gameObject);
        }
	}
}
