using System;
using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    public class PointsContainerCollisionDetector : MonoBehaviour
    {
        public event Action<PointsContainer> onCollisionStart;
        public event Action<PointsContainer> onCollisionContinue;

        private void OnCollisionEnter(Collision col)
        {
            var colContainer = col.gameObject.GetComponent<PointsContainer>();

            if (colContainer == null)
                return;

            Debug.Log(1);
            onCollisionStart?.Invoke(colContainer);
            onCollisionContinue?.Invoke(colContainer);
        }
    }
}
