using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

#region WaveData
[Serializable]
public class SptRecordData
{
    public int totalGameStartCount;               // 게임 시작 횟수
    public int totalGambleTowerCount;             // 타워 뽑기 횟수

    // 타워 소환 횟수
    #region TowerSpawnCount      
    [Header("RedSpawn")]
    public int totalRedLv1SpawnCount;             // red 등급별 소환 횟수
    public int totalRedLv2SpawnCount;
    public int totalRedLv3SpawnCount;
    public int totalRedLv4SpawnCount;
    public int totalRedLv5SpawnCount;
    public int totalRedLv6SpawnCount;
    public int totalRedLv7SpawnCount;
    public int totalRedLv8SpawnCount;
    public int totalRedLv9SpawnCount;

    [Header("BlueSpawn")]
    public int totalBlueLv1SpawnCount;            // blue 등급별 소환 횟수
    public int totalBlueLv2SpawnCount;
    public int totalBlueLv3SpawnCount;
    public int totalBlueLv4SpawnCount;
    public int totalBlueLv5SpawnCount;
    public int totalBlueLv6SpawnCount;
    public int totalBlueLv7SpawnCount;
    public int totalBlueLv8SpawnCount;
    public int totalBlueLv9SpawnCount;

    [Header("GreenSpawn")]
    public int totalGreenLv1SpawnCount;           // green 등급별 소환 횟수
    public int totalGreenLv2SpawnCount;
    public int totalGreenLv3SpawnCount;
    public int totalGreenLv4SpawnCount;
    public int totalGreenLv5SpawnCount;
    public int totalGreenLv6SpawnCount;
    public int totalGreenLv7SpawnCount;
    public int totalGreenLv8SpawnCount;
    public int totalGreenLv9SpawnCount;
    #endregion

    // 타입별 강화 등급 횟수
    #region TypeEnhanceLvCount
    [Header("RedEnhance")]
    public int totalRedEnhanceLv00Count;         // 레드타입의 강화 횟수
    public int totalRedEnhanceLv01Count;
    public int totalRedEnhanceLv02Count;
    public int totalRedEnhanceLv03Count;
    public int totalRedEnhanceLv04Count;
    public int totalRedEnhanceLv05Count;
    public int totalRedEnhanceLv06Count;
    public int totalRedEnhanceLv07Count;
    public int totalRedEnhanceLv08Count;
    public int totalRedEnhanceLv09Count;
    public int totalRedEnhanceLv10Count;
    public int totalRedEnhanceLv11Count;
    public int totalRedEnhanceLv12Count;
    public int totalRedEnhanceLv13Count;
    public int totalRedEnhanceLv14Count;
    public int totalRedEnhanceLv15Count;
    public int totalRedEnhanceLv16Count;
    public int totalRedEnhanceLv17Count;
    public int totalRedEnhanceLv18Count;
    public int totalRedEnhanceLv19Count;
    public int totalRedEnhanceLv20Count;
    public int totalRedEnhanceLv21Count;
    public int totalRedEnhanceLv22Count;
    public int totalRedEnhanceLv23Count;
    public int totalRedEnhanceLv24Count;
    public int totalRedEnhanceLv25Count;
    public int totalRedEnhanceLv26Count;
    public int totalRedEnhanceLv27Count;
    public int totalRedEnhanceLv28Count;
    public int totalRedEnhanceLv29Count;
    public int totalRedEnhanceLv30Count;
    public int totalRedEnhanceLv31Count;
    public int totalRedEnhanceLv32Count;
    public int totalRedEnhanceLv33Count;
    public int totalRedEnhanceLv34Count;
    public int totalRedEnhanceLv35Count;
    public int totalRedEnhanceLv36Count;
    public int totalRedEnhanceLv37Count;
    public int totalRedEnhanceLv38Count;
    public int totalRedEnhanceLv39Count;
    public int totalRedEnhanceLv40Count;

