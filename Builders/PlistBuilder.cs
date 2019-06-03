using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace QYPBXEditTool
{
    public class PlistBuilder
    {
        private List<PlistModel> m_models;
        public PlistBuilder(List<PlistModel> models)
        {
            this.m_models = models;
        }

        public void Builder(string targetPath)
        {
            string plistInfoPath = targetPath + "/Info.plist";
            PlistDocument plistDocument = new PlistDocument();
            plistDocument.ReadFromFile(plistInfoPath);
            PlistElementDict rootDic = plistDocument.root.AsDict();
            m_models.ForEach(o =>
            {
                Hashtable addData = o.GetAddData();
                Hashtable mdfyData = o.GetMdfyData();
                Debug.LogFormat("star mdfy addData");
                BuilderData(rootDic,addData);
                Debug.LogFormat("end mdfy addData");
                Debug.LogFormat("star mdfy mdfyData");
                BuilderData(rootDic,mdfyData);
                Debug.LogFormat("end mdfy mdfyData");
            });
            plistDocument.WriteToFile(plistInfoPath);
        }

        private void BuilderData(PlistElementDict plistElement, Hashtable data)
        {
            foreach (DictionaryEntry o in data)
            {
                if (o.Value is ArrayList arrayList)
                {
                    
                    //数组
                    foreach (var o1 in arrayList)
                    {
                        PlistElementArray elementArray =   plistElement.CreateArray(o.Key.ToString());
                        AddArrayData(elementArray,arrayList);
                    }
                }
                else if (o.Value is IDictionary dictionary)
                {
                    // 字典
                    PlistElementDict elementDict = plistElement.CreateDict(o.Key.ToString());
                    AddDictionaryData(elementDict,dictionary);
                }
                else
                {
                    //普通元素
                    SetDataForKey(o.Key,o.Value,plistElement);
                }
            }
        }
        
        private void SetDataForKey(object key, object value,PlistElementDict elementDict)
        {
            if (value is int)
            {
                elementDict.SetInteger(key.ToString(),Convert.ToInt32(value));
                
            } else if (value is bool)
            {
                elementDict.SetBoolean(key.ToString(),Convert.ToBoolean(value));
                
            } else if (value is string)
            {
                elementDict.SetString(key.ToString(),value.ToString());
            } else if (value is float)
            {
                elementDict.SetReal(key.ToString(),Convert.ToSingle(value));
            }
        }

        private void AddDataToArray(object value, PlistElementArray elementArray)
        {
            if (value is int)
            {
                elementArray.AddInteger(Convert.ToInt32(value));
                
            } else if (value is bool)
            {
                elementArray.AddBoolean(Convert.ToBoolean(value));
                
            } else if (value is string)
            {
                elementArray.AddString(value.ToString());
                
            } else if (value is float)
            {
                elementArray.AddReal(Convert.ToSingle(value));
            }
        }

        private void AddArrayData(PlistElementArray elementArray, ICollection data)
        {
            foreach (var o in data)
            {
                if (o is ArrayList arrayList)
                {
                    PlistElementArray array =  elementArray.AddArray();
                    AddArrayData(array,arrayList);
                    
                } 
                else if (o is IDictionary dictionary)
                {
                    PlistElementDict dic = elementArray.AddDict();
                    AddDictionaryData(dic,dictionary);
                }
                else
                {
                   AddDataToArray(o,elementArray);
                }
            }
        }

        private void AddDictionaryData(PlistElementDict elementDict, IDictionary dictionary)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                if (entry.Value is ArrayList arrayList)
                {
                    PlistElementArray elementArray = elementDict.CreateArray(entry.Key.ToString());
                    AddArrayData(elementArray,arrayList);
                } else if (entry.Value is IDictionary dic)
                {
                    PlistElementDict eDict = elementDict.CreateDict(entry.Key.ToString());
                    AddDictionaryData(eDict,dic);
                }
                else
                {
                    SetDataForKey(entry.Key,entry.Value,elementDict);
                }
            }
        }
    }
    
   
}