using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class CommonHelper
    {
        public static void RefreashData<T>(DbSet<T> entities, System.Windows.Controls.DataGrid dataGrid)
            where T : class
        {
            entities.Load();
            dataGrid.ItemsSource = entities.Local.ToBindingList();
        }
    }
}
