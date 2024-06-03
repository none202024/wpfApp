using Lab5.Commands;
using Lab5.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for PagePerson.xaml
    /// </summary>
    public partial class PagePerson : Page
    {
        private bool _isDirty = true;
        private bool _isReadOnly = true;
        private AppDbContext _db;

        public PagePerson()
        {
            InitializeComponent();

            _db = new AppDbContext();
            _db.Persons.Load();

            clientGrid.ItemsSource = _db.Persons.Local.ToBindingList();
        }

        //Undo
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("отмена");
            _isDirty = true;

            _db = new AppDbContext();
            clientGrid.ItemsSource = _db.Persons.Local.ToBindingList();
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
            clientGrid.IsReadOnly = !_isReadOnly;
            _db.Persons.Add(new Person());
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
                clientGrid.CommitEdit(DataGridEditingUnit.Row, false);
            }
            catch (Exception) { }

            _db.SaveChanges();

            clientGrid.IsReadOnly = _isReadOnly;

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
            clientGrid.ItemsSource = _db.Persons.Where(p => p.Inn.Contains(text) || p.Data.ToString().Contains(text) || p.Shifer.ToString().Contains(text)).ToArray();
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
            clientGrid.IsReadOnly = !_isReadOnly;
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

            if (clientGrid.SelectedItems.Count > 0)
            {
                for (int i = clientGrid.SelectedItems.Count - 1; i >= 0; i--)
                {
                    if (clientGrid.SelectedItems[i] is Person person)
                        _db.Persons.Remove(person);
                }
            }
        }
        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _isDirty;
        }
    }
}
