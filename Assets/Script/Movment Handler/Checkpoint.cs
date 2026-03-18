using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpointTransform;

    public Transform CheckpointTransform { get; set; }

    private void Awake()
    {
        CheckpointTransform = checkpointTransform;
    }
}
