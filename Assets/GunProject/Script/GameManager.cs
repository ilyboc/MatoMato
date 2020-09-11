using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GP
{
    public class GameManager : MonoBehaviour
    {
        public TextMeshProUGUI gameText;//カウントダウン
        public TextMeshProUGUI timeText;//ゲーム開始からの経過時間
        public TextMeshProUGUI scoreText;//スコア

        public EnemyManager enemyManagerToCreate;
        private EnemyManager enemyManager;
        public ScoreManager scoreManagerToCreate;
        private ScoreManager scoreManager;

        public Player playerToCreate;
        private Player player1;

        protected float gameStartTime;

        protected int gameState = 0;
        protected float stateStartTime;
        protected int enemyMakeState = 0;
        protected float lastEnemyTime;

        // Start is called before the first frame update
        void Start()
        {
            gameText.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("gamestate:" + gameState);
            if (gameState > 0 && gameState < 1000)
            {
                timeText.text = "Time:" + (int)(Time.realtimeSinceStartup - gameStartTime);
                scoreText.text = "Score: " + scoreManager.Score;
            }

            switch (gameState)
            {
                case 0://ゲーム開始前待機
                    gameText.text = "Press Space";
                    timeText.text = "";
                    scoreText.text = "";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        enemyManager = Instantiate(enemyManagerToCreate);
                        scoreManager = Instantiate(scoreManagerToCreate);
                        player1 = Instantiate(playerToCreate, new Vector3(0, 0, 0), Quaternion.identity);
                        enemyManager.player1 = this.player1;
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;

                case 1://ゲーム開始準備(一定時間待機)
                    gameText.text = "3";
                    timeText.text = "";
                    scoreText.text = "";
                    if (Time.realtimeSinceStartup - stateStartTime > 1.0f) gameText.text = "2";
                    if (Time.realtimeSinceStartup - stateStartTime > 2.0f) gameText.text = "1";
                    if (Time.realtimeSinceStartup - stateStartTime > 3.0f)
                    {
                        gameText.text = "";
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                        gameStartTime = Time.realtimeSinceStartup;
                    }
                    break;

                // Wave1
                case 2:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeBalloon(0, -1.5f, 10);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 4 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                enemyManager.makeTarget(0, 3+enemyMakeState*3, 13);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 3: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 4://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave2
                case 5:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(Random.Range(-12.0f,12.0f), 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 4 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                enemyManager.makeTarget(Random.Range(-12.0f, 12.0f), 3 + enemyMakeState * 3, 13);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 6: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 7://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave3
                case 8:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(-12, 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 15 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                if (enemyMakeState <= 7)
                                {
                                    enemyManager.makeTarget(-12 + 4 * enemyMakeState, 3, 13);
                                } else
                                {
                                    enemyManager.makeTarget(12 - 4 * (enemyMakeState-8), 15, 13);
                                }
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 9: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 10://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave4
                case 11:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(-3, 3, 13);
                            enemyManager.makeTarget(3, 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 4 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                enemyManager.makeTarget(-3, 3 + enemyMakeState * 3, 13);
                                enemyManager.makeTarget(3, 3 + enemyMakeState * 3, 13);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 12: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 13://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave5
                case 14:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeBalloon(-3, 0f, 8);
                            enemyManager.makeBalloon(3, 0f, 8);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 3 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                enemyManager.makeBalloon(-3 - 3*enemyMakeState, 0f, 8);
                                enemyManager.makeBalloon(3 + 3*enemyMakeState, 0f, 8);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 15: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 16://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave6
                case 17:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(-12, 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 15 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                if (enemyMakeState <= 7)
                                {
                                    if (enemyMakeState % 3 == 0) enemyManager.makeMalusTarget(-12 + 4 * enemyMakeState, 3, 13);
                                    else enemyManager.makeTarget(-12 + 4 * enemyMakeState, 3, 13);
                                }
                                else
                                {
                                    if (enemyMakeState % 3 == 0) enemyManager.makeMalusTarget(12 - 4 * (enemyMakeState - 8), 15, 13);
                                    else enemyManager.makeTarget(12 - 4 * (enemyMakeState - 8), 15, 13);
                                }
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 18: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 19://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave2
                case 20:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(Random.Range(-12.0f, 12.0f), 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 4 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                if (n == 4) enemyManager.makeMalusTarget(Random.Range(-12.0f, 12.0f), 3 + enemyMakeState * 3, 13);
                                else enemyManager.makeTarget(Random.Range(-12.0f, 12.0f), 3 + enemyMakeState * 3, 13);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 21: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 22:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(Random.Range(-12.0f, 12.0f), 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 4 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                if (n == 3) enemyManager.makeMalusTarget(Random.Range(-12.0f, 12.0f), 3 + enemyMakeState * 3, 13);
                                else enemyManager.makeTarget(Random.Range(-12.0f, 12.0f), 3 + enemyMakeState * 3, 13);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 23: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave6
                case 24:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeTarget(-12, 3, 13);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 15 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.15f)
                            {
                                if (enemyMakeState <= 7)
                                {
                                    if (enemyMakeState % 3 == 0) enemyManager.makeMalusTarget(-12 + 4 * enemyMakeState, 3, 13);
                                    else enemyManager.makeTarget(-12 + 4 * enemyMakeState, 3, 13);
                                }
                                else
                                {
                                    if (enemyMakeState % 3 == 0) enemyManager.makeMalusTarget(12 - 4 * (enemyMakeState - 8), 15, 13);
                                    else enemyManager.makeTarget(12 - 4 * (enemyMakeState - 8), 15, 13);
                                }
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 25: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                case 26://(一定時間待機)
                    if (Time.realtimeSinceStartup - stateStartTime > 0.5f)
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;
                // Wave5
                case 27:
                    switch (enemyMakeState)
                    {
                        case 0:
                            enemyManager.makeBalloon(-3, 0f, 8);
                            enemyManager.makeBalloon(3, 0f, 8);
                            lastEnemyTime = Time.realtimeSinceStartup;
                            enemyMakeState++;
                            break;
                        case int n when (n <= 3 && n >= 1):
                            if (Time.realtimeSinceStartup - lastEnemyTime > 0.5f)
                            {
                                enemyManager.makeBalloon(-3 - 3 * enemyMakeState, 0f, 8);
                                enemyManager.makeBalloon(3 + 3 * enemyMakeState, 0f, 8);
                                lastEnemyTime = Time.realtimeSinceStartup;
                                enemyMakeState++;
                            }
                            break;
                        default:
                            gameState++;
                            enemyMakeState = 0;
                            break;
                    }
                    break;

                case 28: // 敵がいなくなったら次のwaveへ
                    if (enemyManager.isEmptyEnemies())
                    {
                        gameState++;
                        stateStartTime = Time.realtimeSinceStartup;
                    }
                    break;

                case 1000://ゲームクリア

                    timeText.text = "Time:" + (int)(Time.realtimeSinceStartup - gameStartTime);
                    gameText.text = "Clear\n" + "Your Score: " + scoreManager.Score;
                    gameState++;
                    break;

                case 1001:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        enemyManager.allDelete();
                        resetGame();
                        gameState = 0;
                    }
                    break;

                case 2000://ゲームオーバー
                    gameText.fontSize = 200;
                    gameText.text = "You Died!!";
                    gameState++;
                    break;

                case 2001:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        enemyManager.allDelete();
                        resetGame();
                        gameState = 0;
                    }
                    break;

                default:
                    gameState = 1000;
                    break;

            }

        }

        private void resetGame()
        {
            Destroy(player1);
            Destroy(enemyManager);
        }
    }
}

