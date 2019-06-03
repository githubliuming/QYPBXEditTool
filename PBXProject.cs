using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace QYPBXEditTool
{
    public class PBXProjectTool
    {
        [PostProcessBuildAttribute(12)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string targetPath)
        {
            if (BuildTarget.iOS != buildTarget)
            {
                Debug.LogFormat(" is not ios platform");
                return;
            } 
           Debug.Log("start mdfy project");
           
           ConfigLoader loader = new ConfigLoader();
           loader.LoadConfig();
           
           new PlistBuilder(loader.plistModes).Builder(targetPath);
           
           Debug.Log("end mdfy project");
        }
    }
}