using System.Windows;

namespace Notes
{
    /// <summary>
    /// Interaction logic for NameWindow.xaml
    /// </summary>
    public partial class NameWindow : Window
    {
        private Note note;

        public NameWindow(Note note)
        {
            InitializeComponent();
            this.note = note;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            note.NoteName = tbName.Text;
            note.NoteDescription = " ";

            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
