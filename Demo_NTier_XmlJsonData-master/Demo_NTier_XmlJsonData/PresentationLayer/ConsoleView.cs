using Demo_NTier_XmlJsonData.DataAccessLayer;
using Demo_NTier_XmlJsonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_NTier_XmlJsonData.BusinessLayer;

/// <summary>
/// Demo app for XML and Json serialization
/// </summary>
namespace Demo_NTier_XmlJsonData.PresentationLayer
{
    public class ConsoleView
    {
        public enum GenderType
        {
            Male,
            Female
        }

        private static FlintstoneCharacterBusiness _fcBusiness = new FlintstoneCharacterBusiness();
        public ConsoleView()
        {
            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        private static void DisplayShoppingLists()
        {
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();

            DisplayScreenHeader("Shopping Lists by Character");

            Console.WriteLine();
            Console.WriteLine("\tCharacters");
            Console.WriteLine("\t------------------");
            foreach (var character in characters)
            {
                Console.WriteLine("\t" + character.FullName);

                if (character.GroceryList != null && character.GroceryList.Any())
                {
                    Console.WriteLine("\t\tShopping List");
                    Console.WriteLine("\t\t-------------");
                    foreach (var item in character.GroceryList)
                    {
                        Console.WriteLine("\t\t" + item.Name.PadRight(15) + item.Quantity.ToString().PadLeft(4));
                    }
                }
            }

            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// Display Main Menu
        /// </summary>
        private static void DisplayMainMenu()
        {
            bool quitApplication = false;
            char menuChoiceKey;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get the user's menu choice
                //
                Console.WriteLine("\ta) List All Characters");
                Console.WriteLine("\tb) Character Detail");
                Console.WriteLine("\tc) Add Character");
                Console.WriteLine("\td) Delete Character");
                Console.WriteLine("\te) Update Character");
                Console.WriteLine("\tf) Character Shopping List");
                Console.WriteLine("\tq) Quit");
                Console.Write("\n\tEnter Choice: ");
                menuChoiceKey = Console.ReadKey().KeyChar;

                //
                // process user's choice
                //
                switch (menuChoiceKey)
                {
                    case 'a':
                        DisplayAllCharacters();
                        break;

                    case 'b':
                        DisplayCharacterDetail();
                        break;

                    case 'c':
                        DisplayAddCharacter();
                        break;

                    case 'd':
                        DisplayDeleteCharacter();
                        break;

                    case 'e':
                        DisplayUpdateCharacter();
                        break;

                    case 'f':
                        DisplayShoppingLists();
                        break;

                    case 'q':
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\t*************************************************");
                        Console.WriteLine("\t   Please indicate your choice with a letter.");
                        Console.WriteLine("\t**************************************************");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        /// <summary>
        /// add a new character
        /// </summary>
        static void DisplayAddCharacter()
        {
            FlintstoneCharacter character = new FlintstoneCharacter();
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();
            bool responding;
            bool adding = true;

            DisplayScreenHeader("Add New Character");

            do
            {
                Console.Write("ID: ");                

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    foreach (var flintstone in characters)
                    {
                        if (result == flintstone.Id)
                        {
                            Console.WriteLine();
                            Console.WriteLine("ID already in database. Please try again.");
                            adding = true;
                            break;
                        }
                        else
                        {
                            character.Id = result;
                            adding = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Please try again.");
                    adding = true;
                }

            } while (adding);

            Console.Write("First Name: ");
            character.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            character.LastName = Console.ReadLine();

            //get age from user
            do
            {
                Console.Write("Age: ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    character.Age = result;
                    responding = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Please try again.");
                    responding = true;
                }
            } while (responding);

            //get gender from user
            do
            {
                Console.WriteLine("Gender (F for Female, M for Male): ");
                string response = Console.ReadLine();

                if (response.ToLower() == "f")
                {
                    character.Gender = FlintstoneCharacter.GenderType.Female;
                    responding = false;
                }
                else if (response.ToLower() == "m")
                {
                    character.Gender = FlintstoneCharacter.GenderType.Male;
                    responding = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a valid value. F for female, M for male...)");
                    responding = true;
                }

            } while (responding);

            _fcBusiness.AddFlinstoneCharacter(character);

            Console.WriteLine("Character successfully added!");
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// update existing character
        /// </summary>
        static void DisplayUpdateCharacter()
        {
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();
            bool updating = true;            

            DisplayScreenHeader("Update Existing Character");

            do
            {
                Console.WriteLine("Please enter the ID for the character you want to update");

                if (int.TryParse(Console.ReadLine(), out int result))
                {                    
                    foreach (var character in characters)
                    {
                        if (result == character.Id)
                        {                            
                            updating = false;
                            _fcBusiness.DelFlinstoneCharacter(character);
                            bool responding;

                            Console.Write("First Name: ");
                            character.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            character.LastName = Console.ReadLine();

                            //get age from user
                            do
                            {
                                Console.Write("Age: ");

                                if (int.TryParse(Console.ReadLine(), out int ageResult))
                                {
                                    character.Age = ageResult;
                                    responding = false;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid entry. Please try again.");
                                    responding = true;
                                }
                            } while (responding);

                            //get gender from user
                            do
                            {
                                Console.WriteLine("Gender (F for Female, M for Male): ");
                                string response = Console.ReadLine();

                                if (response.ToLower() == "f")
                                {
                                    character.Gender = FlintstoneCharacter.GenderType.Female;
                                    responding = false;
                                }
                                else if (response.ToLower() == "m")
                                {
                                    character.Gender = FlintstoneCharacter.GenderType.Male;
                                    responding = false;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Enter a valid value. F for female, M for male...)");
                                    responding = true;
                                }

                            } while (responding);                            
                            _fcBusiness.AddFlinstoneCharacter(character);
                            break;
                        }
                        else
                        {
                            updating = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Please try again.");
                    updating = true;
                }

            } while (updating);

            Console.WriteLine("Character successfully updated!");
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// delete existing character
        /// </summary>
        static void DisplayDeleteCharacter()
        {
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();
            bool deleting = true;

            DisplayScreenHeader("Delete Character");

            do
            {
                Console.WriteLine("Please enter the ID for the character you would like to delete.");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    foreach (var character in characters)
                    {
                        if (character.Id == result)
                        {
                            _fcBusiness.DelFlinstoneCharacter(character);
                            deleting = false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Please try again.");
                    deleting = true;
                }
            } while (deleting);

            Console.WriteLine("Character successfully deleted!");
            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// display a single character's information
        /// </summary>
        static void DisplayCharacterDetail()
        {
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();

            int id = DisplayGetCharacterIdFromList(characters);

            FlintstoneCharacter character = _fcBusiness.FlintstoneCharacterById(id);

            if (_fcBusiness.FileIoStatus == FileIoMessage.Complete)
            {
                DisplayScreenHeader("Character Information");
                DisplayCharacterInfo(character);
            }
            else
            {
                // process file IO error message
            }

            DisplayMainMenuPrompt();
        }

        /// <summary>
        /// list all character's full names and ids and query the user for an id
        /// </summary>
        /// <param name="characters">character id</param>
        /// <returns></returns>
        static int DisplayGetCharacterIdFromList(List<FlintstoneCharacter> characters)
        {
            bool validResponse = false;
            int id = -1;
            List<int> validIds = characters.Select(c => c.Id).OrderBy(characterId => characterId).ToList();

            do
            {
                DisplayScreenHeader("Choose Character");

                Console.WriteLine(
                    "Name".PadRight(20) +
                    "Id".PadRight(4)
                    );
                Console.WriteLine(
                    "-------------".PadRight(20) +
                    "-----".PadRight(4)
                    );

                foreach (var character in characters)
                {
                    Console.WriteLine(
                        character.FullName.PadRight(20) +
                        character.Id.ToString().PadRight(4)
                        );
                }
                Console.WriteLine();
                Console.Write("Enter Id:");
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Please enter an integer value for the ID.");
                    DisplayContinuePrompt();
                }
                else if (!validIds.Contains(id))
                {
                    Console.WriteLine("Please enter a valid Id shown above.");
                    DisplayContinuePrompt();
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);

            return id;
        }

        /// <summary>
        /// display all character property values
        /// </summary>
        /// <param name="character">character</param>
        static void DisplayCharacterInfo(FlintstoneCharacter character)
        {
            Console.WriteLine($"Last Name: {character.LastName}");
            Console.WriteLine($"First Name: {character.FirstName}");
            Console.WriteLine($"Age: {character.Age}");
            Console.WriteLine($"Gender: {character.Gender}");
            Console.WriteLine($"Average Annual Gross: {character.AverageAnnualGross:c}");
            Console.WriteLine($"Description: \n{character.Description}");
        }


        /// <summary>
        /// display a table of all characters: first name, last name, and id
        /// </summary>
        static void DisplayAllCharacters()
        {
            List<FlintstoneCharacter> characters = _fcBusiness.AllFlintstoneCharacters();

            if (_fcBusiness.FileIoStatus == FileIoMessage.Complete)
            {
                DisplayScreenHeader("Flintstone Characters");

                Console.WriteLine(
                    "First Name".PadRight(15) +
                    "Last Name".PadRight(15) +
                    "Id".PadRight(4) +
                    "Age".PadRight(4) +
                    "Gender".PadRight(8) +
                    "Average Gross".PadRight(10)
                    );
                Console.WriteLine(
                    "-------------".PadRight(15) +
                    "-------------".PadRight(15) +
                    "-----".PadRight(4) +
                    "-----".PadRight(4) +
                    "----".PadRight(8) +
                    "------------".PadRight(10)
                    );

                foreach (var character in characters)
                {
                    Console.WriteLine(
                        character.FirstName.PadRight(15) +
                        character.LastName.PadRight(15) +
                        character.Id.ToString().PadRight(4) +
                        character.Age.ToString().PadRight(4) +
                        character.Gender.ToString().PadRight(8) +
                        character.AverageAnnualGross.ToString().PadRight(10)
                        );
                }
            }
            else
            {
                // process file IO error message
            }

            DisplayMainMenuPrompt();
        }


        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        static void DisplayMainMenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            DisplayMainMenu();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tDemonstration:");
            Console.WriteLine("\t\tN-Tier CRUD with XML or JSON Persistence");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using our app.");
            Console.WriteLine();

            Console.WriteLine("\tPress any key to exit.");
            Console.ReadKey();
        }

        #endregion
    }
}
