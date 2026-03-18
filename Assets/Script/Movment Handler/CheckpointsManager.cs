using System.Collections.Generic;
using UnityEngine;

namespace TD.EnemyMovement
{
    public class CheckpointsManager : MonoBehaviour
    {
        public static CheckpointsManager Instance;
        public Checkpoint defaultCheckpoint;
        [SerializeField] private List<Checkpoint> _checkpoints;

        public List<Checkpoint> Checkpoints { get; set; }

        private void Awake()
        {
            Instance = this;
            Checkpoints = _checkpoints;
        }
    }
}