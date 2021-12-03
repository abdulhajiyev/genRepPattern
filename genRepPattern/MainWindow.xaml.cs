using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using genRepPattern.DataAccess.Abstract;
using genRepPattern.DataAccess.Concrete;
using genRepPattern.DataAccess.Context;

namespace genRepPattern
{
    public partial class MainWindow : Window
    {
        GenericRepositoryPattern<Author> _repository;
        public ObservableCollection<Author> Data { get; set; }


        public Author SelectedItem
        {
            get { return (Author)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Author), typeof(MainWindow));



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new GenericRepositoryPattern<Author>();
            Data = new ObservableCollection<Author>(_repository.GetAll().ToList());
            ListView.DisplayMemberPath = "FirstName";

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _repository.SubmitChanges();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            var temp = _repository.GetAll().OrderBy(a => a.Id).ToList();

            var window1 = new Window1();
            window1.ShowDialog();

            var auth = new Author
            {
                FirstName = window1.FirstName,
                LastName = window1.LastName,
                Id = temp[temp.Count - 1].Id + 1
            };
            Data.Add(auth);
            _repository.SubmitChanges();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Data.Remove(SelectedItem);
            _repository.SubmitChanges();
        }
    }
}
