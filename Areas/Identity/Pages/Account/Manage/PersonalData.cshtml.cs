// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proje.Data;
using proje.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proje.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<Member> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly ApplicationDbContext dbContext;

        public PersonalDataModel(
            UserManager<Member> userManager,
            ILogger<PersonalDataModel> logger, ApplicationDbContext context)
        {

            dbContext = context;
            _userManager = userManager;
            _logger = logger;
        }
        public List<Appointment> MemberAppointments { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            MemberAppointments = dbContext.Appointment
                .Where(x => x.MemberId == memberId && x.Date >= DateTime.Now)
                .OrderBy(x => x.Date)
                .ToList();

            return Page();
        }
    }
}