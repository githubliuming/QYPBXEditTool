using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEditor.iOS.Xcode;

namespace QYPBXEditTool
{
    public class SettingBuilder
    {
        private List<BuildSetting> m_settingModels;
        private PBXProject m_proj;
        private string m_targetId;
        

        public SettingBuilder(List<BuildSetting> models)
        {
            this.m_settingModels = models;
        }

        public void Builder(PBXProject proj, string targerId)
        {
            this.m_proj = proj;
            this.m_targetId = targerId;
            m_settingModels.ForEach(o =>
            {
                SetBuildProperty(o.GetMdfyData());
                AddBuilderProperty(o.GetAddData());
                
            });
        }
        private void SetBuildProperty(Hashtable data)
        {
            enumeraData(data, (key, value) =>
            {
                this.m_proj.SetBuildProperty(this.m_targetId,key,value);
                return 0;
            });
        }

        private void AddBuilderProperty(Hashtable data)
        {
            enumeraData(data, (key, value) =>
            {
                this.m_proj.AddBuildProperty(this.m_targetId,key,value);
                return 0;
            });
        }

        private void enumeraData(Hashtable data, Func<string, string,int> callBack)
        {
            foreach (DictionaryEntry o in data)
            {
                if (o.Value is ArrayList arrayList)
                {
                    foreach (var p in arrayList)
                    {
                        callBack(o.Key.ToString(), p.ToString());

                    }
                }
                else
                {
                    callBack(o.Key.ToString(), o.Value.ToString());
                }
            }
        }

    }
}