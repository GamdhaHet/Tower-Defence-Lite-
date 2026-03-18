using UnityEngine;

namespace TD.Manager
{

    public class PlayerInfoManager : MonoBehaviour
    {
        public static PlayerInfoManager Instance;

        public int Lives;
        public int startLives = 20;

        public int Rounds;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Lives = startLives;
            Rounds = 0;
        }
    }
}