using System.Xml.Serialization;

namespace CecEmulator.DomainModels;

[XmlRoot(ElementName="device-info")]
public class DeviceInfo { 

	[XmlElement(ElementName="udn")] 
	public required string Udn { get; init; } 

	[XmlElement(ElementName="serial-number")] 
	public required string Serialnumber { get; init; } 

	[XmlElement(ElementName="device-id")] 
	public required string Deviceid { get; init; } 

	[XmlElement(ElementName="advertising-id")] 
	public required string Advertisingid { get; init; } 

	[XmlElement(ElementName="vendor-name")] 
	public required string Vendorname { get; init; } 

	[XmlElement(ElementName="model-name")] 
	public required string Modelname { get; init; } 

	[XmlElement(ElementName="model-number")] 
	public required string Modelnumber { get; init; } 

	[XmlElement(ElementName="model-region")] 
	public required string Modelregion { get; init; } 

	[XmlElement(ElementName="is-tv")] 
	public required bool Istv { get; init; } 

	[XmlElement(ElementName="is-stick")] 
	public required bool Isstick { get; init; } 

	[XmlElement(ElementName="screen-size")] 
	public required int Screensize { get; init; } 

	[XmlElement(ElementName="panel-id")] 
	public required int Panelid { get; init; } 

	[XmlElement(ElementName="mobile-has-live-tv")] 
	public required bool Mobilehaslivetv { get; init; } 

	[XmlElement(ElementName="ui-resolution")] 
	public required string Uiresolution { get; init; } 

	[XmlElement(ElementName="tuner-type")] 
	public required string Tunertype { get; init; } 

	[XmlElement(ElementName="supports-ethernet")] 
	public required bool Supportsethernet { get; init; } 

	[XmlElement(ElementName="wifi-mac")] 
	public required string Wifimac { get; init; } 

	[XmlElement(ElementName="wifi-driver")] 
	public required string Wifidriver { get; init; } 

	[XmlElement(ElementName="has-wifi-extender")] 
	public required bool Haswifiextender { get; init; } 

	[XmlElement(ElementName="has-wifi-5G-support")] 
	public required bool Haswifi5Gsupport { get; init; } 

	[XmlElement(ElementName="can-use-wifi-extender")] 
	public required bool Canusewifiextender { get; init; } 

	[XmlElement(ElementName="ethernet-mac")] 
	public required string Ethernetmac { get; init; } 

	[XmlElement(ElementName="network-type")] 
	public required string Networktype { get; init; } 

	[XmlElement(ElementName="network-name")] 
	public required string Networkname { get; init; } 

	[XmlElement(ElementName="friendly-device-name")] 
	public required string Friendlydevicename { get; init; } 

	[XmlElement(ElementName="friendly-model-name")] 
	public required string Friendlymodelname { get; init; } 

	[XmlElement(ElementName="default-device-name")] 
	public required string Defaultdevicename { get; init; } 

	[XmlElement(ElementName="user-device-name")] 
	public required string Userdevicename { get; init; } 

	[XmlElement(ElementName="user-device-location")] 
	public required string Userdevicelocation { get; init; } 

	[XmlElement(ElementName="build-number")] 
	public required string Buildnumber { get; init; } 

	[XmlElement(ElementName="software-version")] 
	public required string Softwareversion { get; init; } 

	[XmlElement(ElementName="software-build")] 
	public required int Softwarebuild { get; init; } 

	[XmlElement(ElementName="secure-device")] 
	public required bool Securedevice { get; init; } 

	[XmlElement(ElementName="language")] 
	public required string Language { get; init; } 

	[XmlElement(ElementName="country")] 
	public required string Country { get; init; } 

	[XmlElement(ElementName="locale")] 
	public required string Locale { get; init; } 

	[XmlElement(ElementName="time-zone-auto")] 
	public required bool Timezoneauto { get; init; } 

	[XmlElement(ElementName="time-zone")] 
	public required string Timezone { get; init; } 

	[XmlElement(ElementName="time-zone-name")] 
	public required string Timezonename { get; init; } 

	[XmlElement(ElementName="time-zone-tz")] 
	public required string Timezonetz { get; init; } 

