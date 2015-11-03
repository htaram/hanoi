using UnityEngine;
using System.Collections;

public class DiskMovement : MonoBehaviour 
{	
	public Vector2 snapPosition;
	Transform transformTarget = null;
	public bool bLocked = false;


	void Update () 
	{
		// Check if mouse input has been received
		MouseInput ();

		// If the user is clicking and dragging this object. Follow the mouse position
		if (transformTarget) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transformTarget.position = new Vector2(pos.x, pos.y);
		}
		else{
			// If the user has stopped dragging this object, snap to a pole(old or new). 
			transform.position = new Vector2(snapPosition.x, transform.position.y);
		}
	
	}
	


	void MouseInput()
	{
		// If the disk is locked then do nothing
		if (bLocked)
			return;

		// If mouse button has been clicked
		if(Input.GetMouseButton(0))
		{
			// Check if the mouse click is on this disk
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);		

			// If this disk is being clicked on, allow drag motion
			if(hit.collider != null && hit.collider.gameObject == this.gameObject)
			{	
				transformTarget = transform;
			}

		}	

		// If mouse button has been released, allow snap motion.
		else if (!Input.GetMouseButton(0) && transformTarget != null)
		{
			transform.position = snapPosition;
			transformTarget = null;

		}

	}
}
	

