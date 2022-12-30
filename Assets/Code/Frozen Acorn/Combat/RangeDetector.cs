using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RangeDetector : MonoBehaviour {

    Entity target;
    public string targetTag = "Player";

    public bool flipScaleWithSpriteRendererX;
    [HideIf("@!flipScaleWithSpriteRendererX")]
    public SpriteRenderer flipSpriteRenderer;

    [ShowInInspector]
    public bool HasTarget { get => target; }


    void Update() {
        if (flipScaleWithSpriteRendererX) {
            Vector3 scale = transform.localScale;
            if (flipSpriteRenderer.flipX) {
                if (scale.x > 0) {
                    scale.x = -scale.x;
                }
            
            } else {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!target && collision.gameObject.CompareTag(targetTag)) {
            target = collision.gameObject.GetComponent<Entity>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(target && collision.gameObject == target.gameObject) {
            target = null;
        }
    }


    


}