    [Header("BlueEnhance")]
    public int totalBlueEnhanceLv00Count;        // 블루타입의 강화 횟수
    public int totalBlueEnhanceLv01Count;
    public int totalBlueEnhanceLv02Count;
    public int totalBlueEnhanceLv03Count;
    public int totalBlueEnhanceLv04Count;
    public int totalBlueEnhanceLv05Count;
    public int totalBlueEnhanceLv06Count;
    public int totalBlueEnhanceLv07Count;
    public int totalBlueEnhanceLv08Count;
    public int totalBlueEnhanceLv09Count;
    public int totalBlueEnhanceLv10Count;
    public int totalBlueEnhanceLv11Count;
    public int totalBlueEnhanceLv12Count;
    public int totalBlueEnhanceLv13Count;
    public int totalBlueEnhanceLv14Count;
    public int totalBlueEnhanceLv15Count;
    public int totalBlueEnhanceLv16Count;
    public int totalBlueEnhanceLv17Count;
    public int totalBlueEnhanceLv18Count;
    public int totalBlueEnhanceLv19Count;
    public int totalBlueEnhanceLv20Count;
    public int totalBlueEnhanceLv21Count;
    public int totalBlueEnhanceLv22Count;
    public int totalBlueEnhanceLv23Count;
    public int totalBlueEnhanceLv24Count;
    public int totalBlueEnhanceLv25Count;
    public int totalBlueEnhanceLv26Count;
    public int totalBlueEnhanceLv27Count;
    public int totalBlueEnhanceLv28Count;
    public int totalBlueEnhanceLv29Count;
    public int totalBlueEnhanceLv30Count;
    public int totalBlueEnhanceLv31Count;
    public int totalBlueEnhanceLv32Count;
    public int totalBlueEnhanceLv33Count;
    public int totalBlueEnhanceLv34Count;
    public int totalBlueEnhanceLv35Count;
    public int totalBlueEnhanceLv36Count;
    public int totalBlueEnhanceLv37Count;
    public int totalBlueEnhanceLv38Count;
    public int totalBlueEnhanceLv39Count;
    public int totalBlueEnhanceLv40Count;

    [Header("GreenEnhance")]
    public int totalGreenEnhanceLv00Count;       // 그린타입의 강화 횟수
    public int totalGreenEnhanceLv01Count;
    public int totalGreenEnhanceLv02Count;
    public int totalGreenEnhanceLv03Count;
    public int totalGreenEnhanceLv04Count;
    public int totalGreenEnhanceLv05Count;
    public int totalGreenEnhanceLv06Count;
    public int totalGreenEnhanceLv07Count;
    public int totalGreenEnhanceLv08Count;
    public int totalGreenEnhanceLv09Count;
    public int totalGreenEnhanceLv10Count;
    public int totalGreenEnhanceLv11Count;
    public int totalGreenEnhanceLv12Count;
    public int totalGreenEnhanceLv13Count;
    public int totalGreenEnhanceLv14Count;
    public int totalGreenEnhanceLv15Count;
    public int totalGreenEnhanceLv16Count;
    public int totalGreenEnhanceLv17Count;
    public int totalGreenEnhanceLv18Count;
    public int totalGreenEnhanceLv19Count;
    public int totalGreenEnhanceLv20Count;
    public int totalGreenEnhanceLv21Count;
    public int totalGreenEnhanceLv22Count;
    public int totalGreenEnhanceLv23Count;
    public int totalGreenEnhanceLv24Count;
    public int totalGreenEnhanceLv25Count;
    public int totalGreenEnhanceLv26Count;
    public int totalGreenEnhanceLv27Count;
    public int totalGreenEnhanceLv28Count;
    public int totalGreenEnhanceLv29Count;
    public int totalGreenEnhanceLv30Count;
    public int totalGreenEnhanceLv31Count;
    public int totalGreenEnhanceLv32Count;
    public int totalGreenEnhanceLv33Count;
    public int totalGreenEnhanceLv34Count;
    public int totalGreenEnhanceLv35Count;
    public int totalGreenEnhanceLv36Count;
    public int totalGreenEnhanceLv37Count;
    public int totalGreenEnhanceLv38Count;
    public int totalGreenEnhanceLv39Count;
    public int totalGreenEnhanceLv40Count;
    #endregion

