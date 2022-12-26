using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt{
	
	public static void CopyTo(this Transform source, Transform to){
		to.position = source.position.Clone();
		to.localEulerAngles = source.localEulerAngles.Clone();
		to.localPosition = source.localPosition.Clone();
		to.localRotation = source.localRotation.Clone();
		to.localScale = source.localScale.Clone();
		to.rotation = source.rotation.Clone();
	}
	
	public static bool HasParent(this Transform t){
		return t.parent != null;
	}
	
	public static List<GameObject> GetAllChildren(this Transform t){
		return t.gameObject.GetAllChildren();
	}

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

	public static T GetParentComponent<T>(this Transform mb) where T : Component{
		if(!mb.HasParent()){
			return default(T);
		}
		Transform parent = mb.parent;
		T t = parent.gameObject.GetComponent<T>();
		return t;
	}

	public static void BringToFront(this Transform t) {
		t.SetAsLastSibling();
    }
}
		
