using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static System.Reflection.Assembly;
using System.Reflection;

public static class ReflectionHelper{
	
	/// <summary>
	/// Gets all Types which extend from the provided Type.
	/// </summary>
	/// <param name="appDomain"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public static Type[] GetAllDerivedTypes(this AppDomain appDomain, Type type){
		List<Type> results = new List<Type>();
		Assembly[] assemblies = appDomain.GetAssemblies();
		foreach(Assembly assembly in assemblies){
			Type[] types = assembly.GetTypes();
			foreach(Type t in types){
				if(t.IsSubclassOf(type)){
					results.Add(t);
				}
			}
		}
		return results.ToArray();
	}
	/// <summary>
	/// Get Types as their names.
	/// </summary>
	/// <param name="types"></param>
	/// <returns></returns>
	public static string[] TypesAsNames(Type[] types){
		List<string> names = new List<string>();
		foreach(Type type in types){
			names.Add(type.Name);
		}
		return names.ToArray();
	}
	
	
}