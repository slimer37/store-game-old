using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Tweens
{
    public static IEnumerator CrossFadeGroup(CanvasGroup group, float to, float duration)
    {
        float from = group.alpha;
        yield return LerpValue(duration, t =>
            group.alpha = Mathf.Lerp(from, to, t));
    }

    /// <summary>
    /// Only use this if within another IEnumerator. Otherwise, use Image.CrossFadeAlpha().
    /// </summary>
    public static IEnumerator CrossFadeImage(Image image, float to, float duration)
    {
        image.CrossFadeAlpha(0, 0, true);
        image.CrossFadeAlpha(to, duration, false);
        yield return new WaitForSeconds(duration);
    }

    public static IEnumerator LerpLocation(Transform transform, Vector3 to, float duration, bool local = false)
    {
        Vector3 from = local ? transform.localPosition : transform.position;
        Action<float> callback = local ?
            (Action<float>)(t => transform.localPosition = Vector3.Lerp(from, to, t))
            : t => transform.position = Vector3.Lerp(from, to, t);
        yield return LerpValue(duration, callback);
    }

    public static IEnumerator LerpRotation(Transform transform, Quaternion to, float duration, bool local = false)
    {
        Quaternion from = local ? transform.localRotation : transform.rotation;
        Action<float> callback = local ?
            (Action<float>)(t => transform.localRotation = Quaternion.Lerp(from, to, t))
            : t => transform.rotation = Quaternion.Lerp(from, to, t);
        yield return LerpValue(duration, callback);
    }

    public static IEnumerator LerpRotationEuler(Transform transform, Vector3 to, float duration, bool local = false)
    => LerpRotation(transform, Quaternion.Euler(to), duration, local);

    public static IEnumerator LerpTransform(Transform transform, Vector3 toPos, Quaternion toRot, float duration, bool local = false)
    {
        Vector3 start = transform.position;
        Quaternion startRot = transform.rotation;
        Action<float> callback = local ?
        (Action<float>)(t =>
        {
            transform.localPosition = Vector3.Lerp(start, toPos, t);
            transform.localRotation = Quaternion.Lerp(startRot, toRot, t);
        })
        : t =>
        {
            transform.position = Vector3.Lerp(start, toPos, t);
            transform.rotation = Quaternion.Lerp(startRot, toRot, t);
        };
        yield return LerpValue(duration, callback);
    }

    public static IEnumerator LerpValue(float duration, Action<float> callback)
    {
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            callback.Invoke(t);
            yield return null;
        }
        callback.Invoke(1);
    }
}
