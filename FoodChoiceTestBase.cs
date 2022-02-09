using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace CRUD_Project
{
    class FoodChoiceTest
    {
        static void Main(string[] args)
        {
            UserInput();
        }

        static void ShowCheese()
        {
            Console.WriteLine("ID of entry");
            string ID = Console.ReadLine();
            int IDSelection = Int32.Parse(ID);

            //displays information already present in the database

            var fcDB = new FoodChoiceDataContext();
            var query = from c in fcDB.Tables where c.Id == IDSelection select c;
            try
            {
                foreach (var c in query)
                {
                    Console.WriteLine("Name: " + c.Name + " Age: " + c.Age + " Favorite Food: " + c.Food);
                }
            }
            catch (InvalidCastException e) {
                Console.WriteLine(e);
            }
            
            UserInput();
        }

        static void AddToDatabase()
        {
            var fcDB = new FoodChoiceDataContext();

            Console.WriteLine("First Name");
            string fName = Console.ReadLine();

            Console.WriteLine("Age");
            string age = Console.ReadLine();
            int realage = Int32.Parse(age);

            Console.WriteLine("Favorite Food");
            string fFood = Console.ReadLine();

            Table tab = new Table();
            tab.Name = fName;
            tab.Age = realage;
            tab.Food = fFood;  //obtains user info and creates a new table object with the information given and then submits it

            fcDB.Tables.InsertOnSubmit(tab);
            try
            {
                fcDB.SubmitChanges();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
            UserInput();
        }

        static void DeleteEntry()
        {
            Console.WriteLine("ID of entry to be deleted");
            string ID = Console.ReadLine();
            int IDSelection = Int32.Parse(ID);


            var fcDB = new FoodChoiceDataContext();

            var deleteline = from Table in fcDB.Tables where Table.Id == IDSelection select Table;
            foreach (var detail in deleteline)
            {
                fcDB.Tables.DeleteAllOnSubmit(deleteline);   //user chooses an ID and it is deleted from the table
            }
            fcDB.SubmitChanges();
            UserInput();
        }

        static void ModifyEntry()
        {
            var fcDB = new FoodChoiceDataContext();
            Console.WriteLine("ID of entry you wish to change");
            string ID = Console.ReadLine();
            int IDSelection = Int32.Parse(ID);

            Console.WriteLine("New name");
            string NewName = Console.ReadLine();

            Table modify = fcDB.Tables.Single(Table => Table.Id == IDSelection);   //only allows the changing of names.  
            modify.Name = NewName;
            fcDB.SubmitChanges();
            UserInput();
        }

        static void UserInput() 
        {
            Console.WriteLine("1 to display an entry, 2 to add to the database, 3 to delete, 4 to modify a name");
            string response = Console.ReadLine();

            if (response == "1")
            {
                ShowCheese();
            }
            else if (response == "2")
            {
                AddToDatabase();
            }
            else if (response == "3")
            {
                DeleteEntry();
            }
            else if (response == "4")
            {
                ModifyEntry();
            }
            else
            {
                Console.WriteLine("No you idiot. Try again");
                Console.WriteLine("1 to display an entry, 2 to add to the database, 3 to delete, 4 to modify a name");
                response = Console.ReadLine();
            }
        }
    }


}
