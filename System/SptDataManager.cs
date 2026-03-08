using System;
using System.Collections.Generic;
using UnityEngine;

public class SptDataManager : SptSingleton<SptDataManager>
{
    public SptDiaData diaData;
    public SptItemCountData itemDatas;
    public SptPurchasePackageData packageDatas;

    [Header("DiaData")]
    public int curDia;
    public int totalGetDia;
    public int totalUseDia;

    #region ItemCountData
    [Header("SS")]
    public int item_000;
    public int item_001;
    public int item_002;

    [Header("S")]
    public int item_100;
    public int item_101;
    public int item_102;
    public int item_103;
    public int item_104;
    public int item_105;
    public int item_106;

    [Header("A")]
    public int item_200;
    public int item_201;
    public int item_202;
    public int item_203;
    public int item_204;
    public int item_205;
    public int item_206;

    [Header("B")]
    public int item_300;
    public int item_301;
    public int item_302;
    public int item_303;
    public int item_304;
    public int item_305;
    public int item_306;
    public int item_307;
    public int item_308;
    public int item_309;
    public int item_310;
    public int item_311;
    public int item_312;
    public int item_313;
    public int item_314;
    public int item_315;
    public int item_316;
    public int item_317;
    #endregion

    [Header("PurchasePackage")]
    public List<SptPackagePurchaseCount> purchaseCountList;

    [Header("ADsData")]
    public SptADsData adsData;
    public long adsViewTime;
    public int adsViewCount;
    public bool isJoin;

    public void DataLoad()
    {
        DiaDataLoad();
        ItemDatasLoad();
        PackageDatasLoad();
        ADsDataLoad();
    }
    public void DataSetting()
    {
        DiaDataSetting();
        ItemDatasSetting();
        PackageDatasSetting();
        ADsDataSetting();
    }

    #region DiaData
    public void DiaDataLoad()
    {
        diaData = SptSaveManager.instance.diaData;
        DiaDataSetting();
    }
    public void DiaDataSetting()
    {
        curDia = diaData.curDia;
        totalGetDia = diaData.totalGetDia;
        totalUseDia = diaData.totalUseDia;
    }
    public void UpdateDiaData()
    {
        diaData.curDia = curDia;
        diaData.totalGetDia = totalGetDia;
        diaData.totalUseDia = totalUseDia;

        SptSaveManager.instance.GetDiaData(diaData);
    }

    public void GetDiaToDefence(int stage, bool isClear)
    {
        DiaDataLoad();

        int get = 0;

        // 게임 클리어시 획득 재화
        if (isClear) get = 200;
        else
        {
            if (stage > 60) get = 130;           // 60보스 클리어 획득재화
            else if (stage > 50) get = 75;       // 50보스 클리어 획득재화
            else if (stage > 40) get = 40;       // 40보스 클리어 획득재화
            else if (stage > 30) get = 20;       // 30보스 클리어 획득재화
            else if (stage > 20) get = 10;       // 20보스 클리어 획득재화
            else if (stage > 10) get = 5;        // 10보스 클리어 획득재화
        }

        curDia += get;
        totalGetDia += get;

        UpdateDiaData();
    }
    public void GetDiaToPurchase(int get)
    {
        DiaDataLoad();

        curDia += get;
        totalGetDia += get;
        
        UpdateDiaData();
    }
    public void UseDia(int use)
    {
        DiaDataLoad();

        curDia -= use;
        totalUseDia += use;

        UpdateDiaData();
    }

    #endregion

