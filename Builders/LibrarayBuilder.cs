using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;

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
        }

        public void BuilderLibrary(Hashtable data)
        {
            
        }

        public void BuilderFramework(Hashtable data)
        {
            
        }
    }
}