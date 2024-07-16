using OneSignalSDK;
using UnityEngine;

namespace Core
{
    public class OneSignalInitialize : MonoBehaviour
    {
        private async void Start () 
        {
#if UNITY_IOS
            OneSignalSDK.OneSignal.Initialize(Settings.OneSignalAppID());
        
            //OneSignalSDK.OneSignal.Default.PromptForPushNotificationsWithUserResponse();
            var result = await OneSignal.Notifications.RequestPermissionAsync(true);
#else
            Debug.LogError($"Initializing error, OS != IOS");
#endif
        }
    }
}