using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Text
{
    public class TypewriterText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textComponent;
        [SerializeField] private float letterDelay = 0.05f;
        
        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip typingSound;
        [SerializeField] private int lettersPerSound = 2;

        public bool IsTyping { get; private set; }

        private Coroutine _typingCoroutine;
        private string _fullText;

        public event Action OnTypingFinished;

        public void Play()
        {
            _fullText = textComponent.text;

            textComponent.text = "";

            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            _typingCoroutine = StartCoroutine(TypeRoutine());
        }

        public void Skip()
        {
            if (!IsTyping)
                return;

            StopCoroutine(_typingCoroutine);

            textComponent.text = _fullText;

            IsTyping = false;

            OnTypingFinished?.Invoke();
        }

        private IEnumerator TypeRoutine()
        {
            IsTyping = true;

            for (var i = 1; i <= _fullText.Length; i++)
            {
                textComponent.text = _fullText.Substring(0, i);
                
                PlayTypingSound(i);

                yield return new WaitForSeconds(letterDelay);
            }

            IsTyping = false;

            OnTypingFinished?.Invoke();
        }
        
        private void PlayTypingSound(int index)
        {
            if (audioSource == null || typingSound == null)
                return;
            
            var currentChar = _fullText[index - 1];

            if (char.IsWhiteSpace(currentChar))
                return;
            
            if ((index - 1) % lettersPerSound != 0)
                return;

            audioSource.pitch = Random.Range(0.95f, 1.05f);

            audioSource.PlayOneShot(typingSound);
        }
    }
}