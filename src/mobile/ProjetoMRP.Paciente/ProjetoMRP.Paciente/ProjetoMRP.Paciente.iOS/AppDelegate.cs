using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using HealthKit;
using System.Threading.Tasks;

namespace ProjetoMRP.Paciente.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
            LoadApplication(new App());
            HeartRateModel hrm = HeartRateModel.Instance;
            //hrm.GetSteps();
            hrm.GetSpO2();

            hrm.GetRatesForType(HKQuantityTypeIdentifier.StepCount);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.HeartRate);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.BloodGlucose);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.WalkingHeartRateAverage);

            return base.FinishedLaunching(app, options);
        }

        private HKHealthStore healthKitStore = new HKHealthStore();

        public override void OnActivated(UIApplication application)
        {
            ValidateAuthorization();
        }

        private void ValidateAuthorization()
        {
            var heartRateId = HKQuantityTypeIdentifierKey.HeartRate;
            var heartRateType = HKObjectType.GetQuantityType(heartRateId);
            var bloodGlucoseId = HKQuantityTypeIdentifierKey.BloodGlucose;
            var bloodGlucoseType = HKObjectType.GetQuantityType(bloodGlucoseId);
            var bloodPressureDiastolicId = HKQuantityTypeIdentifierKey.BloodPressureDiastolic;
            var bloodPressureDiastolicType = HKObjectType.GetQuantityType(bloodPressureDiastolicId);
            var bloodPressureSystolicId = HKQuantityTypeIdentifierKey.BloodPressureSystolic;
            var bloodPressureSystolicType = HKObjectType.GetQuantityType(bloodPressureSystolicId);
            var oxygenSaturationId = HKQuantityTypeIdentifierKey.OxygenSaturation;
            var oxygenSaturationType = HKObjectType.GetQuantityType(oxygenSaturationId);
            var respiratoryRateId = HKQuantityTypeIdentifierKey.RespiratoryRate;
            var respiratoryRateType = HKObjectType.GetQuantityType(respiratoryRateId);
            var restingHeartRateId = HKQuantityTypeIdentifierKey.RestingHeartRate;
            var restingHeartRateType = HKObjectType.GetQuantityType(restingHeartRateId);
            var stepCountId = HKQuantityTypeIdentifierKey.StepCount;
            var stepCountType = HKObjectType.GetQuantityType(stepCountId);

            var typesToRead = new NSSet(new[]
            {
                heartRateType,
                bloodGlucoseType,
                bloodPressureDiastolicType,
                bloodPressureSystolicType,
                oxygenSaturationType,
                respiratoryRateType,
                restingHeartRateType,
                stepCountType
,            });

            var typesToWrite = new NSSet();

            healthKitStore.RequestAuthorizationToShare(
                    typesToWrite,
                    typesToRead,
                    ReactToHealthCarePermissions);
        }

        void ReactToHealthCarePermissions(bool success, NSError error)
        {
            var access = healthKitStore.GetAuthorizationStatus(HKObjectType.GetQuantityType(HKQuantityTypeIdentifierKey.HeartRate));
            if (access.HasFlag(HKAuthorizationStatus.SharingAuthorized))
            {
                HeartRateModel.Instance.Enabled = true;
            }
            else
            {
                HeartRateModel.Instance.Enabled = false;
            }
        }


        public override void DidEnterBackground(UIApplication application)
        {
            Console.WriteLine("App entering background state.");

            nint taskID = 0;
            // if you're creating a VOIP application, this is how you set the keep alive
            //UIApplication.SharedApplication.SetKeepAliveTimout(600, () => { /* keep alive handler code*/ });

            // register a long running task, and then start it on a new thread so that this method can return
            taskID = UIApplication.SharedApplication.BeginBackgroundTask(() =>
            {
                Console.WriteLine("Running out of time to complete you background task!");
                UIApplication.SharedApplication.EndBackgroundTask(taskID);
            });

            Task.Factory.StartNew(() => FinishLongRunningTask(taskID));
        }

        private void FinishLongRunningTask(nint taskID)
        {
            HeartRateModel hrm = HeartRateModel.Instance;
            //hrm.GetSteps();
            hrm.GetSpO2();

            hrm.GetRatesForType(HKQuantityTypeIdentifier.StepCount);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.HeartRate);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.BloodGlucose);
            hrm.GetRatesForType(HKQuantityTypeIdentifier.WalkingHeartRateAverage);

            Console.WriteLine($"Starting task {taskID}");
            Console.WriteLine($"Background time remaining: {UIApplication.SharedApplication.BackgroundTimeRemaining}");

            // sleep for 15 seconds to simulate a long running task
            System.Threading.Thread.Sleep(15000);

            Console.WriteLine($"Task {taskID} finished");
            Console.WriteLine($"Background time remaining: {UIApplication.SharedApplication.BackgroundTimeRemaining}");

            // call our end task
            UIApplication.SharedApplication.EndBackgroundTask(taskID);
        }
    }
}
