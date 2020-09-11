using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GP;

namespace GP
{
    public class EnemyManager : MonoBehaviour
    {
        public Player player1;
        public eyeBat eyeBatToCreate;
        public Target targetToCreate;
        public Target malusTargetToCreate;
        public Balloon balloonToCreate;
        protected Enemy[] enemyArray;
        int maxEnemies = 100;

        // Start is called before the first frame update
        void Start()
        {
            enemyArray = new Enemy[maxEnemies];
            for (int i = 0; i < maxEnemies; i++)
            {
                enemyArray[i] = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                makeEyeBat(Random.Range(-25.0f, 25.0f), Random.Range(0.0f, 10.0f), 60);
            }

        }

        public void makeEyeBat(float x, float y, float z)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] == null)
                {
                    enemyArray[i] = Instantiate(eyeBatToCreate);
                    eyeBat enemy = enemyArray[i] as eyeBat;
                    enemy.initialize(x, y, z);
                    enemy.player1 = this.player1;
                    break;
                }
            }
        }
        public void makeTarget(float x, float y, float z)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] == null)
                {
                    enemyArray[i] = Instantiate(targetToCreate);
                    Target enemy = enemyArray[i] as Target;
                    enemy.initialize(x, y, z);
                    enemy.player1 = this.player1;
                    break;
                }
            }
        }
        public void makeMalusTarget(float x, float y, float z)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] == null)
                {
                    enemyArray[i] = Instantiate(malusTargetToCreate);
                    Target enemy = enemyArray[i] as Target;
                    enemy.initialize(x, y, z);
                    enemy.player1 = this.player1;
                    break;
                }
            }
        }
        public void makeBalloon(float x, float y, float z)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] == null)
                {
                    enemyArray[i] = Instantiate(balloonToCreate);
                    Balloon enemy = enemyArray[i] as Balloon;
                    enemy.initialize(x, y, z);
                    enemy.player1 = this.player1;
                    break;
                }
            }
        }

        public bool isEmptyEnemies()
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] != null)
                {
                    return false;
                }
            }
            return true;

        }
        public void allDelete()
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (enemyArray[i] != null)
                {
                    Destroy(enemyArray[i]);
                }
            }
        }
    }
}