    #region ItemData
    public void ItemDatasLoad()
    {
        itemDatas = SptSaveManager.instance.itemDatas;
        ItemDatasSetting();
    }
    public void ItemDatasSetting()
    {
        item_000 = itemDatas.item_000;
        item_001 = itemDatas.item_001;
        item_002 = itemDatas.item_002;

        item_100 = itemDatas.item_100;
        item_101 = itemDatas.item_101;
        item_102 = itemDatas.item_102;
        item_103 = itemDatas.item_103;
        item_104 = itemDatas.item_104;
        item_105 = itemDatas.item_105;
        item_106 = itemDatas.item_106;

        item_200 = itemDatas.item_200;
        item_201 = itemDatas.item_201;
        item_202 = itemDatas.item_202;
        item_203 = itemDatas.item_203;
        item_204 = itemDatas.item_204;
        item_205 = itemDatas.item_205;
        item_206 = itemDatas.item_206;

        item_300 = itemDatas.item_300;
        item_301 = itemDatas.item_301;
        item_302 = itemDatas.item_302;
        item_303 = itemDatas.item_303;
        item_304 = itemDatas.item_304;
        item_305 = itemDatas.item_305;
        item_306 = itemDatas.item_306;
        item_307 = itemDatas.item_307;
        item_308 = itemDatas.item_308;
        item_309 = itemDatas.item_309;
        item_310 = itemDatas.item_310;
        item_311 = itemDatas.item_311;
        item_312 = itemDatas.item_312;
        item_313 = itemDatas.item_313;
        item_314 = itemDatas.item_314;
        item_315 = itemDatas.item_315;
        item_316 = itemDatas.item_316;
        item_317 = itemDatas.item_317;
    }
    public void UpDateItemDatas()
    {
        itemDatas.item_000 = item_000;
        itemDatas.item_001 = item_001;
        itemDatas.item_002 = item_002;

        itemDatas.item_100 = item_100;
        itemDatas.item_101 = item_101;
        itemDatas.item_102 = item_102;
        itemDatas.item_103 = item_103;
        itemDatas.item_104 = item_104;
        itemDatas.item_105 = item_105;
        itemDatas.item_106 = item_106;

        itemDatas.item_200 = item_200;
        itemDatas.item_201 = item_201;
        itemDatas.item_202 = item_202;
        itemDatas.item_203 = item_203;
        itemDatas.item_204 = item_204;
        itemDatas.item_205 = item_205;
        itemDatas.item_206 = item_206;

        itemDatas.item_300 = item_300;
        itemDatas.item_301 = item_301;
        itemDatas.item_302 = item_302;
        itemDatas.item_303 = item_303;
        itemDatas.item_304 = item_304;
        itemDatas.item_305 = item_305;
        itemDatas.item_306 = item_306;
        itemDatas.item_307 = item_307;
        itemDatas.item_308 = item_308;
        itemDatas.item_309 = item_309;
        itemDatas.item_310 = item_310;
        itemDatas.item_311 = item_311;
        itemDatas.item_312 = item_312;
        itemDatas.item_313 = item_313;
        itemDatas.item_314 = item_314;
        itemDatas.item_315 = item_315;
        itemDatas.item_316 = item_316;
        itemDatas.item_317 = item_317;

        SptSaveManager.instance.GetItemData(itemDatas);
    }
    public SptItemCountData ReadItemDatas()
    {
        ItemDatasLoad();

        SptItemCountData data = new SptItemCountData();

        data.item_000 = item_000;
        data.item_001 = item_001;
        data.item_002 = item_002;

        data.item_100 = item_100;
        data.item_101 = item_101;
        data.item_102 = item_102;
        data.item_103 = item_103;
        data.item_104 = item_104;
        data.item_105 = item_105;
        data.item_106 = item_106;

        data.item_200 = item_200;
        data.item_201 = item_201;
        data.item_202 = item_202;
        data.item_203 = item_203;
        data.item_204 = item_204;
        data.item_205 = item_205;
        data.item_206 = item_206;

        data.item_300 = item_300;
        data.item_301 = item_301;
        data.item_302 = item_302;
        data.item_303 = item_303;
        data.item_304 = item_304;
        data.item_305 = item_305;
        data.item_306 = item_306;
        data.item_307 = item_307;
        data.item_308 = item_308;
        data.item_309 = item_309;
        data.item_310 = item_310;
        data.item_311 = item_311;
        data.item_312 = item_312;
        data.item_313 = item_313;
        data.item_314 = item_314;
        data.item_315 = item_315;
        data.item_316 = item_316;
        data.item_317 = item_317;

        return data;
    }

