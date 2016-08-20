using System;

namespace CodingActivity_TheNumberGuessingGame.Solution
{
    class Program
    {
        ///
        /// Declare game variables
        ///
        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;
        private const int MAX_NUMBER_TO_GUESS = 10;

        private static int playersGuess;
        private static int numberToGuess;
        private static int roundNumber;
        private static int numberOfWins;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];

        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;

        /// <summary>
        /// Application's Main method
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //
            // Initialize new game
            //
            InitializeGame();

            //
            // Display the Welcome Screen with application Quit option
            //
            DisplayWelcomeScreen();

            //
            // Display the game rules
            //
            DisplayRulesScreen();

            //
            // Game loop
            // 
            while (playingGame)
            {
                //
                // Initialize new round
                //
                InitializeRound();

                //
                // Round loop
                // 
                while (playingRound)
                {
                    //
                    // Display the player guess screen and return the player's guess
                    //
                    playersGuess = DisplayGetPlayersGuessScreen();

                    //
                    // Evaluate the player's guess and provide the player feedback
                    //
                    DisplayPlayerGuessFeedback();

                    //
                    // Update round variables, process the results and provide player feedback
                    //
                    UpdateAndDisplayRoundStatus();
                }

                //
                // Round complete, display player stats and prompt to Continue/Quit
                //
                DisplayPlayerStats();
            }

