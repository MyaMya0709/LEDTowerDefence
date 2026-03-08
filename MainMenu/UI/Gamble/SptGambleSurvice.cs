using UnityEngine;

public class SptGambleSurvice : MonoBehaviour
{
    public int gambleCost = 50;

    public (bool, int) ItemGamble()
    {
        if (SptDataManager.instance.curDia > gambleCost) // 다이아 충분하면 뽑기
        {
            Debug.Log("ItemGamble!");

            // 다이아 소모 및 다이아 데이터 저장
            SptDataManager.instance.UseDia(gambleCost);

            // 아이텝 뽑기 및 뽑은 아이템 카운팅, 저장
            float ranBaseNum = Random.value;
            int itemID = 0;
            if (ranBaseNum > 32f / 35f)
            {
                Debug.Log("Get SS Item");
                float ranNum = Random.value;
                if (ranNum > 2f / 3f) itemID = 000;
                else if (ranNum > 1f / 3f) itemID = 001;
                else itemID = 002;
            }
            else if (ranBaseNum > 25f / 35f)
            {
                Debug.Log("Get S Item");
                float ranNum = Random.value;
                if (ranNum > 6f / 7f) itemID = 100;
                else if (ranNum > 5f / 7f) itemID = 101;
                else if (ranNum > 4f / 7f) itemID = 102;
                else if (ranNum > 3f / 7f) itemID = 103;
                else if (ranNum > 2f / 7f) itemID = 104;
                else if (ranNum > 1f / 7f) itemID = 105;
                else itemID = 106;
            }
            else if (ranBaseNum > 18f / 35f)
            {
                Debug.Log("Get A Item");
                float ranNum = Random.value;
                if (ranNum > 6f / 7f) itemID = 200;
                else if (ranNum > 5f / 7f) itemID = 201;
                else if (ranNum > 4f / 7f) itemID = 202;
                else if (ranNum > 3f / 7f) itemID = 203;
                else if (ranNum > 2f / 7f) itemID = 204;
                else if (ranNum > 1f / 7f) itemID = 205;
                else itemID = 206;
            }
            else
            {
                Debug.Log("Get B Item");
                float ranNum = Random.value;
                if (ranNum > 17f / 18f) itemID = 300;
                else if (ranNum > 16f / 18f) itemID = 301;
                else if (ranNum > 15f / 18f) itemID = 302;
                else if (ranNum > 14f / 18f) itemID = 303;
                else if (ranNum > 13f / 18f) itemID = 304;
                else if (ranNum > 12f / 18f) itemID = 305;
                else if (ranNum > 11f / 18f) itemID = 306;
                else if (ranNum > 10f / 18f) itemID = 307;
                else if (ranNum > 9f / 18f) itemID = 308;
                else if (ranNum > 8f / 18f) itemID = 309;
                else if (ranNum > 7f / 18f) itemID = 310;
                else if (ranNum > 6f / 18f) itemID = 311;
                else if (ranNum > 5f / 18f) itemID = 312;
                else if (ranNum > 4f / 18f) itemID = 313;
                else if (ranNum > 3f / 18f) itemID = 314;
                else if (ranNum > 2f / 18f) itemID = 315;
                else if (ranNum > 1f / 18f) itemID = 316;
                else itemID = 317;
            }
            SptDataManager.instance.ItemLevelUP(itemID);

            // 서버로 데이터 업로드
            if (SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.SaveItemGambleData();

            return (true, itemID);
        }
        else // 다이아가 부족하면 뽑기 불가능
        {
            Debug.Log("Dia Not Enough");
            return (false, gambleCost - SptDataManager.instance.curDia);
        }
    }
}
