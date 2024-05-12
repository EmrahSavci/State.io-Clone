
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEditor;
using System;

public class MyIAPManager :MonoBehaviour, IStoreListener
{

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    private static Product test_product = null;


    public static string GOLD_50 = "removeads";
    public static string NO_ADS = "com.shiftspacegames.ballwars.removeads";
    public static string SUB1 = "subscription1";

   

    private Boolean return_complete = true;

    void Start()
    {

        NO_ADS = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? "com.shiftspacegames.ballwars.removeads"
            : "com.shiftspacegame.ballwarsio.removeads";
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        MyDebug("Complete = " + return_complete.ToString());
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

       
        builder.AddProduct(NO_ADS, ProductType.NonConsumable);
       

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuySubscription()
    {
        BuyProductID(SUB1);
    }

    public void BuyGold50()
    {
        BuyProductID(GOLD_50);
    }

    public void BuyNoAds()
    {
        BuyProductID(NO_ADS);
    }

    public void CompletePurchase()
    {
        if (test_product == null)
            MyDebug("Cannot complete purchase, product not initialized.");
        else
        {
            m_StoreController.ConfirmPendingPurchase(test_product);
            MyDebug("Completed purchase with " + test_product.transactionID.ToString());
        }

    }

    public void ToggleComplete()
    {
        return_complete = !return_complete;
        MyDebug("Complete = " + return_complete.ToString());

    }
    public void RestorePurchases()
    {
        m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(result => {
            if (result)
            {
                MyDebug("Restore purchases succeeded.");
            }
            else
            {
                MyDebug("Restore purchases failed.");
            }
        });
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                
                MyDebug(string.Format("Purchasing product:" + product.definition.id.ToString()));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                MyDebug("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            MyDebug("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        MyDebug("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        MyDebug("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        test_product = args.purchasedProduct;



        //MyDebug(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));

        if (return_complete)
        {
            PlayerPrefs.SetInt(GameData.Instante.RewardAds, 1);
            MyDebug(string.Format("ProcessPurchase: Complete. Product:" + args.purchasedProduct.definition.id + " - " + test_product.transactionID.ToString()));
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            MyDebug(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id + " - " + test_product.transactionID.ToString()));
            return PurchaseProcessingResult.Pending;
        }

    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        MyDebug(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    private void MyDebug(string debug)
    {

        Debug.Log(debug);
       
    }
}
