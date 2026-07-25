using TMPro;
using UnityEngine;

namespace LootNotifications
{
    public class NotificationEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private CanvasGroup canvasGroup;

        public CanvasGroup CanvasGroup => canvasGroup;

        public void Initialize(string message, Color color)
        {
            text.text = message;
            text.color = color;
            canvasGroup.alpha = 1f;
        }
    }
}