    [Header("Gold")]
    public int totalGetGoldEnemy;                 // 총 적 드랍 획득 골드
    public int totalGetGoldSaleTower;             // 총 판매 획득 골드
    public int totalUseGold;                      // 총 사용 골드

    //라운드 도달 횟수
    #region TotalGoalRoundCount
    [Header("Goal 1-10")]
    public int totalGoal1RoundCount;
    public int totalGoal2RoundCount;
    public int totalGoal3RoundCount;
    public int totalGoal4RoundCount;
    public int totalGoal5RoundCount;
    public int totalGoal6RoundCount;
    public int totalGoal7RoundCount;
    public int totalGoal8RoundCount;
    public int totalGoal9RoundCount;
    public int totalGoal10RoundCount;

    [Header("Goal 10-20")]
    public int totalGoal11RoundCount;
    public int totalGoal12RoundCount;
    public int totalGoal13RoundCount;
    public int totalGoal14RoundCount;
    public int totalGoal15RoundCount;
    public int totalGoal16RoundCount;
    public int totalGoal17RoundCount;
    public int totalGoal18RoundCount;
    public int totalGoal19RoundCount;
    public int totalGoal20RoundCount;

    [Header("Goal 20-30")]
    public int totalGoal21RoundCount;
    public int totalGoal22RoundCount;
    public int totalGoal23RoundCount;
    public int totalGoal24RoundCount;
    public int totalGoal25RoundCount;
    public int totalGoal26RoundCount;
    public int totalGoal27RoundCount;
    public int totalGoal28RoundCount;
    public int totalGoal29RoundCount;
    public int totalGoal30RoundCount;

    [Header("Goal 30-40")]
    public int totalGoal31RoundCount;
    public int totalGoal32RoundCount;
    public int totalGoal33RoundCount;
    public int totalGoal34RoundCount;
    public int totalGoal35RoundCount;
    public int totalGoal36RoundCount;
    public int totalGoal37RoundCount;
    public int totalGoal38RoundCount;
    public int totalGoal39RoundCount;
    public int totalGoal40RoundCount;

    [Header("Goal 40-50")]
    public int totalGoal41RoundCount;
    public int totalGoal42RoundCount;
    public int totalGoal43RoundCount;
    public int totalGoal44RoundCount;
    public int totalGoal45RoundCount;
    public int totalGoal46RoundCount;
    public int totalGoal47RoundCount;
    public int totalGoal48RoundCount;
    public int totalGoal49RoundCount;
    public int totalGoal50RoundCount;

    [Header("Goal 50-60")]
    public int totalGoal51RoundCount;
    public int totalGoal52RoundCount;
    public int totalGoal53RoundCount;
    public int totalGoal54RoundCount;
    public int totalGoal55RoundCount;
    public int totalGoal56RoundCount;
    public int totalGoal57RoundCount;
    public int totalGoal58RoundCount;
    public int totalGoal59RoundCount;
    public int totalGoal60RoundCount;
    #endregion

    [Header("Boost")]
    public int totalGetRedBoostCount;             // 부스트 아이템 획득 수
    public int totalGetBlueBoostCount;            // 부스트 아이템 획득 수
    public int totalGetGreenBoostCount;           // 부스트 아이템 획득 수
    public int totalTryBoostCount;                // 부스트 뽑기 횟수

    [Header("Core")]
    public int totalGetCoreCount;                 // 코어 재화 획득 수
    public int totalUseCoreCount;                 // 코어 사용 횟수
    public int totalTryCoreCount;                 // 코어 강화 시도 횟수

