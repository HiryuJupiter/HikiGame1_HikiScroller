using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collider2DExt{

	/**
		Clones a Collider2D. 
		Currently only supports BoxCollider2D, CircleCollider2D and PolygonCollider2D
	*/
	static int h;
	/// <summary>
	/// Creates a copy of a Collider2D. 
	/// Currently only supports BoxCollider2D, CircleCollider2D and PolygonCollider2D
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	public static Collider2D Clone(this Collider2D col){
		Collider2D clone = null;
		if(col as PolygonCollider2D != null){
			clone = ClonePolygonCollider(col as PolygonCollider2D);
		}else if(col as BoxCollider2D != null){
			clone = CloneBoxCollider(col as BoxCollider2D);
		}else if(col as CircleCollider2D != null){
			clone = CloneCircleCollider(col as CircleCollider2D);
		}
		if(clone != null){
			//Copy Collider2D specific data
			//TODO: re-enable density with a check for a dynamic rigidbody
			//clone.density = col.density;
			clone.isTrigger = col.isTrigger;
			clone.offset = col.offset.Clone();
			clone.sharedMaterial = col.sharedMaterial;
			clone.usedByEffector = col.usedByEffector;
			
			//Inherited members
			clone.enabled = col.enabled;
			col.transform.CopyTo(clone.transform);
			
		}
		return clone;
	}
	/// <summary>
	/// Creates a copy of the collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static PolygonCollider2D ClonePolygonCollider(PolygonCollider2D col){
		GameObject o = new GameObject();
		o.AddComponent<PolygonCollider2D>();
		PolygonCollider2D pc = o.GetComponent<PolygonCollider2D>();
		pc.autoTiling = col.autoTiling;
		pc.pathCount = col.pathCount;
		
		pc.points = ClonePoints(col.points);
		
		return pc;
	}
	/// <summary>
	/// Creates a copy of an array of points.
	/// </summary>
	/// <param name="points"></param>
	/// <returns></returns>
	private static Vector2[] ClonePoints(Vector2[] points){
		Vector2[] clone = new Vector2[points.Length];
		for(int i = 0; i < points.Length; i++){
			clone[i] = points[i].Clone();
		}
		
		return clone;
		
	}
	/// <summary>
	/// Creates a copy of the collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static BoxCollider2D CloneBoxCollider(BoxCollider2D col){
		GameObject o = new GameObject();
		o.AddComponent<BoxCollider2D>();
		BoxCollider2D bc = o.GetComponent<BoxCollider2D>();
		bc.autoTiling = col.autoTiling;
		bc.edgeRadius = col.edgeRadius;
		bc.size = col.size.Clone();
		
		return bc;
	}

	/// <summary>
	/// Creates a copy of the collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static CircleCollider2D CloneCircleCollider(CircleCollider2D col){
		GameObject o = new GameObject();
		o.AddComponent<CircleCollider2D>();
		CircleCollider2D c = o.GetComponent<CircleCollider2D>();
		c.radius = col.radius;
		
		return c;
		
	}
	/// <summary>
	/// Gets the width of a collider. 
	/// Currently supports PolygonCollider2D, BoxCollider2D, CircleCollider2D
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	public static float Width(this Collider2D col){
		if(col as PolygonCollider2D != null){
			return Width(col as PolygonCollider2D);
		}else if(col as BoxCollider2D != null){
			return Width(col as BoxCollider2D);
		}else if(col as CircleCollider2D != null){
			return Width(col as CircleCollider2D);
		}
		Debug.Log("Width not supported for this collider: " + col);
		return 0;
	}
	/// <summary>
	/// Gets the width of the Collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static float Width(BoxCollider2D col){
		return col.size.x;
	}
	/// <summary>
	/// Gets the width of the collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static float Width(CircleCollider2D col){
		return col.radius * 2;
	}
	/// <summary>
	/// Gets the width of the collider.
	/// </summary>
	/// <param name="col"></param>
	/// <returns></returns>
	private static float Width(PolygonCollider2D col){
		Vector2[] points = col.points;
		float smallest = Mathf.Infinity;
		float largest = -Mathf.Infinity;
		foreach(Vector2 point in points){
			if(point.x > largest){
				largest = point.x;
			}
			if(point.x < smallest){
				smallest = point.x;
			}
		}
		
		return largest - smallest;
	}

	/// <summary>
	/// Gets a list of colliders that are colliding with this one.
	/// </summary>
	/// <param name="collider"></param>
	/// <returns></returns>
	public static List<Collider2D> GetHits(this Collider2D collider) {
		return collider.GetHits(100);
	}
	/// <summary>
	/// Gets a list of colliders that are colliding with this one.
	/// </summary>
	/// <param name="collider"></param>
	/// <param name="maxHits"></param>
	/// <returns></returns>
	public static List<Collider2D> GetHits(this Collider2D collider, int maxHits) {
		Collider2D[] res = new Collider2D[maxHits];

		ContactFilter2D filter = new ContactFilter2D();
		filter.useDepth = true;
		filter.minDepth = -Mathf.Infinity;
		filter.maxDepth = Mathf.Infinity;

		int resultCount = Physics2D.OverlapCollider(collider, filter, res);

		List<Collider2D> targetsHit = new List<Collider2D>();

		for(int i = 0; i < resultCount; i++) {
			targetsHit.Add(res[i]);
		}

		return targetsHit;
	}
	/// <summary>
	/// Get a list of a specified component from colliders that have that component.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="colliders"></param>
	/// <returns></returns>
	public static List<T> WithComponent<T>(this List<Collider2D> colliders) where T : Behaviour{
		List<T> results = new List<T>();
		foreach(Collider2D col in colliders) {
			T component = col.gameObject.GetComponent<T>();
            if (component) {
				results.Add(component);
            }
        }
		return results;
    }
}

















