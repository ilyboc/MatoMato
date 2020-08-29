using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GP;

namespace GP
{
    public class Target : Enemy
    {
        private AudioSource audioSource;
        public Player player1;
        private int state = 0;
        private float stateTime;//そのSTATEになった時刻
        private float stateLength;//そのSTATEを維持する時間の長さ
        private Quaternion look;
        public float rotationSpeed = 10;
        public AudioClip appear, death;

        public void initialize(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Start is called before the first frame update
        void Start()
        {
            hp = 1;
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(appear);
            transform.rotation = Quaternion.Euler(-5,0,0);
        }

        // Update is called once per frame
        void Update()
        {
            if (hp <= 0 && state != 1000)
            {
                state = 999;
                // destroy animation
                Destroy(this.gameObject, 3.0f);
            }

            switch (state)
            {
                case 0://待機開始時間記録
                    stateTime = Time.realtimeSinceStartup;
                    stateLength = Random.Range(3.0f, 4.0f);
                    state = 1;
                    //transform.LookAt(player1.transform.position);
                    look = Quaternion.LookRotation(player1.transform.position - transform.position);//ゆっくり向くため
                    break;

                case 1://待機
                    if (Time.realtimeSinceStartup - stateTime > stateLength)
                    {
                        state = 2;
                        // disappear animation
                    }
                    transform.rotation = Quaternion.LerpUnclamped(transform.rotation, look, rotationSpeed * Time.deltaTime); //ゆっくり向く
                    break;
                case 999:
                    audioSource.PlayOneShot(death);
                    state = 1000;
                    break;
                case 1000://死亡
                    transform.position = transform.position - new Vector3(0, 0.1f, 0);
                    break;

            }
        }
    }
}