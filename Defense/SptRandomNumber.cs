using UnityEngine;

public class SptRandomNumber : MonoBehaviour
{
    private System.Random rngBasic;

    void Start()
    {
        rngBasic = new System.Random();
    }

    void Update()
    {

    }

    public (int level, int type) GetLevelAndType()
    {
        int rLevel = GetRandomLevel();
        int rType = GetRandomType();

        RecordResult(rLevel, rType);

        return (rLevel, rType);
    }

    int GetRandomLevel()
    {
        int Determined = GetRandomNumber();
        int index;
        if (Determined < 1)
        {
            index = 9;
        }
        else if (Determined < 4)
        {
            index = 8;
        }
        else if (Determined < 14)
        {
            index = 7;
        }
        else if (Determined < 44)
        {
            index   = 6;
        }
        else if (Determined < 144)
        {
            index = 5;
        }
        else if (Determined < 444)
        {
            index = 4;
        }
        else if (Determined < 1444)
        {
            index = 3;
        }
        else if (Determined < 4444)
        {
            index = 2;
        }
        else
        {
            index = 1;
        }

        return index;
    }

    int GetRandomType()
    {
        float rType = (float)rngBasic.NextDouble();

        int dType;

        if (rType < 0.333)
        {
            dType = 2;
        }
        else if (rType < 0.666)
        {
            dType = 1;
        }
        else
        {
            dType = 0;
        }

        return dType;
    }

    public int GetRandomNumber()
    {
        float rNumA = (float)rngBasic.NextDouble() * 10;
        float rNumB = (float)rngBasic.NextDouble() * 10;
        float rNumC = (float)rngBasic.NextDouble() * 10;
        float rNumD = (float)rngBasic.NextDouble() * 10;

        int numA = Mathf.FloorToInt(rNumA);
        int numB = Mathf.FloorToInt(rNumB);
        int numC = Mathf.FloorToInt(rNumC);
        int numD = Mathf.FloorToInt(rNumD);

        int determinedNum = numA + numB * 10 + numC * 100 + numD * 1000;

        return determinedNum;
    }

    void RecordResult(int level, int type)
    {
        // 레벨, 타입, 레벨 및 타입 으로 구분되어 기록되는 함수 추가

        // 시도 횟수 기록
    }
}
