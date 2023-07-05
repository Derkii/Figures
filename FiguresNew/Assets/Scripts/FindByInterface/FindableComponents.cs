using UnityEngine;

namespace FindByInterface
{
    public class FindableComponents : MonoBehaviour
    {
        [SerializeField]
        private Component[] _componentsToAddToListOfFoundableComponents;

        private void Awake()
        {
            foreach (var findableComponent in _componentsToAddToListOfFoundableComponents)
            {
                Finder.AddComponent(findableComponent);
            }
        }
    }
}