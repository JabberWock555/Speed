using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTracker : MonoBehaviour
{
    [SerializeField] private Color lineColor;

    private Transform[] nodes;
   
   private void Awake() {
     nodes = GetComponentsInChildren<Transform>();
   }
   public Transform[] GetWaypoints() => nodes;

   private void OnDrawGizmosSelected()
   {
       nodes = GetComponentsInChildren<Transform>();
       for (int i = 0; i < nodes.Length; i++){
            Vector3 currentNode = nodes[i].position;
            Vector3 prevNode = Vector3.zero;

            if(i != 0){
               prevNode = nodes[i-1].position;
            }
            else {
               prevNode = nodes[nodes.Length - 1].position;
            }

            Gizmos.DrawLine(prevNode, currentNode);   
            Gizmos.DrawSphere(currentNode, 1f); 
       }
   }
}
