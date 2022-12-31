using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExt {

    /// <summary>
    /// Returns an increment of the current int clamped at the max value
    /// </summary>
    /// <param name="num"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int ClampIncrement(this int num, int max) {
        num++;
        return Mathf.Min(num, max);
    }
    /// <summary>
    /// Returns a decrement of the current int without going below the min value.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static int ClampDecrement(this int num, int min) {
        num--;
        return Mathf.Max(num, min);
    }
    /// <summary>
    /// Returns an increment of the int, looping in the range of the min and max value.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int LoopIncrement(this int num, int min, int max) {
        num++;
        num = num > max ? min : num;
        return num;
    }
    /// <summary>
    /// Returns an increment of the int, looping in the range of 0 and the max value.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int LoopIncrement(this int num, int max) {
        return num.LoopIncrement(0, max);
    }
    /// <summary>
    /// Returns a decrement of the int, looping in the range of the min and max value.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int LoopDecrement(this int num, int min, int max) {
        num--;
        num = num < min ? max : num;
        return num;
    }
    /// <summary>
    /// Returns a decrement of the int, looping in the range of 0 and the max value.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int LoopDecrement(this int num, int max) {
        return num.LoopDecrement(0, max);
    }
}