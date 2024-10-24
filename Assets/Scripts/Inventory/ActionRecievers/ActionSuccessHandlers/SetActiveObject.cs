using UnityEngine;

namespace Assets.Scripts.Inventory.ActionRecievers.ActionSuccessHandlers
{
    public class SetActiveObject : MonoBehaviour, IActionSuccessHandler
    {
        public GameObject Obj;
        public bool ActiveSet;

        public void OnSuccess()
        {
            Obj.SetActive(ActiveSet);
        }
    }
}
