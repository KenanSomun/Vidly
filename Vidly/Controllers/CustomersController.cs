using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        /* DATA BIND-ing
         * MVC zna popuniti unesene podatke u model Customer 
         * (jer smo tamo koristili c.Customer.Name i sl.)
         * Također, bez SaveChanges, nećemo uspješno pohraniti podatke iz 
         * određene operacije Add, Edit i sl. 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                /* Microsoft MVC options not good enough!
                 * TryUpdateModel(customerInDb); - update se sve, sigurnosni problemi!
                 * TryUpdateModel(customerInDb, "", new string[] { "Name", "Email" }); - update su samo 
                 * stringovi iz niza stringova, međutim, ovo su sve MAGIC STRINGS, pa bi mijenjanje 
                 * imena ovih property-a dovelo do krahiranja programa zbog ove linije .. 
                 * // Mapper.Map(customer, customerInDb); - korištenje(m) AutoMapper-a, što bi mogli
                 * osigurati npr. korištenjem novog modela UpdateCustomerDto u kojem bi imali samo ona
                 * polja koja ćemo dopuštati da se mijenjaju .. * DTO - Data Transfer Object
                 */

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }


        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Details/1
        public ActionResult Details(int id)
        {
            /* LINQ C#: 
             * SingleOrDefault - vraća jedan element sekvence, ili default vr. ukoliko je prazna
             * c => - c postaje objekat iz GetCustomers() sa =>
             * Onda se Id tog objekta (c.Id) upoređuje sa id kojeg imamo u URL-u .. 
             */
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }

        /* Lokalni podaci (umjesto baze podataka) za testiranje .. 
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Id = 1, Name = "John Doe"},
                new Customer {Id = 2, Name = "Will Smith"}
            };
        }
        */
    }
}