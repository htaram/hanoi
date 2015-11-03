using UnityEngine;
using System.Collections.Generic;

public class PoleScript : MonoBehaviour {

	public bool isEndPole = false;
	private Stack<GameObject> goDiskStack = new Stack<GameObject>();
	private GameManager gameManager;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	

	private void DisableDisk(GameObject goDisk)
	{
		goDisk.GetComponent<DiskMovement>().bLocked = true;
	}

	
	private void EnableDisk(GameObject goDisk)
	{
		goDisk.GetComponent<DiskMovement>().bLocked = false;
	}
	

	public void AddToPole(GameObject goDisk)
	{
		// Validate input
		if (goDisk == null)
			return;
		
		// Disable the rest of the disks in the stack
		// Only the top disk can be moved 
		if (goDiskStack.Count > 0) {
			GameObject top = goDiskStack.Peek();
			
			// Cannot add this disk here because the top of stack is smaller than the new disk
			if (top.GetComponent<BoxCollider2D>().size.x <= goDisk.GetComponent<BoxCollider2D>().size.x)
			{
				return;
			}

			DisableDisk(top);
		}
		// Until the disk is successfully moved to another pole, it will snap to this one
		goDisk.GetComponent<DiskMovement>().snapPosition = transform.position;

		// Add the disk to the stack for this pole
		goDiskStack.Push (goDisk);

		// Add to moves
		GameManager.moves++;

		// If you finished the game by moving 
		if (isEndPole && goDiskStack.Count == gameManager.goDiskPrefabs.Length)
		{
			gameManager.GameOver();
		}
	}


	public void RemoveFromPole(GameObject goDisk)
	{
		// Remove the top disk from the stack
		if (goDiskStack.Count <= 0)
			return;

		if (goDiskStack.Peek () != goDisk)
			return;

		// Pop top of the stack
		goDiskStack.Pop ();

		// Enable movement for the new stack top
		if (goDiskStack.Count > 0) {
			GameObject top = goDiskStack.Peek();
			EnableDisk(top);
		}
	}


	void OnTriggerEnter2D(Collider2D colliderInput)
	{
		if (colliderInput.gameObject.tag == "Disks") {
			AddToPole(colliderInput.gameObject);
		}
	}

	
	void OnTriggerExit2D(Collider2D colliderInput)
	{				
		if (colliderInput.gameObject.tag == "Disks") {
			RemoveFromPole(colliderInput.gameObject);
		}
	}
}
