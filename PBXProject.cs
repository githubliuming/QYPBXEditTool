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
           
           string path = PBXProject.GetPBXProjectPath(targetPath);
           PBXProject proj = new PBXProject();
           proj.ReadFromFile(path);
           string targetId = proj.TargetGuidByName("Unity-iPhone");
           
           //加载配置数据
           ConfigLoader loader = new ConfigLoader();
           loader.LoadConfig();
           //配置 infoPlist
           new PlistBuilder(loader.plistModes).Builder(targetPath);
           //添加library framework
           new LibrarayBuilder(loader.libModels).Builder(proj,targetId);
           
           proj.WriteToFile(path);
           
           Debug.Log("end mdfy project");
        }
    }
}