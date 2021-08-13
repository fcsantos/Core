using System;
using HealthKit;
using Foundation;

namespace ProjetoMRP.Paciente.iOS
{

	public class GenericEventArgs<T> : EventArgs
	{
		public T Value { get; protected set; }

		public DateTime Time { get; protected set; }

		public GenericEventArgs(T value)
		{
			this.Value = value;
			Time = DateTime.Now;
		}
	}

	public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> args);

	public sealed class HeartRateModel : NSObject
	{
		//Thread-safe singleton: Overkill for the sample app, but proper design
		private static volatile HeartRateModel singleton;
		private static object syncRoot = new Object();

		private HeartRateModel()
		{
		}

		public static HeartRateModel Instance
		{
			get
			{
				//Double-check lazy initialization
				if (singleton == null)
				{
					lock (syncRoot)
					{
						if (singleton == null)
						{
							singleton = new HeartRateModel();
						}
					}
				}

				return singleton;
			}
		}

		private bool enabled = false;

		public event GenericEventHandler<bool> EnabledChanged;
		public event GenericEventHandler<String> ErrorMessageChanged;
		public event GenericEventHandler<Double> HeartRateStored;

		internal void GetSteps()
		{
			var healthKitStore = new HKHealthStore();
			var stepRateType = HKQuantityType.Create(HKQuantityTypeIdentifier.StepCount);
			var sort = new NSSortDescriptor(HKSample.SortIdentifierStartDate, true);
			var q = new HKSampleQuery(stepRateType, HKQuery.GetPredicateForSamples(NSDate.Now.AddSeconds(TimeSpan.FromDays(-365).TotalSeconds), NSDate.Now.AddSeconds(TimeSpan.FromDays(1).TotalSeconds), HKQueryOptions.None), 0, new NSSortDescriptor[] { },
				new HKSampleQueryResultsHandler((HKSampleQuery query2, HKSample[] results, NSError error2) =>
				{
					var query = results; //property created within the model to expose later.
					Console.WriteLine(query);
					Console.WriteLine(results);

					foreach (var item in results)
					{
						var sample = (HKQuantitySample)item;
						var hkUnit = HKUnit.Count;
						var quantity = sample.Quantity.GetDoubleValue(hkUnit);
						var startDateTime = sample.StartDate;
						var endDateTime = sample.EndDate;
						Console.WriteLine(quantity);
						Console.WriteLine(startDateTime);
						Console.WriteLine(endDateTime);
					}
				}));
			healthKitStore.ExecuteQuery(q);
		}

		internal void GetSpO2()
		{
			var healthKitStore = new HKHealthStore();
			var rateType = HKQuantityType.Create(HKQuantityTypeIdentifier.OxygenSaturation);
			var sort = new NSSortDescriptor(HKSample.SortIdentifierStartDate, true);
			var q = new HKSampleQuery(rateType, HKQuery.GetPredicateForSamples(NSDate.Now.AddSeconds(TimeSpan.FromDays(-365).TotalSeconds), NSDate.Now.AddSeconds(TimeSpan.FromDays(1).TotalSeconds), HKQueryOptions.None), 0, new NSSortDescriptor[] { },
				new HKSampleQueryResultsHandler((HKSampleQuery query2, HKSample[] results, NSError error2) =>
				{
					var query = results; //property created within the model to expose later.
					Console.WriteLine(query);
					Console.WriteLine(results);
					if (results == null)
						return;
					foreach (var item in results)
					{
						var sample = (HKQuantitySample)item;
						var hkUnit = HKUnit.Count;
						var quantity = sample.Quantity.GetDoubleValue(hkUnit);
						var startDateTime = sample.StartDate;
						var endDateTime = sample.EndDate;
						Console.WriteLine("SpO2: " + quantity);
						Console.WriteLine("SpO2: " + startDateTime);
						Console.WriteLine("SpO2: " + endDateTime);
					}
				}));
			healthKitStore.ExecuteQuery(q);
		}

		internal void GetRatesForType(HKQuantityTypeIdentifier type)
		{
			var healthKitStore = new HKHealthStore();
			var rateType = HKQuantityType.Create(type);
			var sort = new NSSortDescriptor(HKSample.SortIdentifierStartDate, true);
			var q = new HKSampleQuery(rateType,
						HKQuery.GetPredicateForSamples(
							NSDate.Now.AddSeconds(TimeSpan.FromDays(-30).TotalSeconds), /* Estava -365 */
							NSDate.Now.AddSeconds(TimeSpan.FromDays(1).TotalSeconds),
							HKQueryOptions.None), 0, new NSSortDescriptor[] { },
				new HKSampleQueryResultsHandler((HKSampleQuery query2, HKSample[] results, NSError error2) =>
				{
					var query = results;
					Console.WriteLine(type.ToString() + "  :  " + query);
					Console.WriteLine(type.ToString() + "  :  " + results);
					if (results != null)
					{
						foreach (var item in results)
						{
							var sample = (HKQuantitySample)item;
							var hkUnit = HKUnit.Count;

							//Double quantity = 0d;
							//quantity = sample.Quantity.GetDoubleValue(hkUnit);
							var quantity = sample.Quantity.Description;
							var startDateTime = sample.StartDate;
							var endDateTime = sample.EndDate;
							Console.WriteLine("------------");

							AppleHealthDataModel ahdm = new AppleHealthDataModel();
							ahdm.Description = quantity;
							ahdm.StartDate = NSDateToDateTime(startDateTime);
							ahdm.EndDate = NSDateToDateTime(endDateTime);
							ahdm.Type = type.ToString();

                            try
                            {
								ProjetoMRP.Paciente.Services.ApiService.Post<AppleHealthDataModel>("wearable-data", ahdm).Wait();
							}
							catch (Exception ex)
                            {
								Console.WriteLine("Erro ao fazer post para o service WearableData " + ex.Message);
							}
							
							Console.WriteLine(type.ToString() + "  :  " + quantity);
							Console.WriteLine(type.ToString() + "  :  " + startDateTime);
							Console.WriteLine(type.ToString() + "  :  " + endDateTime);

						}
					}
				}));
			healthKitStore.ExecuteQuery(q);
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled != value)
				{
					enabled = value;
					InvokeOnMainThread(() => EnabledChanged(this, new GenericEventArgs<bool>(value)));
				}
			}
		}

		public void PermissionsError(string msg)
		{
			Enabled = false;
			InvokeOnMainThread(() => ErrorMessageChanged(this, new GenericEventArgs<string>(msg)));
		}

		//Converts its argument into a strongly-typed quantity representing the value in beats-per-minute
		public HKQuantity HeartRateInBeatsPerMinute(ushort beatsPerMinute)
		{
			var heartRateUnitType = HKUnit.Count.UnitDividedBy(HKUnit.Minute);
			var quantity = HKQuantity.FromQuantity(heartRateUnitType, beatsPerMinute);

			return quantity;
		}

		//Attempts to store in the Health Kit database a quantity, which must be of a type compatible with beats-per-minute
		public void StoreHeartRate(HKQuantity quantity)
		{
			var bpm = HKUnit.Count.UnitDividedBy(HKUnit.Minute);
			//Confirm that the value passed in is of a valid type (can be converted to beats-per-minute)
			if (!quantity.IsCompatible(bpm))
			{
				InvokeOnMainThread(() => ErrorMessageChanged(this, new GenericEventArgs<string>("Units must be compatible with BPM")));
			}

			var heartRateId = HKQuantityTypeIdentifierKey.HeartRate;
			var heartRateQuantityType = HKQuantityType.GetQuantityType(heartRateId);

			var heartRateSample = HKQuantitySample.FromType(heartRateQuantityType, quantity, new NSDate(), new NSDate(), new HKMetadata());

			using (var healthKitStore = new HKHealthStore())
			{
				healthKitStore.SaveObject(heartRateSample, (success, error) => {
					InvokeOnMainThread(() => {
						if (success)
						{
							HeartRateStored(this, new GenericEventArgs<Double>(quantity.GetDoubleValue(bpm)));
						}
						else
						{
							ErrorMessageChanged(this, new GenericEventArgs<string>("Save failed"));
						}
						if (error != null)
						{
							//If there's some kind of error, disable 
							Enabled = false;
							ErrorMessageChanged(this, new GenericEventArgs<string>(error.ToString()));
						}
					});
				});
			}
		}

		public static DateTime NSDateToDateTime(Foundation.NSDate date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));
			return reference.AddSeconds(date.SecondsSinceReferenceDate);
		}
	}


	public class AppleHealthDataModel{
        public string Description { get; set; }
		public string Type { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