    public void Clear()
    {
        totalGameStartCount = 0;
        totalGambleTowerCount = 0;

        totalRedLv1SpawnCount = 0;
        totalRedLv2SpawnCount = 0;
        totalRedLv3SpawnCount = 0;
        totalRedLv4SpawnCount = 0;
        totalRedLv5SpawnCount = 0;
        totalRedLv6SpawnCount = 0;
        totalRedLv7SpawnCount = 0;
        totalRedLv8SpawnCount = 0;
        totalRedLv9SpawnCount = 0;

        totalBlueLv1SpawnCount = 0;
        totalBlueLv2SpawnCount = 0;
        totalBlueLv3SpawnCount = 0;
        totalBlueLv4SpawnCount = 0;
        totalBlueLv5SpawnCount = 0;
        totalBlueLv6SpawnCount = 0;
        totalBlueLv7SpawnCount = 0;
        totalBlueLv8SpawnCount = 0;
        totalBlueLv9SpawnCount = 0;

        totalGreenLv1SpawnCount = 0;
        totalGreenLv2SpawnCount = 0;
        totalGreenLv3SpawnCount = 0;
        totalGreenLv4SpawnCount = 0;
        totalGreenLv5SpawnCount = 0;
        totalGreenLv6SpawnCount = 0;
        totalGreenLv7SpawnCount = 0;
        totalGreenLv8SpawnCount = 0;
        totalGreenLv9SpawnCount = 0;

        totalRedEnhanceLv00Count = 0;
        totalRedEnhanceLv01Count = 0;
        totalRedEnhanceLv02Count = 0;
        totalRedEnhanceLv03Count = 0;
        totalRedEnhanceLv04Count = 0;
        totalRedEnhanceLv05Count = 0;
        totalRedEnhanceLv06Count = 0;
        totalRedEnhanceLv07Count = 0;
        totalRedEnhanceLv08Count = 0;
        totalRedEnhanceLv09Count = 0;
        totalRedEnhanceLv10Count = 0;
        totalRedEnhanceLv11Count = 0;
        totalRedEnhanceLv12Count = 0;
        totalRedEnhanceLv13Count = 0;
        totalRedEnhanceLv14Count = 0;
        totalRedEnhanceLv15Count = 0;
        totalRedEnhanceLv16Count = 0;
        totalRedEnhanceLv17Count = 0;
        totalRedEnhanceLv18Count = 0;
        totalRedEnhanceLv19Count = 0;
        totalRedEnhanceLv20Count = 0;
        totalRedEnhanceLv21Count = 0;
        totalRedEnhanceLv22Count = 0;
        totalRedEnhanceLv23Count = 0;
        totalRedEnhanceLv24Count = 0;
        totalRedEnhanceLv25Count = 0;
        totalRedEnhanceLv26Count = 0;
        totalRedEnhanceLv27Count = 0;
        totalRedEnhanceLv28Count = 0;
        totalRedEnhanceLv29Count = 0;
        totalRedEnhanceLv30Count = 0;
        totalRedEnhanceLv31Count = 0;
        totalRedEnhanceLv32Count = 0;
        totalRedEnhanceLv33Count = 0;
        totalRedEnhanceLv34Count = 0;
        totalRedEnhanceLv35Count = 0;
        totalRedEnhanceLv36Count = 0;
        totalRedEnhanceLv37Count = 0;
        totalRedEnhanceLv38Count = 0;
        totalRedEnhanceLv39Count = 0;
        totalRedEnhanceLv40Count = 0;

        totalBlueEnhanceLv00Count = 0;
        totalBlueEnhanceLv01Count = 0;
        totalBlueEnhanceLv02Count = 0;
        totalBlueEnhanceLv03Count = 0;
        totalBlueEnhanceLv04Count = 0;
        totalBlueEnhanceLv05Count = 0;
        totalBlueEnhanceLv06Count = 0;
        totalBlueEnhanceLv07Count = 0;
        totalBlueEnhanceLv08Count = 0;
        totalBlueEnhanceLv09Count = 0;
        totalBlueEnhanceLv10Count = 0;
        totalBlueEnhanceLv11Count = 0;
        totalBlueEnhanceLv12Count = 0;
        totalBlueEnhanceLv13Count = 0;
        totalBlueEnhanceLv14Count = 0;
        totalBlueEnhanceLv15Count = 0;
        totalBlueEnhanceLv16Count = 0;
        totalBlueEnhanceLv17Count = 0;
        totalBlueEnhanceLv18Count = 0;
        totalBlueEnhanceLv19Count = 0;
        totalBlueEnhanceLv20Count = 0;
        totalBlueEnhanceLv21Count = 0;
        totalBlueEnhanceLv22Count = 0;
        totalBlueEnhanceLv23Count = 0;
        totalBlueEnhanceLv24Count = 0;
        totalBlueEnhanceLv25Count = 0;
        totalBlueEnhanceLv26Count = 0;
        totalBlueEnhanceLv27Count = 0;
        totalBlueEnhanceLv28Count = 0;
        totalBlueEnhanceLv29Count = 0;
        totalBlueEnhanceLv30Count = 0;
        totalBlueEnhanceLv31Count = 0;
        totalBlueEnhanceLv32Count = 0;
        totalBlueEnhanceLv33Count = 0;
        totalBlueEnhanceLv34Count = 0;
        totalBlueEnhanceLv35Count = 0;
        totalBlueEnhanceLv36Count = 0;
        totalBlueEnhanceLv37Count = 0;
        totalBlueEnhanceLv38Count = 0;
        totalBlueEnhanceLv39Count = 0;
        totalBlueEnhanceLv40Count = 0;

        totalGreenEnhanceLv00Count = 0;
        totalGreenEnhanceLv01Count = 0;
        totalGreenEnhanceLv02Count = 0;
        totalGreenEnhanceLv03Count = 0;
        totalGreenEnhanceLv04Count = 0;
        totalGreenEnhanceLv05Count = 0;
        totalGreenEnhanceLv06Count = 0;
        totalGreenEnhanceLv07Count = 0;
        totalGreenEnhanceLv08Count = 0;
        totalGreenEnhanceLv09Count = 0;
        totalGreenEnhanceLv10Count = 0;
        totalGreenEnhanceLv11Count = 0;
        totalGreenEnhanceLv12Count = 0;
        totalGreenEnhanceLv13Count = 0;
        totalGreenEnhanceLv14Count = 0;
        totalGreenEnhanceLv15Count = 0;
        totalGreenEnhanceLv16Count = 0;
        totalGreenEnhanceLv17Count = 0;
        totalGreenEnhanceLv18Count = 0;
        totalGreenEnhanceLv19Count = 0;
        totalGreenEnhanceLv20Count = 0;
        totalGreenEnhanceLv21Count = 0;
        totalGreenEnhanceLv22Count = 0;
        totalGreenEnhanceLv23Count = 0;
        totalGreenEnhanceLv24Count = 0;
        totalGreenEnhanceLv25Count = 0;
        totalGreenEnhanceLv26Count = 0;
        totalGreenEnhanceLv27Count = 0;
        totalGreenEnhanceLv28Count = 0;
        totalGreenEnhanceLv29Count = 0;
        totalGreenEnhanceLv30Count = 0;
        totalGreenEnhanceLv31Count = 0;
        totalGreenEnhanceLv32Count = 0;
        totalGreenEnhanceLv33Count = 0;
        totalGreenEnhanceLv34Count = 0;
        totalGreenEnhanceLv35Count = 0;
        totalGreenEnhanceLv36Count = 0;
        totalGreenEnhanceLv37Count = 0;
        totalGreenEnhanceLv38Count = 0;
        totalGreenEnhanceLv39Count = 0;
        totalGreenEnhanceLv40Count = 0;

        totalGetGoldEnemy = 0;
        totalUseGold = 0;
        totalGetGoldSaleTower = 0;

        totalGoal1RoundCount = 0;
        totalGoal2RoundCount = 0;
        totalGoal3RoundCount = 0;
        totalGoal4RoundCount = 0;
        totalGoal5RoundCount = 0;
        totalGoal6RoundCount = 0;
        totalGoal7RoundCount = 0;
        totalGoal8RoundCount = 0;
        totalGoal9RoundCount = 0;
        totalGoal10RoundCount = 0;

        totalGoal11RoundCount = 0;
        totalGoal12RoundCount = 0;
        totalGoal13RoundCount = 0;
        totalGoal14RoundCount = 0;
        totalGoal15RoundCount = 0;
        totalGoal16RoundCount = 0;
        totalGoal17RoundCount = 0;
        totalGoal18RoundCount = 0;
        totalGoal19RoundCount = 0;
        totalGoal20RoundCount = 0;

        totalGoal21RoundCount = 0;
        totalGoal22RoundCount = 0;
        totalGoal23RoundCount = 0;
        totalGoal24RoundCount = 0;
        totalGoal25RoundCount = 0;
        totalGoal26RoundCount = 0;
        totalGoal27RoundCount = 0;
        totalGoal28RoundCount = 0;
        totalGoal29RoundCount = 0;
        totalGoal30RoundCount = 0;

        totalGoal31RoundCount = 0;
        totalGoal32RoundCount = 0;
        totalGoal33RoundCount = 0;
        totalGoal34RoundCount = 0;
        totalGoal35RoundCount = 0;
        totalGoal36RoundCount = 0;
        totalGoal37RoundCount = 0;
        totalGoal38RoundCount = 0;
        totalGoal39RoundCount = 0;
        totalGoal40RoundCount = 0;

        totalGoal41RoundCount = 0;
        totalGoal42RoundCount = 0;
        totalGoal43RoundCount = 0;
        totalGoal44RoundCount = 0;
        totalGoal45RoundCount = 0;
        totalGoal46RoundCount = 0;
        totalGoal47RoundCount = 0;
        totalGoal48RoundCount = 0;
        totalGoal49RoundCount = 0;
        totalGoal50RoundCount = 0;

        totalGoal51RoundCount = 0;
        totalGoal52RoundCount = 0;
        totalGoal53RoundCount = 0;
        totalGoal54RoundCount = 0;
        totalGoal55RoundCount = 0;
        totalGoal56RoundCount = 0;
        totalGoal57RoundCount = 0;
        totalGoal58RoundCount = 0;
        totalGoal59RoundCount = 0;
        totalGoal60RoundCount = 0;

        totalGetRedBoostCount = 0;  
        totalGetBlueBoostCount = 0;
        totalGetGreenBoostCount = 0;
        totalTryBoostCount = 0;
        
        totalGetCoreCount = 0;
        totalUseCoreCount = 0;
        totalTryCoreCount = 0;
    }
}
#endregion

