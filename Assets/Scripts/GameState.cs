using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace Stray
{
    public class GameState : MonoBehaviour
    {

        #region Configuration
        [SerializeField]
        private int health = 100;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        [SerializeField]
        private int cold = 0;
        public int Cold
        {
            get { return cold; }
            set { cold = value; }
        }

        [SerializeField]
        private int wallet = 5;
        public int Wallet
        {
            get { return wallet; }
            set { wallet = value; }
        }

        [SerializeField]
        private ClothingLevel clothing = ClothingLevel.Full;
        public ClothingLevel Clothing
        {
            get { return clothing; }
            set
            {
                clothing = value;
                if (clothing < ClothingLevel.NoJacket && !isVip)
                {
                    canSellClothes = false;
                }

                if (clothing < ClothingLevel.NoSweatshirt)
                {
                    isCold = true;
                }
            }
        }


        [SerializeField]
        private short time;
        public short Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
                if (time == 24)
                {
                    time = 0;
                }
            }
        }
        #endregion

        #region State
        private bool isCold = false;
        private bool isVip = false;
        private bool canSellClothes = true;

        public int Day { get { return time / 24 + 1; } }

        [SerializeField]
        Inventory m_Inventory;
        public Inventory Inventory
        {
            get { return m_Inventory; }
        }
        [SerializeField]
        Place m_Place;
        public IPlace Place
        {
            get { return m_Place; }
            set { m_Place = (Place)value; }
        }
        #endregion


        private void Awake()
        {
            int sessionsCount = FindObjectsOfType<GameState>().Length;
            if (sessionsCount > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            time++;
        }
    }
}