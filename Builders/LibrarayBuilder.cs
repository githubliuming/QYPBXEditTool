using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace QYPBXEditTool
{
    public class LibrarayBuilder
    {
        private List<LibrarayModel> m_librarys;
        private PBXProject proj;
        private string target;

        public LibrarayBuilder(List<LibrarayModel> models)
        {
            this.m_librarys = models;
        }

        public void Builder(PBXProject project,string target)
        {
            this.proj = project;
            this.target = target;
            
            m_librarys.ForEach(o =>
            {
                //添加library
                BuilderSysLibrary(o.GetSysLibs());
                BuilderThirdLibrary(o.GetThirdLibs());
                //添加 framework
                BuilderSysFramework(o.GetSysFramework());
                BuilderThirdFramework(o.GetThirdFrameWork());
            });
            
            
        }

        public void BuilderSysLibrary(ArrayList data)
        {
            foreach (var o in data)
            {
                Debug.LogFormat("library = {0}",o.ToString());
                AddLibrary(o.ToString());
            }
        }
        

        public void BuilderThirdLibrary(ArrayList data)
        {
            
        }

        public void BuilderSysFramework(ArrayList data)
        {
            foreach (var o in data)
            {
                this.proj.AddFrameworkToProject(this.target,o.ToString(),true);
            }
        }

        public void BuilderThirdFramework(ArrayList data)
        {
            
        }

        private void AddLibrary(string libName)
        {
            
            string fileGuid = this.proj.AddFile("usr/lib/" + libName, "Frameworks/" + libName, PBXSourceTree.Sdk);
            this.proj.AddFileToBuild(this.target, fileGuid);
        }
    }
}