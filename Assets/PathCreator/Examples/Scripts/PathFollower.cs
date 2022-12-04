using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        bool İsFinished;
        public States state;
        
        public string DefaultSitAnim="";
        [HideInInspector] int Sittinganim=>Animator.StringToHash(DefaultSitAnim);
        public Animator anim;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            else if (GameManager.Insantance.Path != null)
            {
                pathCreator = GameManager.Insantance.Path;
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                if (transform.position == pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1))
                {
                    if (İsFinished) return;
                    İsFinished = true;
                    GameManager.Insantance.npc_Controller.currentAnimator=anim;
                    GameManager.Insantance.npc_Controller.StateEvent.Invoke(state,Sittinganim);
                    this.enabled=false;
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            Debug.Log("hi");
        }

    }
   
}