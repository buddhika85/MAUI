using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIClient.Models
{
    public class ToDo : INotifyPropertyChanged
    {
		private int _id;

		public int Id
        {
            get => _id;
            set 
            { 
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        private string _toDoName = null!;

        public string ToDoName
        {
            get => _toDoName;
            set 
            { 
                _toDoName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
