using GradeBook.GradeBooks;
using static System.Console;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;
        public static void CommandLoop()
        {
            while (!Quit)
            {
                WriteLine(string.Empty);
                WriteLine(">> What would you like to do?");
                var command = ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void CreateCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                WriteLine("Command not valid, Create requires a name, type of gradebook, if it's weighted (true / false).");
                return;
            }
            var name = parts[1];
            var type = parts[2];
            bool isWeighted = bool.Parse(parts[3]);

            if(type != "standard" && type != "ranked"){
                WriteLine("{0} is not a supported type of gradebook, please try again", type);
                return;
            }

            if(type == "standard"){
                StandardGradeBook standardGradeBook = new StandardGradeBook(name, isWeighted);
                WriteLine("Created standard gradebook {0}.", name);
                GradeBookUserInterface.CommandLoop(standardGradeBook);
            }

            if(type == "ranked"){
                RankedGradeBook rankedGradeBook = new RankedGradeBook(name, isWeighted);
                WriteLine("Created ranked gradebook {0}.", name);
                GradeBookUserInterface.CommandLoop(rankedGradeBook);
            }
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            WriteLine();
            WriteLine("GradeBook accepts the following commands:");
            WriteLine();
            Write("Create 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, ");
            Write("'Type' is what type of grading it should use, ");
            WriteLine("and 'Weighted' is whether or not grades should be weighted (true or false).");
            WriteLine();
            WriteLine("Load 'Name' - Loads the gradebook with the provided 'Name'.");
            WriteLine();
            WriteLine("Help - Displays all accepted commands.");
            WriteLine();
            WriteLine("Quit - Exits the application");
        }
    }
}
