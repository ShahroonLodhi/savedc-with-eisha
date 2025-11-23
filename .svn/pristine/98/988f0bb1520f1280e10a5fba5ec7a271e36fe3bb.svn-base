using System;
using SaveDC.ControlPanel.Src.DB;
using SaveDC.ControlPanel.Src.Objects;

namespace SaveDC.ControlPanel.Src.Managers
{
    public class HistoryManager
    {
        private readonly History history;

        public HistoryManager(History history)
        {
            this.history = history;
        }

        public History Load()
        {
            try
            {
                return new HistoryDAO(history).LoadHistory(history.HistoryId);
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return null;
            }
        }

        public int Save()
        {
            try
            {
                var historyDAO = new HistoryDAO(history);
                return historyDAO.AddHistory();
            }
            catch (Exception e)
            {
                Utils.Utils.LogErrorToFile(e);
                return 0;
            }
        }

        //public bool Exists()
        //{
        //    try
        //    {
        //        return new HistoryDAO(history).HistoryExists();
        //    }
        //    catch (Exception e)
        //    {
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}

        //public bool Delete()
        //{
        //    try
        //    {
        //        HistoryDAO historyDAO = new HistoryDAO(history);
        //        return historyDAO.DeleteHistory();
        //    }
        //    catch (Exception e)
        //    {
        //        RecordID = 0;
        //        Utils.Utils.LogErrorToFile(e);
        //        return false;
        //    }
        //}
    }
}