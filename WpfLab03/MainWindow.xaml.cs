using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfLab03
{
    public partial class MainWindow : Window
    {
        private const string connectionString = "Data Source=LAB1504-01\\SQLEXPRESS;Initial Catalog=Laboratorio3;Integrated Security=True;";
        private List<Student> studentsList = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            LoadStudentsList();
            RefreshDataGrid();
        }

        private void LoadStudentsList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    studentsList.Add(new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"]
                    });
                }
            }
        }

        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = studentsList;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            var filteredStudents = studentsList.Where(s => s.FirstName.ToLower().Contains(searchText) || s.LastName.ToLower().Contains(searchText)).ToList();
            dataGrid.ItemsSource = filteredStudents;
        }
    }
}
