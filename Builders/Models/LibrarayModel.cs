using System.Collections;

namespace QYPBXEditTool
{
    public class LibrarayModel
    {
        private Hashtable m_libraryData;
        private Hashtable m_framework;

        public LibrarayModel(Hashtable libData, Hashtable frameworkData)
        {
            this.m_framework = frameworkData;
            this.m_libraryData = libData;
        }

        public ArrayList GetSysLibs()
        {
            return m_libraryData["system"] as ArrayList;
        }

        public ArrayList GetThirdLibs()
        {
            return m_libraryData["third"] as ArrayList;
        }

        public ArrayList GetSysFramework()
        {
            return m_framework["system"] as  ArrayList;
        }

        public ArrayList GetThirdFrameWork()
        {
            return m_framework["third"] as ArrayList;
        }
    }
}