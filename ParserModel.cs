using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QYPBXEditTool
{
    public class ParserModel<T>
    {
        private object m_value;

        public T GetTypeValue()
        {
            return (T) (this.m_value);
        }

        public void SetValue(object value)
        {
            if (typeof(T) == typeof(KeyValuePair<string,string>) )
            {
                //将字典key value 装换成 KeyValuePair
                DictionaryEntry entry = (DictionaryEntry) value;
                m_value = new KeyValuePair<string, string>(entry.Key.ToString(), entry.Value.ToString());
            }
            else
            {
                m_value = value;
            }
        }
    }
}