﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExt {

	/// <summary>
	/// Resize the List, destroying any gameObjects that no longer fit.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="newSize"></param>
	/// <exception cref="System.Exception"></exception>
	public static void ResizeDestroy<T>(this List<T> list, int newSize) where T : MonoBehaviour{
		if(newSize > list.Count) {
			throw new System.Exception("ResizeDestroy can only be called with a size equal or smaller than the list. To increase size use list.ResizeInstantiate(newSize, prefab, parent)");
        }else if(newSize == list.Count) {
			return;
        }
		List<T> toDestroy = new List<T>();
		toDestroy.AddRange(list.GetFromEnd(list.Count - newSize));
		list.Resize(newSize);

		foreach(T t in toDestroy) {
			GameObject.Destroy(t.gameObject);
        }
    }
	/// <summary>
	/// Resize the list, creating new instances of the object to fill in the new entries.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="newSize"></param>
	/// <param name="prefab"></param>
	/// <param name="parent"></param>
	/// <exception cref="System.Exception"></exception>
	public static void ResizeInstantiate<T>(this List<T> list, int newSize, T prefab, GameObject parent) where T : MonoBehaviour {
		if(newSize < list.Count) {
			throw new System.Exception("ResizeInstantiate can only be called with a size equal or greater than the list. To decrease size using list.ResizeDestroy(newSize) or list.Resize(newSize)");
        }else if(newSize == list.Count) {
			return;
        }
		int newItemsToAdd = newSize - list.Count;
		for (int i = 0; i < newItemsToAdd; i++) {
			T instance = parent.AddInstanceAsChild(prefab);
			list.Add(instance);
		}
    }


	/// <summary>
	/// Activate the next object in the list from the first currently active object. 
	/// </summary>
	/// <param name="list"></param>
	/// <param name="deactivateCurrentlyActiveObject"></param>
	/// <returns>Returns true if successful activating an object</returns>
	public static bool ActivateNext(this List<GameObject> list, bool deactivateCurrentlyActiveObject = true) {
		int activeIndex = list.FirstActiveIndex();

		if(activeIndex != -1) {
			if (deactivateCurrentlyActiveObject) {
				list[activeIndex].SetActive(false);
			}

			if(activeIndex < list.Count - 1) {
				activeIndex++;
			} else if(activeIndex == list.Count - 1) {
				activeIndex = 0;
			}

			list[activeIndex].SetActive(true);
			return true;
        }
		return false;
	}

	/// <summary>
	/// Activate the previous object in the list from the first currently active object. 
	/// </summary>
	/// <param name="list"></param>
	/// <param name="deactivateCurrentlyActiveObject"></param>
	/// <returns>Returns true if successful activating an object</returns>
	public static bool ActivatePrevious(this List<GameObject> list, bool deactivateCurrentlyActiveObject = true) {
		int activeIndex = list.FirstActiveIndex();

		if(activeIndex != -1) {
			if (deactivateCurrentlyActiveObject) {
				list[activeIndex].SetActive(false);
			}

			if(activeIndex == 0) {
				activeIndex = list.Count - 1;
            } else {
				activeIndex--;
            }

			list[activeIndex].SetActive(true);
			return true;
        }
		return false;
    }

	/// <summary>
	/// Gets the index of the first active object in the list
	/// </summary>
	/// <param name="list"></param>
	/// <returns>Returns -1 if no objects are active</returns>
	public static int FirstActiveIndex(this List<GameObject> list) {
		
		for(int i = 0; i < list.Count; i++) {
			if(list[i].activeSelf) {
				return i;
			}
		}
		return -1;
	}
	/// <summary>
	/// Deactivate all objects in the list.
	/// </summary>
	/// <param name="list"></param>
	public static void DeactivateAll(this List<GameObject> list) {
		list.ForEach((go) => go.SetActive(false));
    }
	/// <summary>
	/// Activate all objects in the list.
	/// </summary>
	/// <param name="list"></param>
	public static void ActivateAll(this List<GameObject> list) {
		list.ForEach((go) => go.SetActive(true));
    }
	/// <summary>
	/// Gets all objects in the list that are not null.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static List<T> NonNull<T>(this List<T> list) where T : class {
		List<T> nList = new List<T>(list);
		nList.RemoveAll(n => n == null);
		return nList;
    }
	/// <summary>
	/// Resizes the list by removing elements or adding default elements for the List's type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="newCount"></param>
	public static void Resize<T>(this List<T> list, int newCount) {
		if(newCount <= 0) {
			list.Clear();
		} else {
			while(list.Count > newCount) {
				list.RemoveAt(list.Count - 1);
			}
			while(list.Count < newCount) {
				list.Add(default(T));
			}
		}
	}
	/// <summary>
	/// Check if an index exists within the list
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static bool ValidIndex<T>(this List<T> list, int index) {
		return index >= 0 && index < list.Count;
	}
	/// <summary>
	/// Remove a specified amount of elements from the end of the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="amount"></param>
	public static void RemoveFromEnd<T>(this List<T> list, int amount) {
		if(amount > list.Count) {
			list.Clear();
		}
		for(int i = 0; i < amount; i++) {
			list.RemoveAt(list.Count - 1);
		}
	}
	/// <summary>
	/// Get a specified amount of elements from the end of the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="amount"></param>
	/// <returns></returns>
	public static List<T> GetFromEnd<T>(this List<T> list, int amount) {
		List<T> res = new List<T>();
		if(list.Count <= amount) {
			res.AddRange(list);
			return res;
		} else {

			for(int i = list.Count - 1, j = 0; i >= 0 && j < amount; i--, j++) {
				res.Add(list[i]);
			}
		}
		res.Reverse();
		return res;
	}
	/// <summary>
	/// Concatenate all strings in the list to a single string.
	/// </summary>
	/// <param name="list"></param>
	/// <param name="separator"></param>
	/// <returns></returns>
	public static string AsString(this List<string> list, string separator = "") {
		string res = "";
		list.ForEach(s => res += s + separator);
		return res;
    }
	/// <summary>
	/// Get a specified amount of random elements from the list
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="amount"></param>
	/// <returns></returns>
	public static List<T> Random<T>(this List<T> list, int amount) {
		List<T> res = new List<T>();
		if(list.Count > 0) {
			for(int i = 0; i < amount; i++) {
				res.Add(list.Random());
			}
		}
		return res;
    }

	/// <summary>
	/// Get a random element from the list within a given range of indexes
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="minIndex"></param>
	/// <param name="maxIndex"></param>
	/// <returns></returns>
	public static T Random<T>(this List<T> list, int minIndex, int maxIndex) {
		int index = UnityEngine.Random.Range(minIndex, maxIndex);
		return list[index];
		
    }

	/// <summary>
	/// Get a random element from the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T Random<T>(this List<T> list) {
		if(list.Count == 0) {
			return default(T);
		} else if(list.Count == 1) {
			return list[0];
		}
		return list[UnityEngine.Random.Range(0, list.Count)];

	}
	/// <summary>
	/// Get the last element from the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T Last<T>(this List<T> list) {
		if(list.Count < 1) {
			return default(T);
		}
		return list[list.Count - 1];
	}
	/// <summary>
	/// Get the last X elements from the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="amount"></param>
	/// <returns></returns>
	public static List<T> Last<T>(this List<T> list, int amount) {
		return list.GetFromEnd(amount);
    }
	/// <summary>
	/// Shuffle the order of elements in the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static List<T> Shuffle<T>(this List<T> list) {
		for(int i = 0; i < list.Count; i++) {

			int r = UnityEngine.Random.Range(0, list.Count);

			T value = list[r];
			list[r] = list[0];
			list[0] = value;
		}
		return list;
	}
	/// <summary>
	/// Get the first X elements from the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="amount"></param>
	/// <returns></returns>
	public static List<T> GetFirst<T>(this List<T> list, int amount = 1) {
		List<T> l = new List<T>();
		for(int i = 0; i < amount && i < list.Count; i++) {
			l.Add(list[i]);
		}
		return l;
	}
	/// <summary>
	/// Get the index of the last element in the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static int LastIndex<T>(this List<T> list) {
		return list.Count - 1;
    }

	/// <summary>
	/// Get the middle element in the list. If the number of elements is even, it returns the lower indexed element of the two middle-most elements.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T Middle<T>(this List<T> list){
		if(list.Count == 0){
			return default(T);
		}
		return list.Half(true).Last();
	}
	/// <summary>
	/// Gets half of the list. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="ceil">If true then it will include the middle for odd numbered lists.</param>
	/// <returns></returns>
	public static List<T> Half<T>(this List<T> list, bool ceil = true) {
		List<T> halfList = new List<T>();
		if(list.Count < 2) {
			halfList.AddRange(list);
        } else {
			float count = list.Count;
			count *= 0.5f;
			int targetCount = (int)count;
            if(ceil) {
				targetCount = Mathf.CeilToInt(count);
            }
			if(targetCount >= list.Count) {
				halfList.AddRange(list);
            } else {
				halfList.AddRange(list.GetFirst(targetCount));
            }
        }
		return halfList;
    }
	/// <summary>
	/// Shuffle the elements in the list X times.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="times"></param>
	/// <returns></returns>
	public static List<T> Shuffle<T>(this List<T> list, int times) {
		for(int i = 0; i < times; i++) {
			list.Shuffle();
		}
		return list;
	}
	/// <summary>
	/// Create a copy of the list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static List<T> Copy<T>(this List<T> list) {
		return new List<T>(list);
    }
	/// <summary>
	/// Swap two elements in the list
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="index1"></param>
	/// <param name="index2"></param>
	public static void Swap<T>(this List<T> list, int index1, int index2) {
		T temp = list[index1];
		list[index1] = list[index2];
		list[index2] = temp;
    }
	/// <summary>
	/// Calls GetComponent<K> on each element and creates a new list with those returned components.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="K"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static List<K> AsComponents<T, K>(this List<T> list) where T : MonoBehaviour where K : MonoBehaviour{
		List<K> components = new List<K>();
		foreach(T mb in list) {
			K k = mb.GetComponent<K>();
            if (k) {
				components.Add(k);
            }
        }
		return components;

    }

}