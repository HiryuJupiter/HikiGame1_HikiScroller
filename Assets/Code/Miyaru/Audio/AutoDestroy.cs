using System;
using System.Collections;
using UnityEngine;
using System.Text;

public class AutoDestroy : MonoBehaviour
{
    public float duration = 2f;
    void Awake() => Destroy(gameObject, duration);
}