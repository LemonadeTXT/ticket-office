﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;

namespace TicketOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketService _ticketService;

        public HomeController(ITicketService ticketService, ILogger<HomeController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Tickets");
            }

            return View();
        }

        [Authorize]
        public IActionResult Tickets()
        {
            var ticketsDto = _ticketService.GetAllTicketsDto();

            return View(ticketsDto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Tickets(TicketsFindDto ticketsFindDto)
        {
            var ticketsDto = _ticketService.FindTickets(ticketsFindDto);

            return View(ticketsDto);
        }

        [Authorize]
        public IActionResult FindTickets()
        {
            return View();
        }

        [Authorize]
        public IActionResult GetTicket(int ticketId)
        {
            var ticket = _ticketService.GetTicket(ticketId);

            var userId = int.Parse(User.Identity.Name);

            if (ticket.NumberOfSeats > 0 && !_ticketService.IsPurchasedTicket(userId, ticketId))
            {
                _ticketService.PurchaseTicket(userId, ticketId);

                _logger.LogInformation($"User with Id = {userId} purchase Ticket with Id = {ticketId}.");

                return RedirectToAction("YourTickets", "Profile");
            }
            else
            {
                _logger.LogInformation($"User with Id = {userId} already purchase this Ticket with Id = {ticketId}, and cannot do it again!");
            }

            return RedirectToAction("Tickets");
        }
    }
}