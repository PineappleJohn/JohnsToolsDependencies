using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

    public class Climb : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        private Transform controllerTransform;
        public EasyHand hand = EasyHand.RightHand;
        private GameObject spawnedPrefab;
        public float proximityDistance = 0.5f;
        public LayerMask climbableLayer;

        private void Start()
        {
            controllerTransform = transform;
           
        }

        private void Update()
        {
            HandlePrefabSpawn();
        }

        private void HandlePrefabSpawn()
        {
            if (EasyInputs.GetTriggerButtonDown(hand))
            {
                if (spawnedPrefab == null && IsNearClimbableWall())
                {  
                    spawnedPrefab = Instantiate(prefabToSpawn, controllerTransform.position, controllerTransform.rotation);
                }
            }
            else if (spawnedPrefab != null)
            {
                Destroy(spawnedPrefab);
                spawnedPrefab = null;
            }
        }

        private bool IsNearClimbableWall()
{
    Collider[] nearbyClimbableWalls = Physics.OverlapSphere(controllerTransform.position, proximityDistance, climbableLayer);

    return nearbyClimbableWalls.Length > 0;
    
}
}
