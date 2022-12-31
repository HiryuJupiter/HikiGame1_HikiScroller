using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExt{
	/// <summary>
	/// Sets the object's parent.
	/// </summary>
	/// <param name="go"></param>
	/// <param name="parent"></param>
    public static void SetParent(this GameObject go, GameObject parent){
		go.transform.SetParent(parent.transform);
	}
	/// <summary>
	/// Sets the object's parent.
	/// </summary>
	/// <param name="go"></param>
	/// <param name="parent"></param>
    public static void SetParent(this GameObject go, MonoBehaviour parent){
        go.transform.SetParent(parent.gameObject.transform);
    }
	/// <summary>
	/// Get all children and their children, and their children, etc.
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static List<GameObject> GetAllChildrenRecursive(this GameObject go) {
		List<GameObject> objs = new List<GameObject>();
		List<Transform> children = new List<Transform>();

		for(int i = 0; i < go.transform.childCount; i++) {
			Transform child = go.transform.GetChild(i);
			children.Add(child);
			objs.Add(child.gameObject);
        }

		for(int i = 0; i < children.Count; i++) {
			Transform child = children[i];
			List<GameObject> childObjects = child.gameObject.GetAllChildrenRecursive();
			objs.AddRange(childObjects);
        }
		return objs;
    }
	/// <summary>
	/// Get all children with a given component and their children with that component etc.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <returns></returns>
	public static List<T> GetAllChildrenComponentsRecursive<T>(this GameObject go){
		List<T> childrenComponentList = new List<T>();
		List<Transform> children = new List<Transform>();
		
		for(int i = 0; i < go.transform.childCount; i++){
			Transform child = go.transform.GetChild(i);
			children.Add(child);
			
			if(child.gameObject.TryGetComponent<T>(out T t)) {
				//Debug.Log("Adding " + child.gameObject.name);
				childrenComponentList.Add(t);
            }
		}
		
		for(int i = 0; i < children.Count; i++){
			Transform child = children[i];
			List<T> childComps = child.gameObject.GetAllChildrenComponentsRecursive<T>();
			foreach(T tcomp in childComps){
				childrenComponentList.Add(tcomp);
			}
		}
		
		return childrenComponentList;
	}
	/// <summary>
	/// Create an instance of the prefab and make this GameObject it's parent.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public static T AddInstanceAsChild<T>(this GameObject go, T prefab) where T : MonoBehaviour {
		T instance = GameObject.Instantiate(prefab);
		instance.SetParent(go);
		return instance;
    }

	/// <summary>
	/// Create an instance of the prefab and make this GameObject it's parent.
	/// </summary>
	/// <param name="go"></param>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public static GameObject AddInstanceAsChild(this GameObject go, GameObject prefab) {
		GameObject instance = GameObject.Instantiate(prefab);
		instance.SetParent(go);
		return instance;
    }
	/// <summary>
	/// Activate this GameObject.
	/// </summary>
	/// <param name="go"></param>
	public static void Activate(this GameObject go) {
		go.SetActive(true);
    }
	/// <summary>
	/// Deactivate this GameObject.
	/// </summary>
	/// <param name="go"></param>
	public static void Deactivate(this GameObject go) {
		go.SetActive(false);
    }
	/// <summary>
	/// Check if this GameObject is Active
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool IsActive(this GameObject go) {
		return go.activeSelf && go.activeInHierarchy;
	}
	/// <summary>
	/// Toggle whether this GameObject is Active.
	/// </summary>
	/// <param name="go"></param>
	public static void ToggleActive(this GameObject go) {
		go.SetActive(!go.IsActive());
    }
	/// <summary>
	/// Destroy all children of this GameObject.
	/// </summary>
	/// <param name="go"></param>
	/// <param name="immediate">Immediate is recommended in the Editor.</param>
	public static void DestroyAllChildren(this GameObject go, bool immediate = true){
		for(int i = go.transform.childCount-1; i >= 0; i--){
			Transform child = go.transform.GetChild(i);
			if(immediate){
				GameObject.DestroyImmediate(child.gameObject);
			}else{
				GameObject.Destroy(child.gameObject);
			}
		}
	}
	/// <summary>
	/// Get all children of this GameObject.
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static List<GameObject> GetAllChildren(this GameObject go){
		List<GameObject> list = new List<GameObject>();
		for(int i = 0; i < go.transform.childCount; i++){
			list.Add(go.transform.GetChild(i).gameObject);
		}
		return list;
	}
	/// <summary>
	/// Get all children with the specified component.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <returns></returns>
    public static List<T> GetAllChildrenComponents<T>(this GameObject go){
		List<T> childrenComponentList = new List<T>();
        for(int i = 0; i < go.transform.childCount; i++){
            Transform child = go.transform.GetChild(i);
            T t = child.gameObject.GetComponent<T>();
            if(t != null){
                childrenComponentList.Add(t);
            }
        }

        return childrenComponentList;
	}
	/// <summary>
	/// Get the first child of this GameObject.
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static GameObject GetFirstChild(this GameObject go){
		if(go.GetChildCount() == 0){
			return null;
		}
		return go.transform.GetChild(0).gameObject;
	}
	/// <summary>
	/// Get the last child of this GameObject.
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static GameObject GetLastChild(this GameObject go){
		if(go.GetChildCount() == 0){
			return null;
		}
		return go.transform.GetChild(go.GetChildCount()-1).gameObject;
	}
	/// <summary>
	/// Get the amount of children this GameObject has.
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static int GetChildCount(this GameObject go){
		return go.transform.childCount;
	}
}
