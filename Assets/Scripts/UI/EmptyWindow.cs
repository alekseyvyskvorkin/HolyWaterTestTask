using System.Collections;
using UnityEngine;

namespace TestTask.UI
{
    public class EmptyWindow : MonoBehaviour
    {
        private const float StopDistance = 10f;

        [SerializeField] private float _moveSpeed = 25f;
        [SerializeField] private RectTransform _content;

        private CanvasGroup _canvasGroup;

        private bool _isLoopContentMove = true;
        private float _maxPositionX;
        private Vector2 _endPosition;

        private IEnumerator Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            yield return new WaitForSeconds(1f);
            _maxPositionX = -_content.sizeDelta.x;
            _endPosition = new Vector2(_maxPositionX, 0);
        }

        /// <summary>
        /// Invokes from event trigger in scroll view
        /// </summary>
        public void StartDrag()
        {
            if (_canvasGroup.blocksRaycasts)
                _isLoopContentMove = false;
        }

        /// <summary>
        /// Invokes from event trigger in scroll view
        /// </summary>
        public void Drop()
        {
            _isLoopContentMove = true;
        }

        private void Update()
        {
            if (!_isLoopContentMove) return;

            _content.localPosition = Vector2.MoveTowards(_content.localPosition, _endPosition, Time.deltaTime * _moveSpeed);
            if (Vector2.Distance((Vector2)_content.localPosition, _endPosition) < StopDistance)
            {
                if (_endPosition == Vector2.zero)
                    _endPosition = new Vector2(_maxPositionX, 0);
                else
                    _endPosition = Vector2.zero;
            }
        }
    }
}