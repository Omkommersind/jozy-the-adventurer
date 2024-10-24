using UnityEngine;

namespace Assets.Scripts.Inventory.ActionRecievers
{
    public class BaseActionReceiver : MonoBehaviour, IActionReceiver
    {
        public ItemData RequiredItem;
        public bool DestroyOnSuccess = true;
        private IActionSuccessHandler[] _successHandlers;

        private void Start()
        {
            _successHandlers = GetComponents<IActionSuccessHandler>();
        }

        public bool Interact()
        {
            OnSuccess();
            return true;
        }


        public bool Interact(ItemData item)
        {
            if (item == null || item == RequiredItem)
            {
                OnSuccess();
                return true;
            }
            return false;
        }

        protected void OnSuccess()
        {
            foreach (IActionSuccessHandler handler in _successHandlers) {
                handler.OnSuccess();
            }
        
            if (DestroyOnSuccess)
            {
                Destroy(gameObject);
            }
        }
    }
}
