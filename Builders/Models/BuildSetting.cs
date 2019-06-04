using System.Collections;

namespace QYPBXEditTool
{
    public class BuildSetting
    {
        private Hashtable buildModel;

        public BuildSetting(Hashtable data)
        {
            this.buildModel = data;
        }

        public Hashtable GetAddData()
        {
            return this.buildModel["add"] as Hashtable;
        }

        public Hashtable GetMdfyData()
        {
            return this.buildModel["mdfy"] as  Hashtable;
        }

    }
}