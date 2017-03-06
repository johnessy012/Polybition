using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Touch;

public class rCade_GlobalUtilities {
	
	#region Public Variables

	#endregion

	#region Private Variables

	#endregion

	#region Local Variables

	#endregion

	#region Built In Functions
	void Start () {
		
	}
	
	void Update () {
		
	}
	#endregion

	#region Main Functions

	#endregion

	#region Utility Functions
    public static RaycastHit GatherRaycastData(Camera rayCamera, Vector2 touchPosition) {
        Ray ray = Camera.current.ScreenPointToRay(new Vector3 (touchPosition.x, touchPosition.y, 0));
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity)) {
            return rayHit;
        }
        return rayHit;
    }

    public static NavMeshHit GatherNavMeshData(Camera rayCamera, Vector2 touchPosition, float sampleDistance) {
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y, 0));
        RaycastHit rayHit = new RaycastHit();
        NavMeshHit navHit = new NavMeshHit();
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity)) {
            if (NavMesh.SamplePosition(rayHit.point, out navHit, sampleDistance, NavMesh.AllAreas)) {
                return navHit;
            }
            return navHit;
        }
        return navHit;
    }
    #endregion
}
