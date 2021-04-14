using Common.Data;
using UnityEngine;

namespace Common.Controllers
{
    public class GoController : MonoBehaviour
    {
        [SerializeField] private GoData _goData;

        public void SetActiveData(bool isActive)
        {
            _goData?.SetActiveObjects(isActive);
        }
    
    }
}
