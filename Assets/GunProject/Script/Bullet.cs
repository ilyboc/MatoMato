using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GP
{
    public class Bullet : MonoBehaviour
    {

        public float destroyTime;



        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        // Update is called once per frame
        void Update()
        {




        }
    }

}
