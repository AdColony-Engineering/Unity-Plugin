ROOT_DIR:=$(shell dirname $(realpath $(lastword $(MAKEFILE_LIST))))

IOS_REPO_NAME := AdColony-iOS-SDK-3
ANDROID_REPO_NAME := AdColony-Android-SDK-3
FULL_GIT_PATH_IOS := git@github.com:AdColony/$(IOS_REPO_NAME).git
FULL_GIT_PATH_ANDROID := git@github.com:AdColony/$(ANDROID_REPO_NAME).git
IOS_SDK_BRANCH := master
ANDROID_SDK_BRANCH := master

UNITY_PLUGIN_DIR := $(shell pwd)
IOS_DIR := $(UNITY_PLUGIN_DIR)/src/sdks/$(IOS_REPO_NAME)
ANDROID_DIR := $(UNITY_PLUGIN_DIR)/src/sdks/$(ANDROID_REPO_NAME)
ANDROID_FINAL_JAR := $(ANDROID_DIR)/Library/adcolony.jar
ANDROID_FINAL_WRAPPER_DIR := $(UNITY_PLUGIN_DIR)/src/android
ANDROID_FINAL_WRAPPER_JAR := $(UNITY_PLUGIN_DIR)/src/android/library/build/outputs/jar/adcolony-wrapper.jar
UNITY_PACKAGE_FULL_PATH := $(UNITY_PLUGIN_DIR)/AdColony.unitypackage
UNITY_TEMP_PROJECT_DIR := $(UNITY_PLUGIN_DIR)/tempunitypackage
UNITY_BINARY := "/Applications/Unity/Unity.app/Contents/MacOS/Unity"
UNITY_LOG_FILE := $(UNITY_PLUGIN_DIR)/`basename $(UNITY_PACKAGE_FULL_PATH)`.build.log

# ----AdColony Unity Plugin package build ----
PLUGIN_PACKAGE_FULL_PATH := $(UNITY_PLUGIN_DIR)/AdColony.unitypackage
PLUGIN_TEMP_PROJECT_DIR := $(UNITY_PLUGIN_DIR)/temppluginpackage
PLUGIN_LOG_FILE := $(UNITY_PLUGIN_DIR)/`basename $(PLUGIN_PACKAGE_FULL_PATH)`.build.log

.PHONY: clean

all: unity

# ---- iOS ----

ios:
	@if [ ! -x "$(IOS_DIR)/AdColony.framework" ]; then \
		echo "ERROR: AdColony iOS SDK not found\n"; \
		echo "Make sure you've placed the SDK in the $(IOS_DIR), run: \n  cd $(IOS_DIR)\n  git clone git@github.com:AdColony/AdColony-iOS-SDK-3.git\n"; \
		exit 1; \
	fi;

clean_ios:
	rm -rf $(IOS_DIR)

# ---- Android ----

android_jar:
	@if [ ! -f "$(ANDROID_FINAL_JAR)" ]; then \
		echo "ERROR: AdColony Android SDK not found\n"; \
		echo "Make sure you've placed the SDK in the $(ANDROID_DIR), run: \n  cd $(ANDROID_DIR)\n  git clone git@github.com:AdColony/AdColony-Android-SDK-3.git\n"; \
		exit 1; \
	fi;

clean_android_jar:
	rm -rf $(ANDROID_DIR)

android_wrapper: android_jar
	cp $(ANDROID_FINAL_JAR) $(ANDROID_FINAL_WRAPPER_DIR)/library/libs/AdColony.jar
	cp -r $(ANDROID_DIR)/Library/* $(ANDROID_FINAL_WRAPPER_DIR)/library/libs
	cd $(ANDROID_FINAL_WRAPPER_DIR) && ./gradlew clean && ./gradlew makeJar

clean_android_wrapper:
	rm -rf $(UNITY_PLUGIN_DIR)/src/android/Library/AdColony.jar
	rm -f $(UNITY_PLUGIN_DIR)/src/android/Library/bin/classes.jar

android: android_jar android_wrapper

clean_android: clean_android_jar clean_android_wrapper

# ---- Unity ----
# Copies the SDK w/ android JAR wrapper from the src/SDK directory
prepare_assets_from_sdk_root:
	@# iOS
	rm -rf $(UNITY_PLUGIN_DIR)/Assets/Plugins/iOS/AdColony/AdColony.framework
	cp -r $(IOS_DIR)/AdColony.framework $(UNITY_PLUGIN_DIR)/Assets/Plugins/iOS/AdColony
	rm -rf $(UNITY_PLUGIN_DIR)/Assets/Plugins/iOS/AdColony/AdColony.framework/Versions

	@# Android
	rm -rf $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/AdColony/libs
	mkdir -p $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/libs
	mkdir -p $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/AdColony/libs
	cp $(ANDROID_FINAL_JAR) $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/AdColony/libs/AdColony.jar
	cp $(ANDROID_FINAL_WRAPPER_JAR) $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/AdColony/libs/AdColonyUnityWrapper.jar
	find $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/libs -name "libadcolony.so.*" -exec rm {} +
	find $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/libs -name "libjs.so.*" -exec rm {} +
	cp -r $(ANDROID_DIR)/Library/* $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/libs
	rm $(UNITY_PLUGIN_DIR)/Assets/Plugins/Android/libs/adcolony.jar
	
	@# remove unsupported Unity architectures
	# rm -rf $(ANDROID_DIR)/Library/arm64-v8a
	# rm -rf $(ANDROID_DIR)/Library/armeabi
	# rm -rf $(ANDROID_DIR)/Library/x86_64


unity_package:
	@echo "Logfile: $(UNITY_LOG_FILE)"
	@echo "Building Unity package: $(UNITY_PACKAGE_FULL_PATH)"
	@rm -rf $(UNITY_PACKAGE_FULL_PATH)
	@rm -rf $(UNITY_TEMP_PROJECT_DIR)
	@mkdir $(UNITY_TEMP_PROJECT_DIR)
	@cp -r $(UNITY_PLUGIN_DIR)/Assets $(UNITY_TEMP_PROJECT_DIR)

	@if [ ! -x "$(UNITY_BINARY)" ]; then \
		echo "ERROR: $(UNITY_BINARY) not found"; exit 1; \
	fi;

	@if [ -x "$(UNITY_BINARY)" ]; then \
		"$(UNITY_BINARY)" \
		-batchmode \
		-quit \
		-force-free \
		-logFile $(UNITY_LOG_FILE) \
		-projectPath $(UNITY_TEMP_PROJECT_DIR) \
		-exportPackage "Assets" $(UNITY_PACKAGE_FULL_PATH) \
		|| @echo "ERROR: Error building unity package, see log: $(UNITY_LOG_FILE)"; \
	else \
		@echo "ERROR: Unity package not created, looked for Unity at $(UNITY_BINARY)."; \
	fi;

	@rm -rf $(UNITY_TEMP_PROJECT_DIR)

unity: android ios prepare_assets_from_sdk_root unity_package

clean_unity:
	rm -rf sdks
	rm -rf $(UNITY_TEMP_PROJECT_DIR)

clean: clean_android clean_ios clean_unity
