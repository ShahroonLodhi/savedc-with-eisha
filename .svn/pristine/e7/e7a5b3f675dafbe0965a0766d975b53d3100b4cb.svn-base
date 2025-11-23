using System;
using SaveDC.ControlPanel.Src.DB;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.Managers
{
    public class ClassManager
    {
        private readonly Class_ class_;

        public ClassManager(Class_ class_)
        {
            this.class_ = class_;
        }

        public Class_[] GetClasses()
        {
            return (new ClassDAO()).GetClasses();
        }

        public Class_ Load()
        {
            try
            {
                return new ClassDAO(class_).LoadClass(class_.ClassId);
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return null;
            }
        }
    }
}