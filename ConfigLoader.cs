using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QYPBXEditTool
{
    public class ConfigLoader
    {
        #region 配置key

        private const string PLISTINFO_ROOT_KEY = "plistInfo";
        private const string LIBRARY_ROOT_KEY = "framework";
        private const string FRAMEWORK_ROOT_KEY = "framework";
        private const string BUILDSETTING_ROOT_KEY = "buildSetting";
        
        #endregion

        
        #region 属性
        private Hashtable dataSoure = new Hashtable();
        //plist 流数据
        public List<PlistModel>    plistModes  = new List<PlistModel>();
        public List<LibrarayModel> libModels = new List<LibrarayModel>();
        public List<BuildSetting>  settingModels = new List<BuildSetting>();

        #endregion
        
        public void LoadConfig()
        {
            string[] files = Directory.GetFiles(Application.dataPath, "*.pbxconfig", SearchOption.AllDirectories);
            
            foreach (string file in files)
            {
                Debug.LogFormat("filepath = {0}",file);
                ReadConfigFile(file);
            }
        }
        
        private void ReadConfigFile(string path)
        {
            if(path != null)
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    StreamReader reader = fileInfo.OpenText();
                    string contents = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    this.dataSoure = MiniJSON.jsonDecode(contents) as  Hashtable;
                    
                    this.plistModes.Add(new PlistModel(dataSoure[PLISTINFO_ROOT_KEY] as  Hashtable)); 
                    
                    this.libModels.Add(new LibrarayModel
                    (
                        dataSoure[LIBRARY_ROOT_KEY] as Hashtable,
                        dataSoure[FRAMEWORK_ROOT_KEY] as Hashtable
                    ));
                    this.settingModels.Add(new BuildSetting(dataSoure[BUILDSETTING_ROOT_KEY] as  Hashtable));
                    
//                   
//                    //获取 系统library
//                    parserElement<string>(SYSTEM_LIBRARY_KEY,systemLibrary);
//                    //获取 系统 framework
//                    parserElement<string>(SYSTEM_FRAMEWORK_KEY,systemFramework);
//                    //授权配置
//                    parserElement<KeyValuePair<string,string>>(PERMISSION_KEY,permission);
//                    //第三方library
//                    parserElement(THIRDLIBRARY_KEY,thirdLibrary);
//                    //第三方 framework
//                    parserElement(THIRDFRAMEWORK_KEY,thirdFramework);
//                    //添加 build setting
//                    parserElement(ADDBUILDPROPERTY_KEY,addBuildProperty);
//                    //修改 build setting
//                    parserElement(SETBUILDPROPERTY_KEY,setBuildProperty);
//                    //添加 capabilityName
//                    parserElement(CAPABILITYNAME_KEY,capabilityName);
//
//                    foreach (string s in systemLibrary)
//                    {
//                        Debug.LogFormat("{0}",s);
//                    }
//                    foreach (KeyValuePair<string,string> keyValuePair in permission)
//                    {
//                        Debug.LogFormat("key = {0} value = {1}",keyValuePair.Key,keyValuePair.Value);
//                    }
                }
            } 
        }
        
//        private void parserElement<T>(string key, ICollection<T> collection)
//        {
//            parser<T>(this.dataSoure,key, (ParserModel<T> e) =>
//            {
//                collection.Add(e.GetTypeValue());
//                return 0;
//            });
//        }
//        public void parser<T>(Hashtable dataSource, string key, Func<ParserModel<T>,int> enumera)
//        {
//            if (dataSource.ContainsKey(key))
//            {
//                
//                if (dataSource[key] is ICollection tmp)
//                {
//                    foreach (var e in tmp)
//                    {
//                        Debug.LogFormat(" e type = {0} T type = {1}",e.GetType(),typeof(T));
//                        ParserModel<T> model = new ParserModel<T>();
//                        model.SetValue(e);
//                        enumera(model);
//                    }
//                }
//                else
//                {
//                    Debug.LogFormat("unknow key = {0}",key);
//                }
//                
//            }
//        }
    }
}