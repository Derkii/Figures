using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class Path : MonoBehaviour
    {
        [SerializeField]
        private List<Vector3> _points;

        public IEnumerable<Vector3> GetPath()
        {
            return _points;
        }
        public void SetPath(IEnumerable<Vector3> points)
        {
            _points = points.ToList();
        }

        public void AddPoint(Vector3 point)
        {
            _points.Add(point);
        }
    }
}
