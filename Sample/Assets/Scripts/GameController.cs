﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{	
	Asteroid asteroidConfigure;
	Asteroid asteroidRequest;
	Asteroid asteroidPlay;

	AdColony.InterstitialAd Ad = null;

	float currencyPopupTimer = 0.0f;

	void Start() {
		GameObject configureObj = GameObject.FindGameObjectWithTag(Constants.AsteroidConfigureTag);
		asteroidConfigure = configureObj.GetComponent<Asteroid>();

		GameObject requestObj = GameObject.FindGameObjectWithTag(Constants.AsteroidRequestTag);
		asteroidRequest = requestObj.GetComponent<Asteroid>();

		GameObject playObj = GameObject.FindGameObjectWithTag(Constants.AsteroidPlayTag);
		asteroidPlay = playObj.GetComponent<Asteroid>();

		// Only configure asteroid is available at start.
		asteroidConfigure.Show();
		asteroidRequest.Hide();
		asteroidPlay.Hide();

		// ----- AdColony Ads -----

		AdColony.Ads.OnConfigurationCompleted += (List<AdColony.Zone> zones_) => {
			Debug.Log("AdColony.Ads.OnConfigurationCompleted called");

			if (zones_ == null || zones_.Count <= 0) {
				// Show the configure asteroid again.
				asteroidConfigure.Show();
			}
			else {
				// Successfully configured... show the request ad asteroid.
				asteroidRequest.Show();
			}
		};

		AdColony.Ads.OnRequestInterstitial += (AdColony.InterstitialAd ad_) => {
			Debug.Log("AdColony.Ads.OnRequestInterstitial called");
			Ad = ad_;

			// Successfully requested ad... show the play ad asteroid.
			asteroidPlay.Show();
		};

		AdColony.Ads.OnRequestInterstitialFailedWithZone += (string zoneId) => {
			Debug.Log("AdColony.Ads.OnRequestInterstitialFailedWithZone called, zone: " + zoneId);

			// Request Ad failed... show the request ad asteroid.
			asteroidRequest.Show();
		};

		AdColony.Ads.OnOpened += (AdColony.InterstitialAd ad_) => {
			Debug.Log("AdColony.Ads.OnOpened called");

			// Ad started playing... show the request ad asteroid for the next ad.
			asteroidRequest.Show();
		};

		AdColony.Ads.OnClosed += (AdColony.InterstitialAd ad_) => {
			Debug.Log("AdColony.Ads.OnClosed called, expired: " + ad_.Expired);
		};

		AdColony.Ads.OnExpiring += (AdColony.InterstitialAd ad_) => {
			Debug.Log("AdColony.Ads.OnExpiring called");
			Ad = null;

			// Current ad expired... show the request ad asteroid.
			asteroidRequest.Show();
			asteroidPlay.Hide();
		};

		AdColony.Ads.OnIAPOpportunity += (AdColony.InterstitialAd ad_, string iapProductId_, AdColony.AdsIAPEngagementType engagement_) => {
			Debug.Log("AdColony.Ads.OnIAPOpportunity called");
		};

		AdColony.Ads.OnRewardGranted += (string zoneId, bool success, string name, int amount) => {
			Debug.Log(string.Format("AdColony.Ads.OnRewardGranted called\n\tzoneId: {0}\n\tsuccess: {1}\n\tname: {2}\n\tamount: {3}", zoneId, success, name, amount));
		};

		AdColony.Ads.OnCustomMessageReceived += (string type, string message) => {
			Debug.Log(string.Format("AdColony.Ads.OnCustomMessageReceived called\n\ttype: {0}\n\tmessage: {1}", type, message));
		};
	}

	void Update() {
		if (currencyPopupTimer > 0.0f) {
			// This is a temporary hack to make sure we can re-show the request asteroid 
			// if we are playing a currency ad and the user click "NO" on the popup dialog.
			currencyPopupTimer -= Time.deltaTime;
			if (currencyPopupTimer <= 0.0f) {
				asteroidRequest.Show();
				asteroidPlay.Hide();
			}
		}
	}

	void ConfigureAds() {
		// Configure the AdColony SDK
		Debug.Log("**** Configure ADC SDK ****");

		// Set some test app options with metadata.
		AdColony.AppOptions appOptions = new AdColony.AppOptions();
		appOptions.UserId = "foo";
		appOptions.AdOrientation = AdColony.AdOrientationType.AdColonyOrientationAll;
		appOptions.SetOption("test_key", "Happy Fun Time!");

		AdColony.UserMetadata metadata = new AdColony.UserMetadata();
		metadata.Age = 35;
		metadata.Gender = "Male";
		metadata.Interests = new List<string> { "gaming", "hiking" };
		metadata.Latitude = 47.6469425;
		metadata.Longitude = -122.2004281;
		metadata.ZipCode = "98033";
		metadata.HouseholdIncome = 75000;
		metadata.MaritalStatus = "Single";
		metadata.EducationLevel = "Bachelors";
		metadata.SetMetadata("test_key_02", "Happy Meta Time?");
		appOptions.Metadata = metadata;

		string[] zoneIDs = new string[] { Constants.InterstitialZoneID, Constants.CurrencyZoneID };

		AdColony.Ads.Configure(Constants.AppID, appOptions, zoneIDs);
	}

	void RequestAd() {
		// Request an ad.
		Debug.Log("**** Request Ad ****");

		AdColony.AdOptions adOptions = new AdColony.AdOptions();
		adOptions.ShowPrePopup = true;
		adOptions.ShowPostPopup = true;

		AdColony.Ads.RequestInterstitialAd(Constants.CurrencyZoneID, adOptions);
	}

	void PlayAd() {
		if (Ad != null) {
			AdColony.Ads.ShowAd(Ad);

			if (Ad.ZoneId == Constants.CurrencyZoneID) {
				currencyPopupTimer = 5.0f;
			}
		}
	}

	public void PerformAdColonyAction(Asteroid asteroid) {
		if (asteroid == asteroidConfigure) {
			ConfigureAds();
		} else if (asteroid == asteroidRequest) {
			RequestAd();
		} else if (asteroid == asteroidPlay) {
			PlayAd();
		}
	}
}