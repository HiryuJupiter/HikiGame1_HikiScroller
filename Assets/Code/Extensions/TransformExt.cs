using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt{
	
	/// <summary>
	/// Copies a Transform's data to another Transform
	/// </summary>
	/// <param name="source"></param>
	/// <param name="to"></param>
	public static void CopyTo(this Transform source, Transform to){
		to.position = source.position.Clone();
		to.localEulerAngles = source.localEulerAngles.Clone();
		to.localPosition = source.localPosition.Clone();
		to.localRotation = source.localRotation.Clone();
		to.localScale = source.localScale.Clone();
		to.rotation = source.rotation.Clone();
	}
	/// <summary>
	/// Checks if the Transform has a parent
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static bool HasParent(this Transform t){
		return t.parent != null;
	}
	/// <summary>
	/// Gets all children GameObjects of the Transform
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public static List<GameObject> GetAllChildren(this Transform t){
		return t.gameObject.GetAllChildren();
	}
	/// <summary>
	/// Gets the nearest parent in the hierarchy with a given Component attached.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="transform"></param>
	/// <returns></returns>
	public static T GetClosestParentComponent<T>(this Transform transform) where T : Component {
		if (!transform.HasParent()) {
			return default(T);
		}
		Transform activeObject = transform;

		T t = activeObject.GetComponentInParent<T>();

		if(t == null) {
			return activeObject.parent.GetClosestParentComponent<T>();
        } else {
			return t;
        }

	}
	/// <summary>
	/// Gets a component from the Transform's parent.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static T GetParentComponent<T>(this Transform mb) where T : Component{
		if(!mb.HasParent()){
			return default(T);
		}
		Transform parent = mb.parent;
		T t = parent.gameObject.GetComponent<T>();
		return t;
	}

	/// <summary>
	/// Brings this transform in front of others with the same sorting order.
	/// </summary>
	/// <param name="t"></param>
	public static void BringToFront(this Transform t) {
		t.SetAsLastSibling();
    }
}
		