            DisplayClosingScreen();
        }

        /// <summary>
        /// Initialize all game variables
        /// </summary>
        private static void InitializeGame()
        {
            numberToGuess = 0;
            roundNumber = 0;
            numberOfWins = 0;
            playingGame = true;
        }

        /// <summary>
        /// Initialize all round variables and get number to guess
        /// </summary>
        private static void InitializeRound()
        {
            //
            // Initialize round variables
            //
            playingRound = true;
            numberGuessedCorrectly = false;
            numberOfCurrentPlayerGuess = 1;

            //
            // Initialize the array of player guesses with zeros
            //
            for (int i = 0; i < MAX_NUMBER_OF_PLAYER_GUESSES; i++)
            {
                numbersPlayerHasGuessed[i] = 0;
            }

            //
            // Increment round number
            //
            roundNumber = roundNumber++;

            //
            // Get a random number
            //
            numberToGuess = GetNumberToGuess();
        }

        /// <summary>
        /// Display the opening screen and prompt to Continue/Quit
        /// </summary>
        private static void DisplayWelcomeScreen()
        {
            ConsoleKeyInfo playerKeyResponse;

            Console.Clear();

            Console.WriteLine("\n\n");
            Console.WriteLine("     Welcome to The Number Guessing Game");
            Console.WriteLine("        Laughing Leaf Productions");
            Console.WriteLine("\n\n");
            Console.WriteLine("      Press the (Enter) key to Play.");
            Console.WriteLine("       Press the (Esc) key to Quit.");

            playerKeyResponse = Console.ReadKey();

            //
            // Note: Console window closes immediately without displaying the closing screen.
            //
            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Display the game rules
        /// </summary>
        private static void DisplayRulesScreen()
        {
            Console.Clear();

            Console.WriteLine("                The Number Guessing Game");
            Console.WriteLine("\n\n");
            Console.WriteLine("The computer will randomly select a number between 1 and 10.");
            Console.WriteLine("You will have four attempts to guess the number. After each");
            Console.WriteLine("guess the computer will indicate if you have guessed correctly");
            Console.WriteLine("or whether your guess is high or low.");
            Console.WriteLine("\n\n");
            Console.WriteLine("Press the any key to continue.");

            Console.ReadLine();
        }

        /// <summary>
        /// Prompt for and return the player's guess
        /// </summary>
        /// <returns></returns>
        private static int DisplayGetPlayersGuessScreen()
        {
            string playerResponse;
            int playersGuess;
            bool validResponse = false;
            Console.Clear();

            //
            // Validate player's guess
            //
            while (!validResponse)
            {
                //
                // Clear screen and set header
                //
                DisplayReset();

                Console.Write("Enter your guess as a number between 1 and {0}: ", MAX_NUMBER_TO_GUESS);
                playerResponse = Console.ReadLine();

                //
                // Test if player's response is an integer
                // Note: The player does have the option to quit the game via the DisplayContinueQuitPrompt method
                //
                if (int.TryParse(playerResponse, out playersGuess))
                {
                    //
                    // Test if the player's response is within the desired range
                    //
                    if (playersGuess >= 1 && playersGuess <= MAX_NUMBER_TO_GUESS)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("It appears that you did not enter a number within the proper range.");
                        Console.WriteLine("Please try again.");

                        DisplayContinueQuitPrompt();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("It appears that you did not enter a valid integer.");
                    Console.WriteLine("Please try again.");

                    DisplayContinueQuitPrompt();
                }
            }
            return Program.playersGuess;
        }

        /// <summary>
        /// Evaluate the player's guess and provide the player feedback
        /// </summary>
        private static void DisplayPlayerGuessFeedback()
        {
            if (playersGuess == numberToGuess)
            {
                numberGuessedCorrectly = true;
                Console.WriteLine("The number you guessed, " + playersGuess + ", is correct!.");
            }
            else if (playersGuess > numberToGuess)
            {
                Console.WriteLine("The number you guessed, " + playersGuess + ", is too high.");
            }
            else
            {
                Console.WriteLine("The number you guessed, " + playersGuess + ", is too low.");
            }
        }

        /// <summary>
        /// Update round status, process the results and provide player feedback
        /// </summary>
        private static void UpdateAndDisplayRoundStatus()
        {
            //
            // Player guessed correctly
            // 
            if (numberGuessedCorrectly)
            {
                numberOfWins = numberOfWins + 1;
                playingRound = false;

                Console.WriteLine("Press the (Enter) key to see your current stats.");
            }
            //
            // Player guessed incorrectly and has more guesses left
            // 
            else if (numberOfCurrentPlayerGuess < MAX_NUMBER_OF_PLAYER_GUESSES)
            {
                numbersPlayerHasGuessed[numberOfCurrentPlayerGuess - 1] = playersGuess;

                DisplayNumbersGuessed();

                numberOfCurrentPlayerGuess = numberOfCurrentPlayerGuess + 1;

                Console.WriteLine("Press the (Enter) key to guess another number.");
            }
            //
            // Player guessed incorrectly and has no more guesses left
            // 
            else
            {
                playingRound = false;

                Console.WriteLine("I am afraid you have used all " + MAX_NUMBER_OF_PLAYER_GUESSES + " of your guesses.\n");
                Console.WriteLine("Press the (Enter) key to see your current stats.");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Display the player's current stats and prompt to Continue/Quit
        /// </summary>
        private static void DisplayPlayerStats()
        {
            ConsoleKeyInfo playerKeyResponse;

            //
            // Calculate the Number of Losses
            //
            int numberOfLosses = roundNumber - numberOfWins;

            //
            // Calculate the Percentage of Wins
            //
            double winRate = ((double)numberOfWins / roundNumber) * 100;
            int percentageOfWins = (int)Math.Round(winRate);

            //
            // Display the players current stats
            //
            DisplayReset();
            Console.WriteLine("You current stats:\n");
            Console.WriteLine("Number of Rounds Played : " + roundNumber);
            Console.WriteLine("Number of Wins          : " + numberOfWins);
            Console.WriteLine("Number of Losses        : " + numberOfLosses);
            Console.WriteLine("Percentage of Wins      : " + percentageOfWins + "%");
            Console.WriteLine("\n\n");

            //
            // Prompt to continue or quit
            //
            Console.WriteLine("Press the (Enter) key to play another round.");
            Console.WriteLine("Press the (Esc) key to Quit.");
            playerKeyResponse = Console.ReadKey();

            //
            // set flag if player wants to quit
            //
            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                playingGame = false;
            }
        }

        /// <summary>
        /// Display the numbers already guessed
        /// </summary>
        private static void DisplayNumbersGuessed()
        {
            Console.WriteLine("The numbers you have currently guessed include:");
            Console.WriteLine();
            for (int guess = 0; guess < MAX_NUMBER_OF_PLAYER_GUESSES; guess++)
            {
                if (numbersPlayerHasGuessed[guess] != 0)
                {
                    Console.WriteLine("Guess {0}: {1}", guess + 1, numbersPlayerHasGuessed[guess]);
                }

            }
            Console.WriteLine();
        }

        /// <summary>
        /// Display the closing screen
        /// </summary>
        private static void DisplayClosingScreen()
        {
            //
            // Clear screen and set header
            //
            DisplayReset();

            Console.WriteLine("Thank you for playing our Number Guessing Game.\n");
            Console.WriteLine("          Laughing Leaf Productions.\n");
            Console.WriteLine("         Press the (Enter) key to Exit.");

            Console.ReadLine();

            Environment.Exit(0);
        }

        /// <summary>
        /// Generate and return a random number 
        /// </summary>
        /// <returns>random integer in desired range</returns>
        private static int GetNumberToGuess()
        {
            int numberToGuess;
            Random randomNumber = new Random();

            numberToGuess = randomNumber.Next(1, MAX_NUMBER_TO_GUESS);

            return numberToGuess;
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.SetCursorPosition(15, 1);
            Console.WriteLine("   The Number Guessing Game   ");
            Console.WriteLine();

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue/Quit prompt
        /// </summary>
        public static void DisplayContinueQuitPrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine("Press any key to continue or press the ESC key to quit.");
            Console.WriteLine();
            ConsoleKeyInfo response = Console.ReadKey();

            //
            // Set flag if player chooses to quit
            //
            if (response.Key == ConsoleKey.Escape)
            {
                playingGame = false;
            }

            Console.CursorVisible = true;
        }
    }
}
