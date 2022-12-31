using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class AnimationClipExt{

	#if UNITY_EDITOR

	/// <summary>
	/// Gets the sprites from an animation clip. This only works in the Editor.
	/// </summary>
	/// <param name="clip"></param>
	/// <returns></returns>
	public static List<Sprite> GetSprites(this AnimationClip clip){
		List<Sprite> sprites = new List<Sprite>();
		if(clip != null){
			foreach(EditorCurveBinding binding in AnimationUtility.GetObjectReferenceCurveBindings(clip)){
				ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);
			
				foreach(ObjectReferenceKeyframe keyframe in keyframes){
					sprites.Add((Sprite)keyframe.value);
				}
			
			}
		}
		
		return sprites;
	}
	
	#endif
	
	
}