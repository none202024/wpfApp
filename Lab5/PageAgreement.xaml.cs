using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for PageAgreement.xaml
    /// </summary>
    public partial class PageAgreement : Page
    {
        private bool _isDirty = true;
        private bool _isReadOnly = true;
        private AppDbContext _db;
        public PageAgreement()
        {
            InitializeComponent();

            _db = new AppDbContext();
            _db.Agreements.Include(p => p.Person).Include(a => a.StatusAgreement).Include(a => a.TypeAgreement).Load();

            agrGrid.ItemsSource = _db.Agreements.Local.ToBindingList();
        }

        //Undo
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("отмена");
            _isDirty = true;

            _db = new AppDbContext();

            CommonHelper.RefreashData(_db.Agreements, agrGrid);
        }

        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_isDirty;
        }



        //New
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("new");
            _isDirty = false;
            agrGrid.IsReadOnly = !_isReadOnly;
            _db.Agreements.Add(new Agreement());
        }
        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isDirty;
        }


        //Save
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Save");
            _isDirty = true;

            //Для того чтобы закоммитить изименения в датагриде и отправить их в БД, иначе изменения не будут приняты
            try
            {
                agrGrid.CommitEdit(DataGridEditingUnit.Row, false);
            }
            catch (Exception) { }

            _db.SaveChanges();

            agrGrid.IsReadOnly = _isReadOnly;

        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_isDirty;
        }



        //Find
        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Find");
            _isDirty = false;

            var text = SearchField.Text;
            agrGrid.ItemsSource = _db.Agreements.Where(a => a.Person.Inn.Contains(text) || a.TypeAgreement.TypeName.ToString().Contains(text) || a.StatusAgreement.Status.ToString().Contains(text)).ToArray();
        }
        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isDirty;
        }



        //Edit
        private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Edit");
            _isDirty = false;
            agrGrid.IsReadOnly = !_isReadOnly;
        }
        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isDirty;
        }



        //Delete
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Delete");
            _isDirty = false;

            if (agrGrid.SelectedItems.Count > 0)
            {
                for (int i = agrGrid.SelectedItems.Count - 1; i >= 0; i--)
                {
                    if (agrGrid.SelectedItems[i] is Agreement agr)
                        _db.Agreements.Remove(agr);
                }
            }
        }
        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isDirty;
        }
    }
}
