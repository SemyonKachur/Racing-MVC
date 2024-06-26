using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Analytics;

namespace Services.IAP
{
    internal class IAPService : IStoreListener
    {
        [Header("Components")]
        private ProductLibrary productLibrary;

        private ResourcePath _path = new ResourcePath("ScriptableObjects/ProductLibrary");

        private static IAPService iapService;

        [field: Header("Events")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseSucceed { get; private set; }
        [field: SerializeField] public UnityEvent PurchaseFailed { get; private set; }

        private bool _isInitialized;
        private IStoreController _controller;
        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _purchaseValidator;
        private PurchaseRestorer _purchaseRestorer;


        public IAPService()
        {
            iapService = this;
            productLibrary = ResourcesLoader.LoadProductLibrary(_path);
            InitializeProducts();
            OnInitialized(this._controller,this._extensionProvider);
        }

        public static IAPService GetIAPService()
        {
            if (iapService == null) iapService = new IAPService();
            return iapService;
        }
           
        private void InitializeProducts()
        {
            StandardPurchasingModule purchasingModule = StandardPurchasingModule.Instance();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(purchasingModule);

            foreach (Product product in productLibrary.Products)
                builder.AddProduct(product.Id, product.ProductType);
            UnityPurchasing.Initialize(this,builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensionsProvider)
        {
            _isInitialized = true;
            _controller = controller;
            _extensionProvider = extensionsProvider;
            _purchaseValidator = new PurchaseValidator();
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            Log("Initialized");
            Initialized?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _isInitialized = false;
            Error("Initialization Failed");
        }


        public void Buy(string id)
        {
            if (!_isInitialized)
            {
                Error($"Buy {id} FAIL. Not initialized.");
                return;
            }

            _controller.InitiatePurchase(id);
            Log("Buying sucsess");
            UnityEngine.Analytics.Analytics.Transaction(id, 1, "RUR");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (_purchaseValidator.Validate(args) == false)
            {
                OnPurchaseFailed(args.purchasedProduct.definition.id, "NonValid");
                return PurchaseProcessingResult.Complete;
            }

            PurchaseSucceed.Invoke();
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason) =>
            OnPurchaseFailed(product.definition.id, failureReason.ToString());

        private void OnPurchaseFailed(string productId, string reason)
        {
            Error($"Failed {productId}: {reason}");
            PurchaseFailed?.Invoke();
        }


        public string GetCost(string productID)
        {
            UnityEngine.Purchasing.Product product = _controller.products.WithID(productID);

            if (product != null)
                return product.metadata.localizedPriceString;

            return "N/A";
        }

        public void RestorePurchases()
        {
            if (!_isInitialized)
            {
                Error("RestorePurchases FAIL. Not initialized.");
                return;
            }

            _purchaseRestorer.Restore();
        }


        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