#region DiaData
[Serializable]
public class SptDiaData
{
    public int curDia;
    public int totalGetDia;
    public int totalUseDia;
}
#endregion

#region ItemCountData
[Serializable]
public class SptItemCountData
{
    [Header("SS")]
    public int item_000;        // 타워 뽑기 금액 감소
    public int item_001;        // 코어 뽑기 금액 감소
    public int item_002;        // 부스트 뽑기 금액 감소

    [Header("S")]
    public int item_100;        // 시작 코어 +
    public int item_101;        // red 강화 비용 감소
    public int item_102;        // blue 강화 비용 감소 
    public int item_103;        // green 강화 비용 감소
    public int item_104;        // 1Lv red 판매금액 up
    public int item_105;        // 1Lv blue 판매금액 up
    public int item_106;        // 1Lv green 판매금액 up

    [Header("A")]
    public int item_200;        // 시작 골드 +
    public int item_201;        // 2Lv red 판매금액 up
    public int item_202;        // 2Lv blue 판매금액 up
    public int item_203;        // 2Lv green 판매금액 up
    public int item_204;        // 3Lv red 판매금액 up
    public int item_205;        // 3Lv blue 판매금액 up
    public int item_206;        // 3Lv green 판매금액 up
                 
    [Header("B")]
    public int item_300;        // 4Lv red 판매금액 up
    public int item_301;        // 4Lv blue 판매금액 up
    public int item_302;        // 4Lv green 판매금액 up
    public int item_303;        // 5Lv red 판매금액 up
    public int item_304;        // 5Lv blue 판매금액 up
    public int item_305;        // 5Lv green 판매금액 up
    public int item_306;        // 6Lv red 판매금액 up
    public int item_307;        // 6Lv blue 판매금액 up
    public int item_308;        // 6Lv green 판매금액 up
    public int item_309;        // 7Lv red 판매금액 up
    public int item_310;        // 7Lv blue 판매금액 up
    public int item_311;        // 7Lv green 판매금액 up
    public int item_312;        // 8Lv red 판매금액 up
    public int item_313;        // 8Lv blue 판매금액 up
    public int item_314;        // 8Lv green 판매금액 up
    public int item_315;        // 9Lv red 판매금액 up
    public int item_316;        // 9Lv blue 판매금액 up
    public int item_317;        // 9Lv green 판매금액 up

