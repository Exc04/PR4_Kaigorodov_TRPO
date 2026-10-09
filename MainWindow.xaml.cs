using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Kaigorodov_TRPO_PR4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Folder = "Data";
        private static readonly Random _rnd = new();

        private int GenerateID()
        {
            int id;
            do
            {
                id = _rnd.Next(1, int.MaxValue);
            }
            while (File.Exists(Path.Combine(Folder, $"{id}.json")));

            return id;
        }

        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(Folder);

            var book = (Book)Resources["CurrentBook"];
            book.Id = GenerateID();
        }

        private string GetFilePath(int id) => Path.Combine(Folder, $"{id}.json");

        private void ResetBook(object sender, RoutedEventArgs e)
        {
            var currentBook = (Book)Resources["CurrentBook"];
            currentBook.Id = 0;
            currentBook.Name = "Название книги";
            currentBook.Author = "Автор";
            currentBook.Year = DateTime.Now.Year;
            currentBook.Page = 100;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SearchBox.Text, out int id))
            {
                MessageBox.Show("Введите корректный ID.");
                return;
            }
            
            string path = GetFilePath(id);

            if (!File.Exists(path))
            {
                MessageBox.Show($"Книга с Id = {id} не найдена");
                return;
            }

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<Book>(json);

            var book = (Book)Resources["CurrentBook"];
            book.Id = loaded.Id;
            book.Name = loaded.Name;
            book.Author = loaded.Author;
            book.Year = loaded.Year;
            book.Page = loaded.Page;

            MessageBox.Show($"Книга с id = {id} Загружена");
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            var book = (Book)Resources["CurrentBook"];
            book.Id = GenerateID();
            book.Name = "Название книги";
            book.Author = "Автор";
            book.Year = DateTime.Now.Year;
            book.Page = 100;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var book = (Book)Resources["CurrentBook"];
            var json = JsonSerializer.Serialize(book, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetFilePath(book.Id), json);

            MessageBox.Show($"Книга сохранена в файл {book.Id}.json");
        }
    }
}