using UnityEngine;
using System.Collections;

public class TextureOffsetScroll : MonoBehaviour
{
    public Vector2 ScrollSpeed;
    Vector2 initialOffset;
	Renderer _render;

	void Start ()
    {
        _render = GetComponent<Renderer> ();
        initialOffset = _render.sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update ()
    {
        float x = Mathf.Repeat (Time.time * ScrollSpeed.x, 1);
        float y = Mathf.Repeat (Time.time * ScrollSpeed.y, 1);
		Vector2 offset = new Vector2 (x, y);
        _render.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable ()
    {
        _render.sharedMaterial.SetTextureOffset ("_MainTex", initialOffset);
	}
}