	[XmlElement(ElementName="time-zone-offset")] 
	public required int Timezoneoffset { get; init; } 

	[XmlElement(ElementName="clock-format")] 
	public required string Clockformat { get; init; } 

	[XmlElement(ElementName="uptime")] 
	public required int Uptime { get; init; } 

	[XmlElement(ElementName="power-mode")] 
	public required string Powermode { get; init; } 

	[XmlElement(ElementName="supports-suspend")] 
	public required bool Supportssuspend { get; init; } 

	[XmlElement(ElementName="supports-find-remote")] 
	public required bool Supportsfindremote { get; init; } 

	[XmlElement(ElementName="find-remote-is-possible")] 
	public required bool Findremoteispossible { get; init; } 

	[XmlElement(ElementName="supports-audio-guide")] 
	public required bool Supportsaudioguide { get; init; } 

	[XmlElement(ElementName="supports-rva")] 
	public required bool Supportsrva { get; init; } 

	[XmlElement(ElementName="has-hands-free-voice-remote")] 
	public required bool Hashandsfreevoiceremote { get; init; } 

	[XmlElement(ElementName="developer-enabled")] 
	public required bool Developerenabled { get; init; } 

	[XmlElement(ElementName="keyed-developer-id")] 
	public required object Keyeddeveloperid { get; init; } 

	[XmlElement(ElementName="search-enabled")] 
	public required bool Searchenabled { get; init; } 

	[XmlElement(ElementName="search-channels-enabled")] 
	public required bool Searchchannelsenabled { get; init; } 

	[XmlElement(ElementName="voice-search-enabled")] 
	public required bool Voicesearchenabled { get; init; } 

	[XmlElement(ElementName="notifications-enabled")] 
	public required bool Notificationsenabled { get; init; } 

	[XmlElement(ElementName="notifications-first-use")] 
	public required bool Notificationsfirstuse { get; init; } 

	[XmlElement(ElementName="supports-private-listening")] 
	public required bool Supportsprivatelistening { get; init; } 

	[XmlElement(ElementName="supports-private-listening-dtv")] 
	public required bool Supportsprivatelisteningdtv { get; init; } 

	[XmlElement(ElementName="supports-warm-standby")] 
	public required bool Supportswarmstandby { get; init; } 

	[XmlElement(ElementName="headphones-connected")] 
	public required bool Headphonesconnected { get; init; } 

	[XmlElement(ElementName="supports-audio-settings")] 
	public required bool Supportsaudiosettings { get; init; } 

	[XmlElement(ElementName="expert-pq-enabled")] 
	public required double Expertpqenabled { get; init; } 

	[XmlElement(ElementName="supports-ecs-textedit")] 
	public required bool Supportsecstextedit { get; init; } 

	[XmlElement(ElementName="supports-ecs-microphone")] 
	public required bool Supportsecsmicrophone { get; init; } 

	[XmlElement(ElementName="supports-wake-on-wlan")] 
	public required bool Supportswakeonwlan { get; init; } 

	[XmlElement(ElementName="supports-airplay")] 
	public required bool Supportsairplay { get; init; } 

	[XmlElement(ElementName="has-play-on-roku")] 
	public required bool Hasplayonroku { get; init; } 

	[XmlElement(ElementName="has-mobile-screensaver")] 
	public required bool Hasmobilescreensaver { get; init; } 

	[XmlElement(ElementName="support-url")] 
	public required string Supporturl { get; init; } 

	[XmlElement(ElementName="grandcentral-version")] 
	public required string Grandcentralversion { get; init; } 

	[XmlElement(ElementName="supports-trc")] 
	public required bool Supportstrc { get; init; } 

	[XmlElement(ElementName="trc-version")] 
	public required double Trcversion { get; init; } 

	[XmlElement(ElementName="trc-channel-version")] 
	public required string Trcchannelversion { get; init; } 

	[XmlElement(ElementName="davinci-version")] 
	public required string Davinciversion { get; init; } 

	[XmlElement(ElementName="av-sync-calibration-enabled")] 
	public required double Avsynccalibrationenabled { get; init; } 
}

