# Change Log

## v3.3.5 (2018/07/06)
* Officially open sourced Unity plugin

## v3.3.4 (2018/05/25)
* Updated to AdColony SDK 3.3.4 (iOS) and 3.3.4 (Android)
* [iOS] Fixed a bug where advertisement video's close button was not easily tappable because of the status bar overlapping.
* [iOS] Fixed a bug where unsafe access to the device's battery level was causing a crash mentioned in [iOS SDK issue #49](https://github.com/AdColony/AdColony-iOS-SDK-3/issues/49).
* [Android] Fixed new NullPointerException mentioned in [Android SDK issue #29](https://github.com/AdColony/AdColony-Android-SDK-3/issues/29#issuecomment-381380548).
* [Unity] Added a new API to pass user consent as required for compliance with the European Union's General Data Protection Regulation (GDPR). If you are collecting consent from your users, you can make use of this new API to inform AdColony and all downstream consumers of the consent. Please see our GDPR FAQ for more information and our GDPR wiki for implementation details.
* [Unity] Removed symbolic links from within native SDK
* [Unity] Fixed missing zone ID in some log statements
* [Unity] Fixed exception during OnRequestInterstitialFailed callback mentioned in [Unity Plugin issue #42](https://github.com/AdColony/AdColony-Unity-SDK-3/issues/42)
* [All] Several bug fixes and stability improvements.

## v3.3.0 (2018/02/02)
* Updated to AdColony SDK 3.3.0 (iOS) and 3.3.0 (Android)
* Added Integral Ad Science (IAS) for viewability measurement
* Better iPhone X compatibility (iOS)
* Fixed storage overuse issue reported by a small number of publishers upgrading from 2.x -> 3.x (Android)
* Several bug fixes, memory usage optimizations, and stability improvements

## v3.2.1 (2017/09/20)
* Updated to AdColony SDK 3.2.1 (iOS) and 3.2.0 (Android).
* Support for iOS 11 and Android O
* Added onLeftApplication and onClicked callbacks
* Various bugfixes from support channels

## v3.1.0 (2017/04/07)
* Updated to AdColony SDK 3.1.1 (iOS) and 3.1.2 (Android).
* Updated to Google PlayServicesResolver 1.2.15
* Support for thin/fat Android builds
* Removed AdColony Compass™ public API
* Added OnRequestInterstitialFailedWithZone event
* Deprecating OnRequestInterstitialFailed event
* Adds -ObjC linker flag to iOS builds for Aurora SDK 3.0 ads
* Misc cleanup and bugfixes

## 3.0.6 (2016/12/16)
* Updated the Unity SDK package to use iOS and Android SDK 3.0.6.

## 3.0.0 (2016/11/04)
* Initial upload of the Unity plugin and sample app.