using System;

namespace GuessTheNumberGame.Solution
{
    class Program
    {
        ///
        /// Declare and initialize game variables
        /// (Instructor Note: All C# variable values are initialized for clarity.)
        ///
        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;

        private static int playersGuess = 0;
        private static int numberToGuess = 0;
        private static int roundNumber = 0;
        private static int numberOfWins = 0;
        private static int numberOfCurrentPlayerGuess = 1;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];

        private static bool playingGame = true;
        private static bool playingRound = false;
        private static bool numberGuessedCorrectly = false;

        /// <summary>
        /// Application's Main method
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //
            // Display the Welcome Screen with application Quit option
            //
            DisplayWelcomeScreen();

            //
            /// Display the game rules
            //
            DisplayRules();

            ///
            /// Game loop
            /// 
            while (playingGame)
            {
                //
                // Setup new round to play
                //
                SetupRound();

                //
                // Round loop
                // 
                while (playingRound)
                {


                    //
                    // Display the player guess screen and return the player's guess
                    //
                    playersGuess = GetPlayersGuess();

                    //
                    // Evaluate the player's guess and provide the player feedback
                    //
                    EvaluatePlayerGuess();

                    //
                    // Update round variables, process the results and provide player feedback
                    //
                    UpdateRoundStatus();
                }

                //
                // Round complete, display player stats and prompt to Continue/Quit
                //
                DisplayPlayerStats(roundNumber, numberOfWins);
            }
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

            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Display the game rules
        /// </summary>
        private static void DisplayRules()
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
        /// Update round variables, process the results and provide player feedback
        /// </summary>
        private static void UpdateRoundStatus()
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

                DisplayNumbersGuessed(numbersPlayerHasGuessed, MAX_NUMBER_OF_PLAYER_GUESSES);

                Console.WriteLine("Press the (Enter) key to guess another number.");

                numberOfCurrentPlayerGuess = numberOfCurrentPlayerGuess + 1;
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
        /// Evaluate the player's guess and provide the player feedback
        /// </summary>
        private static void EvaluatePlayerGuess()
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
        /// Initialize all round variables and get random number
        /// </summary>
        private static void SetupRound()
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
            // (Instructor Note: "roundNumber++:" could also be used to increment the round number.
            //
            roundNumber = roundNumber + 1;

            //
            // Get a random number
            //
            numberToGuess = GetNumberToGuess();
        }

        /// <summary>
        /// Display the player's current stats and prompt to Continue/Quit
        /// </summary>
        /// <param name="roundNumber"></param>
        /// <param name="numberOfWins"></param>
        private static void DisplayPlayerStats(int roundNumber, int numberOfWins)
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

            Console.Clear();

            Console.WriteLine("      The Number Guessing Game\n");
            Console.WriteLine("You current stats:\n");
            Console.WriteLine("Number of Rounds Played : " + roundNumber);
            Console.WriteLine("Number of Wins          : " + numberOfWins);
            Console.WriteLine("Number of Losses        : " + numberOfLosses);
            Console.WriteLine("Percentage of Wins      : " + percentageOfWins + "%");
            Console.WriteLine("\n\n");
            Console.WriteLine("Press the (Enter) key to play another round.");
            Console.WriteLine("Press the (Esc) key to Quit.");

            playerKeyResponse = Console.ReadKey();

            if (playerKeyResponse.Key == ConsoleKey.Escape)
            {
                DisplayClosingScreen();
            }
        }

        /// <summary>
        /// Display the numbers already guessed
        /// </summary>
        /// <param name="numbersPlayerHasGuessed"></param>
        /// <param name="maxNumberOfPlayerGuesses"></param>
        private static void DisplayNumbersGuessed(int[] numbersPlayerHasGuessed, int maxNumberOfPlayerGuesses)
        {
            Console.WriteLine("The numbers you have currently guessed include:");
            Console.WriteLine();
            for (int guess = 0; guess < maxNumberOfPlayerGuesses; guess++)
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
            Console.Clear();

            Console.WriteLine("          The Number Guessing Game\n");
            Console.WriteLine("Thank you for playing our Number Guessing Game.\n");
            Console.WriteLine("          Laughing Leaf Productions.\n");
            Console.WriteLine("         Press the (Enter) key to Exit.");

            Console.ReadLine();

            Environment.Exit(0);
        }

        /// <summary>
        /// Prompt for and return the player's guess
        /// </summary>
        /// <returns></returns>
        private static int GetPlayersGuess()
        {
            string playersResponse;
            int playersGuess;

            Console.Clear();

            Console.SetCursorPosition(15, 1);
            Console.WriteLine("The Number Guessing Game\n");
            Console.Write("Enter your guess as a number between 1 and 10: ");

            playersResponse = Console.ReadLine();
            playersGuess = Int32.Parse(playersResponse);

            return playersGuess;
        }

        /// <summary>
        /// Generate and return a random number 
        /// </summary>
        /// <returns></returns>
        private static int GetNumberToGuess()
        {
            int numberToGuess;
            Random randomNumber = new Random();

            numberToGuess = randomNumber.Next(1, 10);

            return numberToGuess;
        }
    }
}
