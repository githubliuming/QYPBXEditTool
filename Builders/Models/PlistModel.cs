using System.Collections;

namespace QYPBXEditTool
{
    public class PlistModel
    {
        private const string PLISTINFO_ADD_KEY = "add";
        private const string PLISTINFO_MDFY_KEY = "mdfy";
        public Hashtable plistModel { get; set; }

        public PlistModel(Hashtable data)
        {
            this.plistModel = data;
        }

        public Hashtable GetAddData()
        {
            return plistModel[PLISTINFO_ADD_KEY] as Hashtable;
        }

        public Hashtable GetMdfyData()
        {
            return plistModel[PLISTINFO_MDFY_KEY] as Hashtable;
        }
    }
}