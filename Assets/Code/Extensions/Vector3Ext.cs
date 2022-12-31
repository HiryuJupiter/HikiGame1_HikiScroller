using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Ext{
	
	/// <summary>
	/// Creates a copy of a Vector3
	/// </summary>
	/// <param name="vec"></param>
	/// <returns></returns>
	public static Vector3 Clone(this Vector3 vec){
		return new Vector3(vec.x, vec.y, vec.z);
	}
	/// <summary>
	/// Reverses the Direction of a Vector3
	/// </summary>
	/// <param name="vec"></param>
	/// <returns></returns>
	public static Vector3 Reverse(this Vector3 vec){
		return new Vector3(-vec.x, -vec.y, -vec.z);
	}
	
	
}
