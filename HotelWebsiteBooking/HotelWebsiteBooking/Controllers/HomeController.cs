using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Diagnostics;

namespace HotelWebsiteBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Comments.ToList());
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Pay(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions { 
                Email = stripeEmail, 
                Source = stripeToken 
            });
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail= stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"OrderId", "111" },
                    {"Postcode", "LEE111" },

                }
            });
            if (charge.Status == "succeeded") 
            { 
                string BalanceTransactoinId = charge.BalanceTransactionId;
                return View();
            }
            else
            {

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}