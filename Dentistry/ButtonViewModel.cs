using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dentistry
{
    public class ButtonViewModel
    {
        public string Content { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsEnable { get; set; }

        public ICommand Command;
        public ButtonViewModel(string content, int row = 0, int column = 0, ICommand command = null)
        {
            Content = content;
            Row = row;
            Column = column;
            Command = command;
        }
    }
}
