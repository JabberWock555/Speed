using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent_AI : MonoBehaviour
{
    private RaycastHit rayHit;
    [SerializeField] private WaypointTracker waypointTracker;
    [SerializeField] [Range(0,10)] private int waypointOffest = 0;
    [SerializeField] [Range(0,100)] private float angleThreshold = 0;
    private int currentWaypointIndex = 0 ;
    public Transform[] nodes;
    private bool isDeaccelerate = false;
    float distance = Mathf.Infinity;
    Vector3 direction = Vector3.zero; 
    Transform targetWaypoint ;
    private Opponent_Car_Controller carController;

    private void Awake() {
      carController = GetComponent<Opponent_Car_Controller>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        nodes = waypointTracker.GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate() {
        AISteer();
        CalculateDistanceInWaypoints();
    }

    private void AISteer(){

        if (!nodes[currentWaypointIndex]) carController.Handbrake();

        if(direction != Vector3.zero){
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            Debug.Log("Angle : "+angle);
            if(angle > angleThreshold){
                carController.Brakes();
                carController.TurnRight();
            }else if(angle < -angleThreshold){
                carController.Brakes();
                carController.TurnLeft();
            }
            else{
                carController.ResetSteeringAngle();
            }
        }

        carController.GoForward();
    }

    private void CalculateDistanceInWaypoints(){
        if(nodes.Length ==0 ) return;

        Vector3 pos = transform.position;
        targetWaypoint  = nodes[currentWaypointIndex + waypointOffest];
        direction = targetWaypoint.position - pos;

        if( direction.magnitude < distance ){
            currentWaypointIndex++;
            distance = direction.magnitude;
            if (currentWaypointIndex >= nodes.Length)
            {
                currentWaypointIndex = 0; 
            }
            return;
        }

        CalculateWayPointOffset();
    }

    private void CalculateWayPointOffset(){
        if(nodes.Length ==0) return;

        if(carController.carSpeed > 100) waypointOffest = 10;
        else waypointOffest = 8;
    }

    private void OnDrawGizmos() {
        if(nodes.Length > 0){
            Gizmos.DrawWireSphere(targetWaypoint.position, 2f);
        }
    }
}
