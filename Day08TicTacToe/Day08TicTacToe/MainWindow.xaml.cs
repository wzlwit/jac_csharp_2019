using System;
using System.Collections.Generic;
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

namespace Day08TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int movesCount = 0;
        private int currentPlayerTurn = 1; // 1 - Player 1, 2 - Player 2
        char[,] board = new char[3, 3]; // characters either code 0 for empty, 'O' or 'X'

        public MainWindow()
        {
            InitializeComponent();
            SetBoardEnabledState(false); // disable the board
        }

        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            if (tbPlayer1Name.Text == "" || tbPlayer2Name.Text == "")
            {
                MessageBox.Show("Player names must not be empty");
                return;
            }
            SetBoardEnabledState(true);
            UpdateCurrentPlayer();
            tbPlayer1Name.IsEnabled = false;
            tbPlayer2Name.IsEnabled = false;
        }

        private void UpdateCurrentPlayer()
        {
            if (currentPlayerTurn == 1)
            {
                lblPlayer1.Background = Brushes.Yellow;
                lblPlayer2.Background = null;
            }
            if (currentPlayerTurn == 2)
            {
                lblPlayer1.Background = null;
                lblPlayer2.Background = Brushes.Yellow;
            }
        }

        private void SetBoardEnabledState(bool enabled)
        {
            bt00.IsEnabled = enabled;
            bt01.IsEnabled = enabled;
            bt02.IsEnabled = enabled;
            bt10.IsEnabled = enabled;
            bt11.IsEnabled = enabled;
            bt12.IsEnabled = enabled;
            bt20.IsEnabled = enabled;
            bt21.IsEnabled = enabled;
            bt22.IsEnabled = enabled;
        }

        private void RestartGame()
        {
            bt00.Content = "";
            bt01.Content = "";
            bt02.Content = "";
            bt10.Content = "";
            bt11.Content = "";
            bt12.Content = "";
            bt20.Content = "";
            bt21.Content = "";
            bt22.Content = "";
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = (char)0; // same as '\0' but not the same as '0'
                }
            }
            movesCount = 0;
            currentPlayerTurn = 1;
            UpdateCurrentPlayer();
        }

        private bool IsPlayerWinner(char player)
        {
            // return true IF:
            // - three of 'player' in rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row,0] == player && board[row,1] == player && board[row,2] == player)
                {
                    return true;
                }
            }
            // - three of 'player' in columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0,col] == player && board[1,col] == player && board[2,col] == player)
                {
                    return true;
                }
            }
            // - two diagnoals of 'player'
            if (board[0,0] == player && board[1,1] == player && board[2,2] == player)
            {
                return true;
            }
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            {
                return true;
            }
            return false; // otherwise return false
        }

        private void BtBoard_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            // bt.Content = "Q";
            if (bt.Content.ToString() != "")
            {
                return;
            }
            // register the move - ids are in bt 'Row Column' format
            int row = int.Parse(bt.Name.Substring(2, 1)); // bt02 gives 0
            int col = int.Parse(bt.Name.Substring(3, 1)); // bt02 gives 2
            if (currentPlayerTurn == 1)
            {
                bt.Content = "O"; // view
                board[row, col] = 'O'; // model
                currentPlayerTurn = 2;                
            } else
            { // 2
                bt.Content = "X"; // view
                board[row, col] = 'X'; // model
                currentPlayerTurn = 1;
            }
            movesCount++;
            // check if there is a winner
            if (IsPlayerWinner('O'))
            {
                MessageBox.Show(String.Format("Player O ({0}) wins", tbPlayer1Name.Text));
                RestartGame();
                return;
            }
            if (IsPlayerWinner('X'))
            {
                MessageBox.Show(String.Format("Player X ({0}) wins", tbPlayer2Name.Text));
                RestartGame();
                return;
            }
            // if no winner - check if board is full - 9 moves were made
            if (movesCount == 9)
            {
                MessageBox.Show("Game over - no winner");
                RestartGame();
                return;
            }
            UpdateCurrentPlayer();
        }
    }
}
