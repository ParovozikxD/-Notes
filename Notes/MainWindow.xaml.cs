using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NoteContext noteContext;

        public MainWindow()
        {
            InitializeComponent();

            noteContext = new NoteContext();
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            tcNotesList.Items.Add("sda");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string rtfText;

            using (MemoryStream ms = new MemoryStream())
            {
                var textRange = new TextRange(rtbNoteBox.Document.ContentStart, rtbNoteBox.Document.ContentEnd);
                textRange.Save(ms, DataFormats.Rtf);

                rtfText = Encoding.ASCII.GetString(ms.ToArray());
            }
          
            noteContext.Notes.Add(new Note("test", rtfText));

            noteContext.SaveChanges();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            var a = noteContext.Notes.OrderByDescending(e => e.NoteID).Take(1).FirstOrDefault();

            byte[] byteArray = Encoding.ASCII.GetBytes(a.NoteDescription);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(rtbNoteBox.Document.ContentStart, rtbNoteBox.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }

            
        }
    }
}
