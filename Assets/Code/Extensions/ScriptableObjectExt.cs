using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectExt{

#if UNITY_EDITOR
    /// <summary>
    /// Adds another ScriptableObject as a SubAsset.
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="objectToAdd"></param>
    public static void AddSubAsset(this ScriptableObject asset, ScriptableObject objectToAdd) {
        AssetDatabase.AddObjectToAsset(objectToAdd, asset);
        asset.Dirty();
        AssetDatabase.SaveAssets();
        objectToAdd.Import();
        
    }
    /// <summary>
    /// Forces the Editor to save a ScriptableObject's changes.
    /// </summary>
    /// <param name="asset"></param>
    public static void ForceSaveChanges(this ScriptableObject asset) {
        asset.Dirty();
        AssetDatabase.SaveAssetIfDirty(asset);
        asset.Import();
    }
    /// <summary>
    /// Deletes this ScriptableObject
    /// </summary>
    /// <param name="asset"></param>
    public static void DeleteAsset(this ScriptableObject asset) {
        AssetDatabase.DeleteAsset(asset.GetPath());
    }

    /// <summary>
    /// Make the Editor import this ScriptableObject Asset. ScriptableObject must exist as an asset first.
    /// </summary>
    /// <param name="asset"></param>
    public static void Import(this ScriptableObject asset) {
        AssetDatabase.ImportAsset(asset.GetPath());
    }
    /// <summary>
    /// Inform the Editor that changes have been made to this ScriptableObject.
    /// </summary>
    /// <param name="asset"></param>
    public static void Dirty(this ScriptableObject asset) {
        EditorUtility.SetDirty(asset);
    }
    /// <summary>
    /// Get the directory this ScriptableObject exists in.
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    public static string GetDirectoryPath(this ScriptableObject asset) {
        string path = asset.GetPath();
        return path.Substring(0, path.LastIndexOf("/")+1);
    }
    /// <summary>
    /// Get the Path to this ScriptableObject.
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    public static string GetPath(this ScriptableObject asset) {
        return AssetDatabase.GetAssetPath(asset);
    }
    /// <summary>
    /// Save this ScriptableObject as an Asset.
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="path"></param>
    public static void SaveAsAsset(this ScriptableObject asset, string path) {
        AssetDatabase.CreateAsset(asset, path);
    }
    /// <summary>
    /// Save Changes to this ScriptableObject.
    /// </summary>
    /// <param name="asset"></param>
    public static void SaveChanges(this ScriptableObject asset){
        asset.Dirty();
        AssetDatabase.SaveAssets();
        asset.Import();
    }
#endif
}
