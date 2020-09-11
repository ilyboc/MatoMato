using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GP;

namespace GP
{
    public class Balloon : Enemy
    {
        public Player player1;
        private AudioSource audioSource;

        private int state = 0;
        private float stateTime;//そのSTATEになった時刻
        private float stateLength;//そのSTATEを維持する時間の長さ
        private Quaternion look;
        public float speed = 0.1f;
        public AudioClip appear, death;
        public int scoreValue = 2;

        public void initialize(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Start is called before the first frame update
        void Start()
        {
            hp = 1;
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(state);
            if (hp <= 0 && state != 998)
            {
                state = 998;

                audioSource.PlayOneShot(death);
                // instantiate particle effects
                GameObject scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
                scoreManager.GetComponent<ScoreManager>().addScore(scoreValue);
            }

            switch (state)
            {
                case 0://待機開始時間記録
                    state = 1;
                    look = Quaternion.LookRotation(player1.transform.position - transform.position);//ゆっくり向くため
                    break;

                case 1://待機
                    if (transform.position.y > 20)
                    {
                        state = 1000;
                    }
                    break;
                case 998:
                    Destroy(this.gameObject,0.2f);
                    break;
                case 1000://死亡
                    Destroy(this.gameObject);
                    break;

            }
        }
    }
}