using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Net;

namespace StudentManagement.Controllers
{
    public class JWTController : Controller
    {
        private readonly ILogger<JWTController> _logger;
        // string _connectionString= "server=localhost;user=root;database=studentData;port=3306;password=Lion@123";
        public JWTController(ILogger<JWTController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login([FromForm] LoginData response)
        {
            string email = response.email;
            string password = response.password;
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=studentData;port=3306;password=Lion@123")){
                con.Open();
                using( MySqlCommand cmd = new MySqlCommand()){
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM LoginData WHERE email = @email AND password = @password";
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    
                    // Dictionary<string, string> totList = new Dictionary<string, string>();
                    using (MySqlDataReader dr = cmd.ExecuteReader()){
                        if(dr.Read()){
                            // totList.Add("status", "success");
                            ViewData["status"] = "success";
                            Console.WriteLine("Login Successful");
                        }
                        else{
                            // totList.Add("status", "failed");
                            // var totList={'status': 'failed'};
                            ViewData["status"] = "failed";
                            Console.WriteLine("Login Failed");
                        }
                    }
                }

            }
            Console.WriteLine(email+" "+password);
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
