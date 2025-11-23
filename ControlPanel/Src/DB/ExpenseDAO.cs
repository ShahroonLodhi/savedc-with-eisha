using System;
using System.Data;
using System.Data.SqlClient;
using SaveDC.ControlPanel.Src.Configurations;
using SaveDC.ControlPanel.Src.Managers;
using SaveDC.ControlPanel.Src.Objects;
using SaveDC.ControlPanel.Src.Utils;

namespace SaveDC.ControlPanel.Src.DB
{
    public class ExpenseDAO
    {
        private readonly Fund expense;

        public ExpenseDAO()
        {
        }

        //public ExpenseDAO(Fund oexpense)
        //{
        //    expense = oexpense;
        //}

        public Int32 RecordCount { get; set; }

        
    }
}