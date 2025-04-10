using UnityEngine;

namespace UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup _canvasGroup;

        [SerializeField] 
        private bool _hideOnStart = true;

        protected virtual void Start()
        {
            if (!_hideOnStart)
                return;

            SetViewVisible(false);
        }

        public void SetViewVisible(bool isVisible)
        {
            _hideOnStart = false;

            _canvasGroup.alpha = isVisible ? 1 : 0;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }

        private void OnValidate()
        {
            if (_canvasGroup != null)
                return;

            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}