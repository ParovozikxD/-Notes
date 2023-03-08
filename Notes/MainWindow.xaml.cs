using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RichTextBox currentNoteSpace;
        private int currentNoteID;
        private bool IsNoteChanged;

        public MainWindow()
        {
            InitializeComponent();
            InitializeNotes();
            InitialComboboxFontSize();
        }
        private void InitialComboboxFontSize()
        {
            cbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void InitializeNotes()
        {
            foreach (var item in DataBase.GetAll())
            {
                tcNotesList.Items.Add(new TabItem()
                {
                    Header = item.NoteName,
                    Content = LoadNote(item.NoteID)
                });
            }
        }

        private RichTextBox LoadNote(int nodeID)
        {
            var note = DataBase.GetNote(nodeID);

            if (note != null)
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(note.NoteDescription);

                RichTextBox noteSpace = new RichTextBox();

                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    var textRange = new TextRange(noteSpace.Document.ContentStart, noteSpace.Document.ContentEnd);
                    textRange.Load(ms, DataFormats.Rtf);
                }

                return noteSpace;
            }
            else
            {
                throw new Exception("Missing notes");
            }
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note();

            NameWindow nameWindow = new NameWindow(newNote);
            nameWindow.ShowDialog();

            if (!string.IsNullOrEmpty(newNote.NoteName))
            {
                DataBase.AddNote(newNote);

                tcNotesList.Items.Add(new TabItem()
                {
                    Header = newNote.NoteName,
                    Content = LoadNote(newNote.NoteID)
                });
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string newDescription;

            using (MemoryStream ms = new MemoryStream())
            {
                var textRange = new TextRange(currentNoteSpace.Document.ContentStart, currentNoteSpace.Document.ContentEnd);

                textRange.Save(ms, DataFormats.Rtf);

                newDescription = Encoding.ASCII.GetString(ms.ToArray());
            }

            DataBase.UpdateNote(currentNoteID, newDescription);

            IsNoteChanged = false;
        }

        private void tcNotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentNoteSpace != null)
            {
                currentNoteSpace.TextChanged -= CurrentNoteSpace_TextChanged;
            }

            if (IsNoteChanged)
            {
                var result = MessageBox.Show("Данные не сохранены! Сохранить?", "Notes", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    btnSave_Click(sender, e);
                    IsNoteChanged = false;
                }
                else if (result == MessageBoxResult.No)
                {
                    IsNoteChanged = false;
                }
            }

            currentNoteSpace = (RichTextBox)tcNotesList.SelectedContent;
            currentNoteID = tcNotesList.SelectedIndex + 1;

            currentNoteSpace.TextChanged += CurrentNoteSpace_TextChanged;
        }

        private void CurrentNoteSpace_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsNoteChanged = true;
        }

        private void cbFontFamilies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFontFamilies.SelectedItem != null)
                currentNoteSpace.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cbFontFamilies.SelectedItem);
        }

        private void cbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFontSize.SelectedItem != null)
                currentNoteSpace.Selection.ApplyPropertyValue(FontSizeProperty, cbFontSize.SelectedItem);
        }
    }
}
