using UnityEngine;
using UnityEngine.Purchasing;

public class SptPackageIAP : MonoBehaviour
{
    public SptPaymentPopup payPopup;
    public SptLoadingPopup loadingPopup;
    public SptSuccessPopup successPopup;
    public SptGetResultPopup resultPopup;
    public SptFailPopup failPopup;
    public SptApprovalWaitPopup approvalWaitPopup;

    public SptPackageData purchasePackageData;

    public SptStoreAnimator animator;

    #region IAPButton
    // 제품 정보 불러오기 (ID,Price)
    public void OnProductFetched(Product product)
    {
        Debug.Log("OnProductFetched");
        Debug.Log($"{product.definition.id}, price: {product.metadata.localizedPriceString}");
    }
    // 제품 정보 불러오기 실패
    public void OnProductsFetchFailed(ProductDefinition productDefinition, string reason)
    {
        Debug.Log("OnProductsFetchFailed");
        Debug.Log($"실패한 제품의 아이디 : {productDefinition.id}");
        Debug.Log($"Products fetch failed: {reason}");

        payPopup.OnClose();
        // 결제 실패
        failPopup.OpenPopup();
    }
    // 복원 및 과거 구매 불러오기 완료?
    public void OnPurchasesFetched(Order order)
    {
        Debug.Log("OnPurchasesFetched");
        // 확인된 주문 처리
    }
    // 구매가 보류 상태 (Pending)
    public void OnOrderPending(PendingOrder pendingOrder)
    {
        Debug.Log("OnOrderPending");

        // 서버로 영수증 전송 / 유저에게 '결제 처리 중' UI 보여주기
        loadingPopup.OpenPopup();
    }
    // 구매 확정됨 (Confirmed)
    public void OnOrderConfirmed(ConfirmedOrder confirmedOrder)
    {
        Debug.Log("OnOrderConfirmed");

        payPopup.OnClose();
        loadingPopup.OnClose();
        // 결제 성공
        //successPopup.OpenPopup();

        // 패키지 서버 데이터 로드
        if (purchasePackageData != null)
        {
            // 애니메이션 시작
            animator.AnimationStart(purchasePackageData);

            if (purchasePackageData.packageID >= 100 && SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadPurchasePackageData();
            else if (purchasePackageData.packageID >= 100 && !SptGameManager.instance.isRecord) SptGameManager.instance.LoadPurchasePackageNext();

            else if (purchasePackageData.packageID < 100 && SptGameManager.instance.isRecord) SptGooglePlayGameServices.instance.LoadPurchaseDiaData();
            else if (purchasePackageData.packageID < 100 && !SptGameManager.instance.isRecord) SptGameManager.instance.LoadPurchaseDiaNext();
        }
        else Debug.Log("purchasePackageData Null!");
    }
    // 구매 실패
    public void OnPurchaseFailed(FailedOrder failedOrder)
    {
        Debug.Log("OnPurchaseFailed");
        Debug.LogError($"Purchase failed: {failedOrder.FailureReason} for product {failedOrder.Info.PurchasedProductInfo[0].productId}");

        payPopup.OnClose();
        loadingPopup.OnClose();
        // 사용자에게 실패 메시지 보여주기
        failPopup.OpenPopup();
    }
    // 구매 연기됨
    public void OnOrderDeferred(DeferredOrder deferredOrder)
    {
        Debug.Log("OnOrderDeferred");
        Debug.Log($"Purchase product : {deferredOrder.Info.PurchasedProductInfo[0].productId}");

        payPopup.OnClose();
        loadingPopup.OnClose();
        // 유저에게 연기 상태 안내 UI 띄우기
        approvalWaitPopup.OpenPopup();
    }
    #endregion


    #region 패키지 구입 reward / UISet
    public void PurchasePackageSetting()
    {
        SptItemCountData itemCountData = new SptItemCountData();
        foreach (var item in purchasePackageData.itemDatas) itemCountData.ItemLevelUP(item.itemID);

        // 아이템,다이아,패키지 업데이트
        SptDataManager.instance.PurchaseItem(itemCountData);
        SptDataManager.instance.GetDiaToPurchase(purchasePackageData.saleDia);
        SptDataManager.instance.PurchasePackage(purchasePackageData.packageID);
    }

    public void OpenPackageResultPopup()
    {
        resultPopup.SetPurchasePackage(purchasePackageData);
    }
    public void OpenDiaResultPopup()
    {
        resultPopup.SetGetDia(purchasePackageData.saleDia);
    }
    #endregion
}
