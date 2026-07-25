using System.Collections;
using System.Collections.Generic;
using LootNotifications;
using UnityEngine;

namespace Managers
{
    public class PickupNotificationManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Transform content;
        [SerializeField] private NotificationEntry notificationPrefab;

        [Header("Settings")]
        [SerializeField] private int maxMessages = 6;
        [SerializeField] private float messageLifetime = 5f;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private float fadeUpDuration = 0.5f;

        private readonly List<NotificationEntry> _messages = new();

        public void AddMessage(string text, Color color)
        {
            var entry = Instantiate(notificationPrefab, content);

            entry.Initialize(text, color);

            _messages.Add(entry);

            StartCoroutine(RemoveAfterTime(entry));

            if (_messages.Count > maxMessages)
            {
                Destroy(_messages[0].gameObject);
                _messages.RemoveAt(0);
            }
        }

        private IEnumerator RemoveAfterTime(NotificationEntry entry)
        {
            yield return new WaitForSeconds(messageLifetime);

            var timer = 0f;

            while (timer < fadeDuration)
            {
                if (entry == null)
                    yield break;
                
                timer += Time.deltaTime;
                
                entry.CanvasGroup.alpha = Mathf.Lerp(
                    1f, 
                    0f,
                    timer / fadeDuration);
                
                var startPos = entry.transform.localPosition;
                var endPos = startPos + Vector3.up * fadeUpDuration;
                
                entry.transform.localPosition = Vector3.Lerp(
                    startPos,
                    endPos,
                    timer / fadeDuration);

                yield return null;
            }
            
            if (entry == null)
                yield break;

            _messages.Remove(entry);

            Destroy(entry.gameObject);
        }
    }
}