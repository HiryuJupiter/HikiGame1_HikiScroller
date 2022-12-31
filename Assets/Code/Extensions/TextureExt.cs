using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureExt{
    
    /// <summary>
    /// Gets the Texture2D as a sprite.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="pixelsPerUnit"></param>
    /// <returns></returns>
    public static Sprite AsSprite(this Texture2D texture, int pixelsPerUnit = 100) {
        return Sprite.Create(texture, texture.GetRect(), new Vector2(0.5f, 0.5f), pixelsPerUnit);
    }
    /// <summary>
    /// Gets the area of the texture as a Rect.
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public static Rect GetRect(this Texture2D texture){
        return new Rect(0,0,texture.width,texture.height);
    }

    /// <summary>
    /// Draws this Texture2D onto another Texture2D.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="target"></param>
    /// <param name="offset"></param>
    /// <param name="callApply"></param>
    public static void DrawToTexture(this Texture2D texture, Texture2D target, Vector2Int offset, bool callApply = false){
		int xOffset = offset.x;
		int yOffset = offset.y;

		for(int x = 0; x < texture.width; x++){
			for(int y = 0; y < texture.height; y++){
				Color pixel = texture.GetPixel(x,y);
				if(pixel.a > 0.01f){
					target.SetPixel(x + xOffset, y + yOffset, pixel);
				}
			}
		}
        if(callApply){
            target.Apply();
        }
	}
    /// <summary>
    /// Scales the Texture2D to a given size.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static Texture2D ChangeSize(this Texture2D texture, int width, int height, int depth = 24){

        RenderTexture rt = new RenderTexture(width, height, depth);
        RenderTexture.active = rt;

        Graphics.Blit(texture, rt);

        Texture2D result = new Texture2D(width, height);

        result.ReadPixels(new Rect(0,0,width, height), 0,0);
        result.Apply();

        return result;
    }
    /// <summary>
    /// Fills the Texture2D with a given colour.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="fillColour"></param>
    /// <param name="callApply"></param>
    /// <returns></returns>
    public static Texture2D FillWithColour(this Texture2D texture, Color32 fillColour, bool callApply = false){
        Color32[] fillPixels = new Color32[texture.width * texture.height];
        for(int i = 0; i < fillPixels.Length; i++){
            fillPixels[i] = fillColour;
        }
        texture.SetPixels32(fillPixels);
        if(callApply){
            texture.Apply();
        }
        return texture;
    }
    /// <summary>
    /// Clears all of the texture's pixels.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="callApply"></param>
    /// <returns></returns>
    public static Texture2D Clear(this Texture2D texture, bool callApply = false){
        return texture.FillWithColour(Color.clear, callApply);
    }



}
