using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declares all needed variables
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

        //starts new game and sets all values to default
        private void NewGame()
        {
            //Create a new blank array of free buttons. Length is 12 to account for textblock.
            ContentResults = new ContentType[9];


            //Iterates through ContentsResults and sets all contained buttons to Free
            for (var i = 0; i < ContentResults.Length; i++)
            {
                ContentResults[i] = ContentType.Free;
            }
            //Makes sure that Player 1 starts the game
            Player1Turn = true;

            //Makes sure all button content is empty at the start of each new game
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Changes content, background and foreground to default values at the start of a new game
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Makes sure game has not finished
            GameEnded = false;
        }
        #endregion

        #region Button_Click

        /// <summary>
        /// handles button click event
        /// </summary>
        /// <param name="sender"> The button that was clicked </param>
        /// <param name="e"> The events of the click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if the game has ended, call NewGame() method again
            if (GameEnded == true)
            {
                NewGame();
                return;
            }

            //casts sender to a Button
            var button = (Button)sender; //or "sender as Button"

            //gets which column the button clicked is in
            var buttonColumn = Grid.GetColumn(button);
            //gets which row the button clicked is in
            var buttonRow = Grid.GetRow(button);
            //gets the index in ContentResults array for which button is clicked
            var arrayIndex = (buttonColumn + (buttonRow * 3)) - 3;

            //don't do anything if clicked button already has a value(has already been clicked)
            if (ContentResults[arrayIndex] != ContentType.Free)
            {
                return;
            }

            //compact if statement- if it's player 1's turn, content= X, else content=O
            ContentResults[arrayIndex] = Player1Turn ? ContentType.ContentX : ContentType.ContentO;

            //sets button content to the X or O depending on whose turn it is
            button.Content = Player1Turn ? "X" : "O";

            //changes O to red foreground
            if (!Player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //toggles player turns
            Player1Turn ^= true;

            //checks for a winner
            CheckForWinner();
        }

        #endregion

        #region CheckForWinner

        /// <summary>
        /// checks for winner
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for Horizontal wins



            //Row 0
            if (ContentResults[0] != ContentType.Free && (ContentResults[0] & ContentResults[1] & ContentResults[2]) == ContentResults[0])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button1.Background = Button2.Background = Button3.Background = Brushes.Gold;
            }



            //Row 1
            if (ContentResults[3] != ContentType.Free && (ContentResults[3] & ContentResults[4] & ContentResults[5]) == ContentResults[3])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button4.Background = Button5.Background = Button6.Background = Brushes.Gold;
            }



            //Row 2
            if (ContentResults[6] != ContentType.Free && (ContentResults[6] & ContentResults[7] & ContentResults[8]) == ContentResults[6])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button7.Background = Button8.Background = Button9.Background = Brushes.Gold;
            }
            #endregion

            #region Vertical Wins
            //Check for Vertical wins



            //Column 0
            if (ContentResults[0] != ContentType.Free && (ContentResults[0] & ContentResults[3] & ContentResults[6]) == ContentResults[0])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button1.Background = Button4.Background = Button7.Background = Brushes.Gold;
            }



            //Column 1
            if (ContentResults[1] != ContentType.Free && (ContentResults[1] & ContentResults[4] & ContentResults[7]) == ContentResults[1])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button2.Background = Button5.Background = Button8.Background = Brushes.Gold;
            }



            //Column 2
            if (ContentResults[2] != ContentType.Free && (ContentResults[2] & ContentResults[5] & ContentResults[8]) == ContentResults[2])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button3.Background = Button6.Background = Button9.Background = Brushes.Gold;
            }
            #endregion

            #region Diagonal Wins

            //Check for Diagonal wins 



            //Diagonal Down Right
            if (ContentResults[0] != ContentType.Free && (ContentResults[0] & ContentResults[4] & ContentResults[8]) == ContentResults[0])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button1.Background = Button5.Background = Button9.Background = Brushes.Gold;
            }



            //Diagonal Up Right
            if (ContentResults[2] != ContentType.Free && (ContentResults[2] & ContentResults[4] & ContentResults[6]) == ContentResults[2])
            {
                //ends game
                GameEnded = true;

                //highlight winning cells in desired color
                Button3.Background = Button5.Background = Button7.Background = Brushes.Gold;
            }

            #endregion
            #region No Wins
            //check for no winner and full Container
            if (!ContentResults.Any(result => result == ContentType.Free))
            {
                //ends game
                GameEnded = true;

                //turn all cells a desired color to signal end of no win game
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.OrangeRed;
                });
            }
            #endregion
        }

        #endregion
    }
}
