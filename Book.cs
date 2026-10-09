using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kaigorodov_TRPO_PR4
{
    public class Book : INotifyPropertyChanged
    {
        public int _id = 0;

        public string _name = "Название книги";

        public string _author = "Автор";

        public int _year = DateTime.Now.Year;

        public int _page = 100;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public int Page
        {
            get => _page;
            set
            {
                if (_page != value)
                {
                    _page = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Age => DateTime.Now.Year - Year;
    }
}