    public void ItemLevelUP(int itemID)
    {
        switch (itemID)
        {
            // SS
            case 000:
                item_000++;
                break;
            case 001:
                item_001++;
                break;
            case 002:
                item_002++;
                break;

            // S
            case 100:
                item_100++;
                break;
            case 101:
                item_101++;
                break;
            case 102:
                item_102++;
                break;
            case 103:
                item_103++;
                break;
            case 104:
                item_104++;
                break;
            case 105:
                item_105++;
                break;
            case 106:
                item_106++;
                break;

            // A
            case 200:
                item_200++;
                break;
            case 201:
                item_201++;
                break;
            case 202:
                item_202++;
                break;
            case 203:
                item_203++;
                break;
            case 204:
                item_204++;
                break;
            case 205:
                item_205++;
                break;
            case 206:
                item_206++;
                break;

            // B
            case 300:
                item_300++;
                break;
            case 301:
                item_301++;
                break;
            case 302:
                item_302++;
                break;
            case 303:
                item_303++;
                break;
            case 304:
                item_304++;
                break;
            case 305:
                item_305++;
                break;
            case 306:
                item_306++;
                break;
            case 307:
                item_307++;
                break;
            case 308:
                item_308++;
                break;
            case 309:
                item_309++;
                break;
            case 310:
                item_310++;
                break;
            case 311:
                item_311++;
                break;
            case 312:
                item_312++;
                break;
            case 313:
                item_313++;
                break;
            case 314:
                item_314++;
                break;
            case 315:
                item_315++;
                break;
            case 316:
                item_316++;
                break;
            case 317:
                item_317++;
                break;
        }
    }
}
[Serializable]
public class SptPackagingItemData
{
    public int item_ID;
    public int item_Count;
}
#endregion

#region PurchasePackage
[Serializable]
public class SptPurchasePackageData
{
    public List<SptPackagePurchaseCount> purchaseList;
}
[Serializable]
public class SptPackagePurchaseCount
{
    public int packageID;
    public int purchaseCount;
}
#endregion

#region ADsData
[Serializable]
public class SptADsData
{
    public long adsViewTime;
    public int adsViewCount;
    public bool isJoin;

    public void Clear()
    {
        adsViewTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 1000 * 60 * 15;
        adsViewCount = 0;
        isJoin = false;
    }
}
#endregion

#region SettingData
[Serializable]
public class SptSettingData
{
    // SoundSetting
    public bool isMute;
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public float uiVolume;

    public void Clear()
    {
        isMute = false;
        masterVolume = 20;
        bgmVolume = 20;
        sfxVolume = 20;
        uiVolume = 20;
    }
}
#endregion