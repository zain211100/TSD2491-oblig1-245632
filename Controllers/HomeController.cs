using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TSD2491_oblig1_245632.Models;

namespace TSD2491_oblig1_245632.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

        List<string> animalEmoji = new List<string>()
    {
        "🦍", "🦍",
        "🐴", "🐴",
        "🐷", "🐷",
        "🦊", "🦊",
        "🐱", "🐱",
        "🐯", "🐯",
        "🐸", "🐸",
        "🦬", "🦬"
    };

        List<string> foodEmoji = new List<string>()
    {
        "🍎", "🍎",
        "🍉", "🍉",
        "🍔", "🍔",
        "🍕", "🍕",
        "🍩", "🍩",
        "🍓", "🍓",
        "🍇", "🍇",
        "🥦", "🥦",
        "🥑", "🥑",
        "🍄", "🍄",
        "🥖", "🥖"
    };

        List<string> drinkEmoji = new List<string>()
    {
        "🍺", "🍺",
        "🍷", "🍷",
        "🍹", "🍹",
        "🍵", "🍵",
        "🥤", "🥤",
        "🍸", "🍸",
        "🍼", "🍼",
        "🥛", "🥛",
        "🍶", "🍶",
        "🍻", "🍻",
        "🧉", "🧉"
    };

        public static List<string> shuffledAnimals;
        public static List<string> shuffledFood;
        public static List<string> shuffledDrink;
        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
            if (Class1.suffal == 0)
            {
                Class1.suffal = 1;
                Random random = new Random();
                shuffledAnimals = animalEmoji.OrderBy(item => random.Next()).ToList();
                shuffledFood = foodEmoji.OrderBy(item => random.Next()).ToList();
                shuffledDrink = drinkEmoji.OrderBy(item => random.Next()).ToList();
            }
        }

		public IActionResult Index()
		{
          

            ViewBag.shuffledAnimals = shuffledAnimals;
            ViewBag.shuffledFood = shuffledFood;
            ViewBag.shuffledDrink = shuffledDrink;

            return View();
		}

		public IActionResult Match(string name,string desc,int id)
		{

   
            if (Class1.gameStarts == 1)
            {

                if (desc == "Animal")
                {
                    if (name == Class1.LastMatch && id!=Class1.oldID)
                    {
                        int ss = shuffledAnimals.IndexOf(name);
                        shuffledAnimals.RemoveAt(ss);
                        ss = shuffledAnimals.IndexOf(name);
                        shuffledAnimals.RemoveAt(ss);


                    }
                }


                if (desc == "Food")
                {
                    if (name == Class1.LastMatch && id != Class1.oldID)
                    {
                        int ss = shuffledFood.IndexOf(name);
                        shuffledFood.RemoveAt(ss);
                        ss = shuffledFood.IndexOf(name);
                        shuffledFood.RemoveAt(ss);


                    }
                }


                if (desc == "Drink")
                {
                    if (name == Class1.LastMatch && id != Class1.oldID)
                    {
                        int ss = shuffledDrink.IndexOf(name);
                 
                        shuffledDrink.RemoveAt(ss);
                        ss = shuffledDrink.IndexOf(name);
              
                        shuffledDrink.RemoveAt(ss);


                    }
                }



                Class1.LastMatch = name;
                Class1.oldID = id;


            }
            else
            {
                ViewBag.message = "PLEASE TYPE USER NAME!";
            }

            ViewBag.shuffledAnimals = shuffledAnimals;
            ViewBag.shuffledFood = shuffledFood;
            ViewBag.shuffledDrink = shuffledDrink;
            if (shuffledAnimals.Count == 0 && shuffledFood.Count == 0 && shuffledDrink.Count == 0)
            {
                ViewBag.userList = Class1.Playes.OrderByDescending(x => x.Times);
                ViewBag.message = "GAME FINISHED!";
            }

            return View("Index");
		}

		[HttpPost]
		public IActionResult UserReg(string username)
		{
            Class1.suffal = 1;
            Random random = new Random();
            shuffledAnimals = animalEmoji.OrderBy(item => random.Next()).ToList();
            shuffledFood = foodEmoji.OrderBy(item => random.Next()).ToList();
            shuffledDrink = drinkEmoji.OrderBy(item => random.Next()).ToList();

            ViewBag.shuffledAnimals = shuffledAnimals;
            ViewBag.shuffledFood = shuffledFood;
            ViewBag.shuffledDrink = shuffledDrink;
            Class1.gameStarts = 1;
            Class1.CurrentPlayer = username;
            int count = 0;
           if(Class1.Playes.Where(x => x.Name == username).Count()>0)
            {
               var  data = Class1.Playes.Where(x => x.Name == username).FirstOrDefault();
                data.Times = data.Times+1;
                
                
                Class1.Playes.Append(data);
            }
            else { 
             

                var usr = new User
                {
                    Name = username,
                    Times =1
                };
                Class1.Playes.Add(usr);
            }
         

            return View("Index");
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
