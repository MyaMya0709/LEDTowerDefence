using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SptSpawner : MonoBehaviour
{
    public int stageID;

    public List<SptStageData> stageDatas;
    public SptStageData curStageData;
    public GameObject curEnemy;
    public GameObject spawnEnemies;

    public int spawnCount;
    public int spawnMaxCount;
    public float spawnInterval = 0.5f;

    public float timeCounter = 0;
    public float totalTime = 0;
    public float newTimer = 0;
    public float timeLimit = 3; //초기 딜레이
    public int timerType = 1; //타이머 타입

    public Transform corner_00;
    public Transform corner_01;
    public Transform corner_02;
    public Transform corner_03;

    public int aliveEnemyCount;
    public int gameOverCount = 100;

    public bool isStart = false;
    public bool isGameFinish = false;
    public bool isGameClear = false;
    public bool isBossStage = false;

    private void Update()
    {
        if (!isStart) return;

        timeCounter += Time.deltaTime;
        if (timeCounter > spawnInterval)
        {
            timeCounter = 0;
            totalTime += spawnInterval;
            TimeFunction();
        }
    }

    private void TimeFunction()
    {
        if (isGameFinish) return;

        newTimer += spawnInterval;

        if (timerType == 0)
        {
            //적 스폰
            SpawnRoundEnemy();

            // 라운드 종료 조건 체크
            CheckRoundEnd();
        }

        if (newTimer > timeLimit)
        {
            newTimer = 0;
            NextStep();
        }

        // 타이머 UI 업데이트
        float viewTimer = timeLimit - newTimer;
        SptGameManager.instance.defenceUI.UpdateClock(viewTimer);
    }

    private void SpawnRoundEnemy()
    {
        if (spawnMaxCount <= spawnCount) return;

        SpawnEnemy();
        aliveEnemyCount++;
        spawnCount++;
        
        // 적의 수 표기
        SptGameManager.instance.defenceUI.UpdateEnemyUI(aliveEnemyCount);
    }

    private void CheckRoundEnd()
    {
        // 살아있는 적의 수 체크
        if (aliveEnemyCount >= gameOverCount)
        {
            // 게임오버 정산 시간
            timerType = 2;
            isGameClear = false;
            NextStep();
            return;
        }

        // 디펜스 중 일 때 체크
        if (timeLimit > newTimer)
        {
            // 모든 적 스폰 && 스폰된 모든 적 제거
            if (spawnCount >= spawnMaxCount && aliveEnemyCount <= 0)
            {
                // 마지막 스테이지 체크
                if (stageDatas.Count == stageID)
                {
                    // 게임클리어 정산 시간
                    timerType = 2;
                    isGameClear = true;
                    NextStep();
                }

                // 라운드 조기 종료 ---- 남은 시간 5 이상, 남은 라운드 체크
                if (timeLimit > 5 && stageDatas.Count > stageID)
                {
                    // 대기 시간
                    NextStep();
                }
            }
        }
        // 디펜스 끝날 때 체크
        else
        {
            // 마지막 스테이지 체크
            if (stageDatas.Count == stageID)
            {
                // 살아있는 적이 있는지 체크
                if (aliveEnemyCount > 0)
                {
                    // 게임오버 정산 시간
                    timerType = 2;
                    isGameClear = false;
                    NextStep();
                }
                else
                {
                    // 게임클리어 정산 시간
                    timerType = 2;
                    isGameClear = true;
                    NextStep();
                }
            }
            else
            {
                // 보스 스테이지 체크
                if (isBossStage)
                {
                    // 살아있는 적이 있는지 체크
                    if (aliveEnemyCount > 0)
                    {
                        // 게임오버 정산 시간
                        timerType = 2;
                        isGameClear = false;
                        NextStep();
                    }
                    else
                    {
                        // 대기 시간
                        NextStep();
                    }
                }
                else
                {
                    // 대기 시간
                    NextStep();
                }
            }
        }
    }

    private void NextStep()
    {
        // 0 == 디펜스 시간 / 1 == 대기 시간 / 2 == 정산 시간 
        switch (timerType)
        {
            case 0:
                WaitingTime();
                break;
            case 1:
                DefenseTime();
                break;
            case 2:
                EndGameTime();
                break;
            default:
                break;
        }
    }

    public void DefenseTime()
    {
        StageSetting();

        // 라운드 알람
        SptGameManager.instance.defenceUI.ShowRoundNotice(stageID);

        newTimer = 0;
        timeLimit = curStageData.stageTime;
        timerType = 0;
    }

    public void WaitingTime()
    {
        newTimer = 0;
        timeLimit = 3;
        timerType = 1;
    }

    public void EndGameTime()
    {
        newTimer = 0;
        timeLimit = 3;

        //종료 함수 호출
        GameFinish();
    }

    public void SpawnEnemy()
    {
        //적 프리펩 인스턴스화
        GameObject enemyOjt = Instantiate(curEnemy, new Vector3(corner_00.position.x, corner_00.position.y, curEnemy.transform.position.z), Quaternion.identity, spawnEnemies.transform);

        // 코너 위치 세팅 후 이동 시작
        enemyOjt.GetComponent<SptEnemy>().Setting(corner_00.position, corner_01.position, corner_02.position, corner_03.position);
    }

    public void StartSetting()
    {
        // 플레이 횟수 저장
        SptRecordManager.instance.StartCountRecord();

        // 변수 초기화
        aliveEnemyCount = 0;
        spawnCount = 0;

        // UI 세팅
        SptGameManager.instance.defenceUI.SetStartUI();

        // 타이머 초기 세팅
        newTimer = 0;
        timeLimit = 3;
        timerType = 1;

        // 게임 스타트 트리거
        isStart = true;

        // 타이머UI 업데이트
        SptGameManager.instance.defenceUI.UpdateClock(timeLimit);

    }


    public void StageSetting()
    {
        stageID++;
        SptGameManager.instance.defenceUI.UpdateRoundUI(stageID);

        // 현재 스테이지 정보
        curStageData = stageDatas[stageID - 1];

        // 적 참조
        curEnemy = curStageData.enemyPrf;

        // 보스 스테이지 체크
        isBossStage = curStageData.isBossStage;

        // 스폰 가능한 수
        spawnMaxCount = curStageData.spawnCount;

        // 스폰 카운트 초기화
        spawnCount = 0;
    }

    public void GameFinish()
    {
        isGameFinish = true;

        // 도달 스테이지 저장
        SptRecordManager.instance.GoalRoundRecord(stageID);

        // 게임 끝 알림
        if (isGameClear) SptGameManager.instance.defenceUI.ShowGameNotice(1);
        else
        { 
            SptGameManager.instance.defenceUI.AllTowerDestroy();
            SptGameManager.instance.defenceUI.ShowGameNotice(-1);
        }

        // 서버 데이터 로드
        if (SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadDefenceFinData();
        else SptGameManager.instance.LoadDefenceFinNext();
    }
}