    public int GetItemLevel(int itemID)
    {
        int ret;

        switch (itemID)
        {
            // SS
            case 000:
                ret = item_000;
                break;
            case 001:
                ret = item_001;
                break;
            case 002:
                ret = item_002;
                break;

            // S
            case 100:
                ret = item_100;
                break;
            case 101:
                ret = item_101;
                break;
            case 102:
                ret = item_102;
                break;
            case 103:
                ret = item_103;
                break;
            case 104:
                ret = item_104;
                break;
            case 105:
                ret = item_105;
                break;
            case 106:
                ret = item_106;
                break;

            // A
            case 200:
                ret = item_200;
                break;
            case 201:
                ret = item_201;
                break;
            case 202:
                ret = item_202;
                break;
            case 203:
                ret = item_203;
                break;
            case 204:
                ret = item_204;
                break;
            case 205:
                ret = item_205;
                break;
            case 206:
                ret = item_206;
                break;

            // B
            case 300:
                ret = item_300;
                break;
            case 301:
                ret = item_301;
                break;
            case 302:
                ret = item_302;
                break;
            case 303:
                ret = item_303;
                break;
            case 304:
                ret = item_304;
                break;
            case 305:
                ret = item_305;
                break;
            case 306:
                ret = item_306;
                break;
            case 307:
                ret = item_307;
                break;
            case 308:
                ret = item_308;
                break;
            case 309:
                ret = item_309;
                break;
            case 310:
                ret = item_310;
                break;
            case 311:
                ret = item_311;
                break;
            case 312:
                ret = item_312;
                break;
            case 313:
                ret = item_313;
                break;
            case 314:
                ret = item_314;
                break;
            case 315:
                ret = item_315;
                break;
            case 316:
                ret = item_316;
                break;
            case 317:
                ret = item_317;
                break;

            default:
                ret = -1;
                break;
        }

        return ret;
    }
    public void ItemLevelUP(int itemID)
    {
        ItemDatasLoad();

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

        UpDateItemDatas();
    }
    public void PurchaseItem(SptItemCountData data)
    {
        ItemDatasLoad();

        item_000 += data.item_000;
        item_001 += data.item_001;
        item_002 += data.item_002;

        item_100 += data.item_100;
        item_101 += data.item_101;
        item_102 += data.item_102;
        item_103 += data.item_103;
        item_104 += data.item_104;
        item_105 += data.item_105;
        item_106 += data.item_106;

        item_200 += data.item_200;
        item_201 += data.item_201;
        item_202 += data.item_202;
        item_203 += data.item_203;
        item_204 += data.item_204;
        item_205 += data.item_205;
        item_206 += data.item_206;

        item_300 += data.item_300;
        item_301 += data.item_301;
        item_302 += data.item_302;
        item_303 += data.item_303;
        item_304 += data.item_304;
        item_305 += data.item_305;
        item_306 += data.item_306;
        item_307 += data.item_307;
        item_308 += data.item_308;
        item_309 += data.item_309;
        item_310 += data.item_310;
        item_311 += data.item_311;
        item_312 += data.item_312;
        item_313 += data.item_313;
        item_314 += data.item_314;
        item_315 += data.item_315;
        item_316 += data.item_316;
        item_317 += data.item_317;

        UpDateItemDatas();
    }

    #endregion

    #region packageData
    public void PackageDatasLoad()
    {
        packageDatas = SptSaveManager.instance.packageDatas;
        PackageDatasSetting();
    }
    public void PackageDatasSetting()
    {
        purchaseCountList = packageDatas.purchaseList;
    }
    public void UpdatePurchasePackageData()
    {
        packageDatas.purchaseList = purchaseCountList;

        SptSaveManager.instance.GetPackageData(packageDatas);
    }
    public void PurchasePackage(int packageID)
    {
        PackageDatasLoad();

        // 추가여부 체크용 변수
        bool isAdd = true;

        // 구매 내역 확인
        for (int i = 0; i < purchaseCountList.Count; i++)
        {
            // 구매 내역에 아이디가 있으면 아이디 추가 X
            if (purchaseCountList[i].packageID == packageID)
            {
                isAdd = false;
                // 구입 횟수 카운팅
                purchaseCountList[i].purchaseCount++;
            }
        }

        // 구매한 패키지 아이디 추가
        if (isAdd)
        {
            SptPackagePurchaseCount countData = new SptPackagePurchaseCount() { packageID = packageID , purchaseCount = 1};
            purchaseCountList.Add(countData);
        }

        UpdatePurchasePackageData();
    }
    #endregion

    #region ADsData
    public void ADsDataLoad()
    {
        adsData = SptSaveManager.instance.adsData;
        ADsDataSetting();
    }
    public void ADsDataSetting()
    {
        adsViewTime = adsData.adsViewTime;
        adsViewCount = adsData.adsViewCount;
        isJoin = adsData.isJoin;
    }
    public void UpdateADsData()
    {
        adsData.adsViewTime = adsViewTime;
        adsData.adsViewCount = adsViewCount;
        adsData.isJoin = isJoin;

        SptSaveManager.instance.GetADsData(adsData);
    }

    public void CountingADsView(bool isTimeCheck)
    {
        ADsDataLoad();

        if (isTimeCheck) adsViewTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        adsViewCount++;

        UpdateADsData();
    }
    public void JoinADsContract()
    {
        ADsDataLoad();

        isJoin = true;

        UpdateADsData();
    }
    public void TerminationADsContract()
    {
        ADsDataLoad();

        isJoin = false;

        UpdateADsData();
    }
    #endregion

}
