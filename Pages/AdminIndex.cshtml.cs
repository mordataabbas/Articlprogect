using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlProgect.Core;
using ArticlProgect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
namespace ArticlProgect.Pages
{
    [Authorize]
    public class AdminIndexModel : PageModel
    {
        private readonly IDataHelper<AuthorPost> dataHelper;

        public AdminIndexModel( IDataHelper<AuthorPost> dataHelper)
            {
            this.dataHelper = dataHelper;
        }
        public void OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
