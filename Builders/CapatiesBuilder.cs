using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace QYPBXEditTool
{
    public class CapatiesBuilder
    {
        private string open_key = "isOpen";
        public List<Hashtable> m_capaties;
        public ProjectCapabilityManager CapabilityManager;
        public CapatiesBuilder(List<Hashtable> capaties)
        {
            m_capaties = capaties;
        }

        public void Builder(string projPath)
        {
            m_capaties.ForEach(o =>
            {
                if (CapabilityManager == null)
                {
                    this.CapabilityManager = new ProjectCapabilityManager(projPath,
                        o["entitlementFilePath"].ToString(),
                        PBXProject.GetUnityTargetName());
                }
                paraseAndBuilder(o);

            });
            
            this.CapabilityManager.WriteToFile();

        }

        public void paraseAndBuilder(Hashtable data)
        {
            foreach (DictionaryEntry o in data)
            {
                Debug.LogFormat("run key = {0}",o.Key.ToString());
                if (!o.Key.Equals("entitlementFilePath"))
                {
                    switch (o.Key.ToString())
                    {
                        case "AccessWiFiInfomation":
                        {
                            AccessWiFiInfomation(data["AccessWiFiInfomation"] as Hashtable);
                            break;
                        }
                        case "AppGroups":
                        {
                            AppGroups(data["AppGroups"] as Hashtable);
                            break;
                        }
                        case "ApplePay":
                        {
                            ApplePay(data["ApplePay"] as Hashtable);
                            break;
                        }
                        case "AssociatedDomains":
                        {
                            AssociatedDomains(data["AssociatedDomains"] as Hashtable);
                            break;
                        }
                        case "BackgroundModes":
                        {
                            BackgroundModes(data["AssociatedDomains"] as Hashtable);
                            break;
                        }
                        case "DataProtection":
                        {
                            DataProtection(data["DataProtection"] as Hashtable);
                            break;
                        }
                        case "GameCenter":
                        {
                            GameCenter(data["GameCenter"] as Hashtable);
                            break;
                        }
                        case "HealthKit":
                        {
                            HealthKit(data["HealthKit"] as Hashtable);
                            break;
                        }
                        case "HomeKit":
                        {
                            HomeKit(data["HomeKit"] as Hashtable);
                            break;
                        }
                        case "iColoud":
                        {
                            iColoud(data["iColoud"] as Hashtable);
                            break;
                        }
                        case "InAppPurchase":
                        {
                            InAppPurchase(data["InAppPurchase"] as Hashtable);
                            break;
                        }
                        case "InterAppAudio":
                        {
                            InterAppAudio(data["InterAppAudio"] as Hashtable);
                            break;
                        }
                        case "keychainSharing":
                        {
                            keychainSharing(data["keychainSharing"] as Hashtable);
                            break;
                        }
                        case "Maps":
                        {
                            Maps(data["Maps"] as Hashtable);
                            break;
                        }
                        case "PersonalVPN":
                        {
                            PersonalVPN(data["PersonalVPN"] as Hashtable);
                            break;
                        }
                        case "PushNotifications":
                        {
                            PushNotifications(data["PushNotifications"] as Hashtable);
                            break;             
                        }
                        case "Siri":
                        {
                            Siri(data["Siri"] as Hashtable);
                            break;
                        }
                        case "wallet":
                        {
                            wallet(data["wallet"] as Hashtable);
                            break;
                        }
                    }
                }
            }
        }                                                                                                                                                                     
        public void wallet(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddWallet(data["passSubset"] as string[]);
            }
        }
        public void Siri(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddSiri();
            }
        }
        public void PushNotifications(Hashtable data)
        {
            Debug.LogFormat("is open = {0}",IsOpen(data));
            Debug.LogFormat("data = {0}",data.toJson());
            if (IsOpen(data))
            {
                this.CapabilityManager.AddPushNotifications(true);
            }
        }
        public void PersonalVPN(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddPersonalVPN();
            }
        }

        public void Maps(Hashtable data)
        {
            if (IsOpen(data))
            {
                Int32 value = Convert.ToInt32(data["MapsOptions"].ToString());
                this.CapabilityManager.AddMaps(GetMapsOptions(value));
            }
        }

        public void keychainSharing(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddKeychainSharing(data["accessGroups"] as string[]);
            }
        }

        public void InterAppAudio(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddInterAppAudio();
            }
        }

        public void InAppPurchase(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddInAppPurchase();
            }
        }

        public void iColoud(Hashtable data)
        {
            if (IsOpen(data))
            {
                if (data["enableKeyValueStorage"] is bool enableKeyValueStorage &&
                    data["enableiCloudDocument"] is bool enableiCloudDocument)
                {
                    this.CapabilityManager.AddiCloud(
                        enableKeyValueStorage,
                        enableiCloudDocument,
                        data["customContainers"] as string[]
                        );
                }
            }
        }
        public void HomeKit(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddHomeKit();
            }
        }

        public void HealthKit(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddHealthKit();
            }
        }
        public void GameCenter(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddGameCenter();
            }
        }

        public void DataProtection(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddDataProtection();
            }
        }

        public void BackgroundModes(Hashtable data)
        {
            if (IsOpen(data))
            {
                Int32 value = Convert.ToInt32(data["options"].ToString());
                this.CapabilityManager.AddBackgroundModes(GetOptions(value));
            }
        }

        public void AssociatedDomains(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddAssociatedDomains(data["domains"] as string[]);
            }
        }

        public void ApplePay(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddApplePay(data["merchants"] as string[]);
            }
        }

        public void AccessWiFiInfomation(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddAccessWiFiInformation();
            }
        }

        public void AppGroups(Hashtable data)
        {
            if (IsOpen(data))
            {
                this.CapabilityManager.AddAppGroups(data["groups"] as string[]);
            }
        }

        public bool IsOpen(Hashtable data)
        {
            if (data[open_key] is Boolean isOpen)
            {
                return isOpen;
            }

            return false;
        }

        private BackgroundModesOptions GetOptions(Int32 key)
        {
            Dictionary<Int32, BackgroundModesOptions> dictionary = new Dictionary<int, BackgroundModesOptions>
            {
                [0] = BackgroundModesOptions.None,
                [1] = BackgroundModesOptions.AudioAirplayPiP,
                [2] = BackgroundModesOptions.LocationUpdates,
                [4] = BackgroundModesOptions.VoiceOverIP,
                [8] = BackgroundModesOptions.NewsstandDownloads,
                [16] = BackgroundModesOptions.ExternalAccessoryCommunication,
                [32] = BackgroundModesOptions.UsesBluetoothLEAccessory,
                [64] = BackgroundModesOptions.ActsAsABluetoothLEAccessory,
                [128] = BackgroundModesOptions.BackgroundFetch,
                [256] = BackgroundModesOptions.RemoteNotifications,
            };
            return dictionary[key];
        }

        private MapsOptions GetMapsOptions(Int32 key)
        {

            Dictionary<Int32, MapsOptions> dictionary = new Dictionary<int, MapsOptions>
            {
                [0] = MapsOptions.None,
                [1] = MapsOptions.Airplane,
                [2] = MapsOptions.Bike,
                [4] = MapsOptions.Bus,
                [8] = MapsOptions.Car,
                [16] = MapsOptions.Ferry,
                [32] = MapsOptions.Pedestrian,
                [64] = MapsOptions.RideSharing,
                [128] = MapsOptions.StreetCar,
                [256] = MapsOptions.Subway,
                [512] = MapsOptions.Taxi,
                [1024] = MapsOptions.Train,
                [2048] = MapsOptions.Other,
            };
            return dictionary[key];
        }
    }
}