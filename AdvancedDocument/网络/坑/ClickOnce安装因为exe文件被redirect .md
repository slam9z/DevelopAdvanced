[ClickOnce安装因为exe文件被redirect]()

##日志

PLATFORM VERSION INFO
	Windows 			: 6.1.7601.65536 (Win32NT)
	Common Language Runtime 	: 4.0.30319.233
	System.Deployment.dll 		: 4.0.30319.1 (RTMRel.030319-0100)
	clr.dll 			: 4.0.30319.233 (RTMGDR.030319-2300)
	dfdll.dll 			: 4.0.30319.1 (RTMRel.030319-0100)
	dfshim.dll 			: 4.0.31106.0 (Main.031106-0000)

SOURCES
	Deployment url			: http://www.ioffice100.com/downloads/pc/TaskForce.application
						Server		: Microsoft-IIS/8.0
						X-Powered-By	: ASP.NET
	Deployment Provider url		: http://www.ioffice100.com/downloads/pc/TaskForce.application
	Application url			: http://www.ioffice100.com/downloads/pc/Application%20Files/TaskForce_4_1_5_395/TaskForce.exe.manifest
						Server		: Microsoft-IIS/8.0
						X-Powered-By	: ASP.NET

IDENTITIES
	Deployment Identity		: TaskForce.application, Version=4.1.5.395, Culture=neutral, PublicKeyToken=41db5ceb352995f6, processorArchitecture=x86
	Application Identity		: TaskForce.exe, Version=4.1.5.395, Culture=neutral, PublicKeyToken=41db5ceb352995f6, processorArchitecture=x86, type=win32

APPLICATION SUMMARY
	* Installable application.

ERROR SUMMARY
	Below is a summary of the errors, details of these errors are listed later in the log.
	* Activation of http://www.ioffice100.com/downloads/pc/TaskForce.application resulted in exception. Following failure messages were detected:
		+ HTTP redirect is not allowed for application files and assemblies. Cannot download TaskForce.exe.

COMPONENT STORE TRANSACTION FAILURE SUMMARY
	No transaction error was detected.

WARNINGS
	There were no warnings during this operation.

OPERATION PROGRESS STATUS
	* [2016/7/26 14:17:15] : Activation of http://www.ioffice100.com/downloads/pc/TaskForce.application has started.
	* [2016/7/26 14:17:15] : Processing of deployment manifest has successfully completed.
	* [2016/7/26 14:17:15] : Installation of the application has started.
	* [2016/7/26 14:17:17] : Processing of application manifest has successfully completed.
	* [2016/7/26 14:17:49] : Found compatible runtime version 4.0.30319.
	* [2016/7/26 14:17:49] : Request of trust and detection of platform is complete.

ERROR DETAILS
	Following errors were detected during this operation.
	* [2016/7/26 14:23:32] System.Deployment.Application.InvalidDeploymentException (AppFileLocationValidation)
		- HTTP redirect is not allowed for application files and assemblies. Cannot download TaskForce.exe.
		- Source: System.Deployment
		- Stack trace:
			at System.Deployment.Application.DownloadManager.ProcessDownloadedFile(Object sender, DownloadEventArgs e)
			at System.Deployment.Application.FileDownloader.DownloadModifiedEventHandler.Invoke(Object sender, DownloadEventArgs e)
			at System.Deployment.Application.SystemNetDownloader.DownloadSingleFile(DownloadQueueItem next)
			at System.Deployment.Application.SystemNetDownloader.DownloadAllFiles()
			at System.Deployment.Application.FileDownloader.Download(SubscriptionState subState)
			at System.Deployment.Application.DownloadManager.DownloadDependencies(SubscriptionState subState, AssemblyManifest deployManifest, AssemblyManifest appManifest, Uri sourceUriBase, String targetDirectory, String group, IDownloadNotification notification, DownloadOptions options)
			at System.Deployment.Application.ApplicationActivator.DownloadApplication(SubscriptionState subState, ActivationDescription actDesc, Int64 transactionId, TempDirectory& downloadTemp)
			at System.Deployment.Application.ApplicationActivator.InstallApplication(SubscriptionState& subState, ActivationDescription actDesc)
			at System.Deployment.Application.ApplicationActivator.PerformDeploymentActivation(Uri activationUri, Boolean isShortcut, String textualSubId, String deploymentProviderUrlFromExtension, BrowserSettings browserSettings, String& errorPageUrl)
			at System.Deployment.Application.ApplicationActivator.ActivateDeploymentWorker(Object state)

COMPONENT STORE TRANSACTION DETAILS
	No transaction information is available.


##问题

http://www.cnblogs.com/ueqtxu/p/4179752.html