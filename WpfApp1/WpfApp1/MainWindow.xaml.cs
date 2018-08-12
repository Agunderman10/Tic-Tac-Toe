using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        //Holds the current results of buttons in the active game
        private ContentType[] ContentResults;

        //True if it is player 1's turn(X) or player 2's turn (O)
        private bool Player1Turn;

        //True if the game has ended
        private bool GameEnded;

        #endregion

        #region Initial Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        #region NewGame
        private void NewGame()
        {
            //Create a new blank array of free buttons
            ContentResults = new ContentType[9];


            //Iterates through ContentsResults and sets all contained buttons to Free
            for (var i = 0; i < ContentResults.Length; i++)
            {
                ContentResults[i] = ContentType.Free;
            }
            //Makes sure that Player 1 starts the game
            Player1Turn = true;

            //Integrate all buttons on the UI grid
            Container.Children.Cast<Button>().ToList().ForEach(Button => 
            {
                Button.Content = string.Empty;
            });
        }
        #endregion
    }
}
