using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqlLiteAndEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            List = new ObservableCollection<EmployeeMaster>(GetList());
            DataContext = this;
        }
        private ObservableCollection<EmployeeMaster> _list;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }


        public ObservableCollection<EmployeeMaster> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyRaised("List");
            }
        }
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            DatabaseContext context = new DatabaseContext();
            EmployeeMaster employee = new EmployeeMaster()
            {
                EmpName = txtName.Text,
                Designation = txtDig.Text,
                Salary = Convert.ToDouble(txtsalary.Text)
            };
            context.EmployeeMaster.Add(employee);
            context.SaveChanges();

            GetList();
        }
        private ObservableCollection<EmployeeMaster> GetList()
        {
            DatabaseContext context = new DatabaseContext();
            List = new ObservableCollection<EmployeeMaster>(context.EmployeeMaster.ToList());
            return List;

        }
    }
}
