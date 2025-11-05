using UnityEngine;
namespace MemoryGame.Views
{
    [RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform _rt; Rect _last;
    void Awake(){ _rt = GetComponent<RectTransform>(); Apply(); }
    void Update(){ if (Screen.safeArea != _last) Apply(); }
    void Apply()
    {
        _last = Screen.safeArea;
        var min = _last.position; var max = _last.position + _last.size;
        min.x /= Screen.width;  min.y /= Screen.height;
        max.x /= Screen.width;  max.y /= Screen.height;
        _rt.anchorMin = min; _rt.anchorMax = max;
        _rt.offsetMin = Vector2.zero; _rt.offsetMax = Vector2.zero;
    }
}

}