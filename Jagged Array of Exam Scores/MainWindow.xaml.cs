using System.Windows;

namespace Jagged_Array_of_Exam_Scores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JaggedArrayOfExamScores jaggedArrayOfExamScores;

        public MainWindow()
        {
            InitializeComponent();
            jaggedArrayOfExamScores = new JaggedArrayOfExamScores();
            DataContext = jaggedArrayOfExamScores;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (jaggedArrayOfExamScores.ReadExamScoresFromFile())
            {
                lblError.Visibility = Visibility.Hidden;
                jaggedArrayOfExamScores.RefreshExamScores();
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
            }
        }
    }
}
