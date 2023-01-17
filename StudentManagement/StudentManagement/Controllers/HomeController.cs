using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Net;

//For itextsharp
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

             public void CreatePDF(string filePath)
    {
        // Create a new PDF document
        var document = new Document(PageSize.A4, 10, 10, 10, 10);

        // Create a new PDF writer
        var pdfWriter = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

        // Open the PDF document
        document.Open();

        // Add the receipt content
        var font = FontFactory.GetFont("Arial", 12);
        var companyName = new Paragraph("Acme Inc.", font);
        var companyAddress = new Paragraph("123 Main St, Suite 200, Anytown USA 12345", font);
        var itemsPurchased = new Paragraph("Item 1: $10.00\nItem 2: $5.00", font);
        var totalCost = new Paragraph("Total: $15.00", font);
        document.Add(companyName);
        document.Add(companyAddress);
        document.Add(itemsPurchased);
        document.Add(totalCost);

        // Close the PDF document
        document.Close();
    }

        public IActionResult Index()
        {
            return View();
        }

        // Get Request Using C#

        // public string FetchReq(){
        //         using System.Net;
        //         string url = "https://aquamediumorchidvoxels.suryashjha.repl.co/tasks";
        //         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //         request.Method = "GET";
        //         HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //         StreamReader reader = new StreamReader(response.GetResponseStream());
        //         string responseString = reader.ReadToEnd();

        //         Console.WriteLine(responseString);
        //         return responseString;
        // }

        
        public IActionResult studentList()
        {
            // Database Connection
            Console.WriteLine("1");
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=studentData;port=3306;password=Lion@123");
            con.Open();
            Console.WriteLine("2");

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Student;", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("3");
            Dictionary<int, Dictionary<string, string>> totList = new Dictionary<int, Dictionary<string, string>>();
            int c = 1;
            while (dr.Read())
            {
                totList[c] = new Dictionary<string, string>();
                totList[c].Add("rollNo", dr.GetString(0));
                totList[c].Add("firstName",dr.GetString(1).Trim());
                totList[c].Add("lastName", dr.GetString(2).Trim());
                totList[c].Add("gender", dr.GetString(3).Trim());
                totList[c].Add("class", dr.GetString(4).Trim());
                c++;
                // Console.WriteLine(dr.GetString(2));
            }
            dr.Close();
            cmd.Dispose();
            con.Close();
            ViewData["totList"] = totList;
            // FetchReq();
           // Console.WriteLine(totList[1]["gender"]);
            return View();
        }
        
        public IActionResult Login([FromForm] LoginData response)
        {

            string val= response.email;
            string pass= response.password;

            // string pass= Request.Form["trtpassword"];
            // string val=email.Value;
            // string trval=tremail.Value;

            Console.WriteLine(val+" "+pass);
            // Console.WriteLine(pass);

            return View();
        }
        public IActionResult check(string button_val)
        {
            Console.WriteLine("clicked");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            string filePath = "C:\\Users\\HP\\Desktop\\ASPProjects\\StudentManagement\\StudentManagement\\wwwroot\\pdf\\test.pdf";
            CreatePDF(filePath);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}