using UnityEngine;

namespace Player.Interactions
{
    public class HighlightEffect : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _highlightColor = Color.yellow;
        private Color _defaultColor;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _defaultColor = _spriteRenderer.color;
        }
        
        public void EnableHighlight()
        {
            _spriteRenderer.color = _highlightColor;
        }


        public void DisableHighlight()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}