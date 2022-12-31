using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MonoBehaviourExt{

	public delegate void ActionDelegate();
	public delegate bool ConditionDelegate();
	public delegate Coroutine EnumeratedDelegate();
	public delegate IEnumerator EnumeratorDelegate();

	/// <summary>
	/// Performs an action after the provided delay
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="delay">Delay before performing action</param>
	/// <param name="action">The action to be executed.</param>
	public static void DoAfter(this MonoBehaviour mb, float delay, ActionDelegate action) {
		mb.StartCoroutine(DelayedActionCoroutine(delay, action));
    }

	public static void DoAfterFrames(this MonoBehaviour mb, int frames, ActionDelegate action) {
		mb.StartCoroutine(DelayedFrameActionCoroutine(frames, action));
    }

	/// <summary>
	/// Performs an action in intervals. Stops if stopCondition is true
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="interval"></param>
	/// <param name="action"></param>
	/// <param name="stopCondition"></param>
	public static void DoEvery(this MonoBehaviour mb, float interval, ActionDelegate action, ConditionDelegate stopCondition = null) {
		mb.StartCoroutine(IntervalActionCoroutine(interval, action, stopCondition));
    }
	/// <summary>
	/// Perform a set of actions in order.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="actions"></param>
	public static void DoInOrder(this MonoBehaviour mb, params EnumeratedDelegate[] actions) {
		mb.StartCoroutine(OrderedActionCoroutine(mb, actions));
    }
	/// <summary>
	/// Perform a set of actions as a Coroutine in order.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="actions"></param>
	public static void DoAsCoroutine(this MonoBehaviour mb, EnumeratorDelegate[] actions) {
		EnumeratedDelegate[] actionOrders = new EnumeratedDelegate[actions.Length];
		for(int i = 0; i < actions.Length; i++) {
			actionOrders[i] = () => {
				return mb.StartCoroutine(actions[i]());
			};
        }

		mb.DoInOrder(actionOrders);
    }
	/// <summary>
	/// Perform an action as a Coroutine.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="action"></param>
	public static void DoAsCoroutine(this MonoBehaviour mb, EnumeratorDelegate action) {
		mb.StartCoroutine(action());
	}

	/// <summary>
	/// Perform a set of actions in order.
	/// </summary>
	/// <param name="routineStarter"></param>
	/// <param name="actions"></param>
	/// <returns></returns>
	static IEnumerator OrderedActionCoroutine(MonoBehaviour routineStarter, params EnumeratedDelegate[] actions) {
		for(int i = 0; i < actions.Length; i++) {
			Debug.Log("Action " + i);
			yield return actions[i]();
        }
    }
	/// <summary>
	/// Perform an action after a delay.
	/// </summary>
	/// <param name="delay"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	static IEnumerator DelayedActionCoroutine(float delay, ActionDelegate action) {
		yield return new WaitForSeconds(delay);
		action();
        
    }
	/// <summary>
	/// Perform an action after a given amount of frames.
	/// </summary>
	/// <param name="frames"></param>
	/// <param name="action"></param>
	/// <returns></returns>
	static IEnumerator DelayedFrameActionCoroutine(int frames, ActionDelegate action) {
		for (int i = 0; i < frames; i++) {
			yield return new WaitForEndOfFrame();
		}
		action();
    }

	/// <summary>
	/// Perform an action at set intervals until the stop condition is met.
	/// </summary>
	/// <param name="interval"></param>
	/// <param name="action"></param>
	/// <param name="stopCondition"></param>
	/// <returns></returns>
	static IEnumerator IntervalActionCoroutine(float interval, ActionDelegate action, ConditionDelegate stopCondition = null) {
        while (true) {
			action();
			yield return new WaitForSeconds(interval);
			if(stopCondition != null && stopCondition()) {
				break;
            }
        }
    }
	/// <summary>
	/// Destroy all children of this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="immediate"></param>
	public static void DestroyAllChildren(this MonoBehaviour mb, bool immediate = true){
		mb.gameObject.DestroyAllChildren(immediate);
	}
	/// <summary>
	/// Check if this behaviour's gameObject is active
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static bool IsActive(this MonoBehaviour mb){
		return mb.gameObject.activeSelf && mb.gameObject.activeInHierarchy;
	}
	/// <summary>
	/// Activate this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	public static void Activate(this MonoBehaviour mb){
		mb.gameObject.SetActive(true);
	}
	/// <summary>
	/// Deactivate this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	public static void Deactivate(this MonoBehaviour mb){
		mb.gameObject.SetActive(false);
	}
	/// <summary>
	/// Get the amount of children this behaviour's gameObject has.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static int GetChildCount(this MonoBehaviour mb){
		return mb.gameObject.transform.childCount;
	}
	/// <summary>
	/// Get the first child of this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static GameObject GetFirstChild(this MonoBehaviour mb){
		if(mb.GetChildCount() == 0){
			return null;
		}
		return mb.gameObject.transform.GetChild(0).gameObject;
	}
	/// <summary>
	/// Get the last child of this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static GameObject GetLastChild(this MonoBehaviour mb){
		if(mb.GetChildCount() == 0){
			return null;
		}
		return mb.gameObject.transform.GetChild(mb.GetChildCount()-1).gameObject;
	}
	/// <summary>
	/// Get all of the specified components from children and their children etc. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static List<T> GetAllChildrenComponentsRecursive<T>(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildrenComponentsRecursive<T>();
	}
	/// <summary>
	/// Get all of the specified components from immediate children.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static List<T> GetAllChildrenComponents<T>(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildrenComponents<T>();
	}
	/// <summary>
	/// Check if this behaviour's object has a parent.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static bool HasParent(this MonoBehaviour mb){
		return mb.gameObject.transform.parent != null;
	}
	/// <summary>
	/// Get a component from this behaviour's gameObject's parent.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static T GetParentComponent<T>(this MonoBehaviour mb) where T : Component{
		if(!mb.HasParent()){
			return default(T);
		}
		Transform parent = mb.GetParent();
		T t = parent.gameObject.GetComponent<T>();
		return t;
	}
	/// <summary>
	/// Get the parent Transform of this behaviour's gameObject.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static Transform GetParent(this MonoBehaviour mb){
		return mb.gameObject.transform.parent;
	}
	
	/// <summary>
	/// Set the parent of this object. (For UI use only)
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="parent"></param>
	public static void SetParent(this MonoBehaviour mb, RectTransform parent) {
		mb.gameObject.transform.SetParent(parent, false);
    }
	/// <summary>
	/// Set the parent of this object.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="parent"></param>
	public static void SetParent(this MonoBehaviour mb, Transform parent){
		mb.gameObject.transform.SetParent(parent);
	}
	/// <summary>
	/// Set the parent of this object.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="parent"></param>
	public static void SetParent(this MonoBehaviour mb, MonoBehaviour parent){
		mb.gameObject.transform.SetParent(parent.gameObject.transform);
	}

	/// <summary>
	/// Set the parent of this object.
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="parent"></param>
	public static void SetParent(this MonoBehaviour mb, GameObject parent){
		mb.gameObject.transform.SetParent(parent.transform);
	}

	/// <summary>
	/// Remove this object's parent.
	/// </summary>
	/// <param name="mb"></param>
	public static void RemoveParent(this MonoBehaviour mb){
		mb.gameObject.transform.SetParent(null);
	}
	/// <summary>
	/// Get all immediate children of this object.
	/// </summary>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static List<GameObject> GetAllChildren(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildren();
	}
	/// <summary>
	/// Get the Closest Parent with the specified component.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="mb"></param>
	/// <returns></returns>
	public static T GetClosestParentComponent<T>(this MonoBehaviour mb) where T : Component{
		return mb.transform.GetClosestParentComponent<T>();
		
	}

}












