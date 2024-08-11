using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlProgect.Core;
using ArticlProgect.Data;

namespace ArticlProgect.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Core.category> dataHelperForCatecore;
        private readonly IDataHelper<AuthorPost> dataHelperForPost;
       public readonly int NoOFItem;

        public IndexModel(
            ILogger<IndexModel> logger,
            IDataHelper<Core.category> dataHelperForCatecore,
            IDataHelper<Core.AuthorPost> dataHelperForPost
            )
        {
            _logger = logger;
            this.dataHelperForCatecore = dataHelperForCatecore;
            this.dataHelperForPost = dataHelperForPost;
            NoOFItem = 6;

            ListOfCategore = new List<Core.category>();
            ListOfPost = new List<AuthorPost>();
        }

        public List<Core.category> ListOfCategore { get; set; }
        public List<Core.AuthorPost> ListOfPost { get; set; }
        public void OnGet(string LoadState,string CategoreName, string search, int id)
        {
            GetAllCategore();

            if (LoadState == null || LoadState == "All")
            {
                GetALLPost();
            }
            else if (LoadState == "ByCategore")
            {
                GetDataByCategoreName(CategoreName);
            }
            else if (LoadState == "Search")
            {
                SearchData(search);
            }
            else if (LoadState == "Next")
            {
                GetNextData(id);
            }
            else if (LoadState == "Prev")
            {
                GetNextData(id - NoOFItem);
            }


        }

        private void GetAllCategore()
        {
            ListOfCategore = dataHelperForCatecore.GetAllData();
        }

        private void GetALLPost()
        {
            ListOfPost = dataHelperForPost.GetAllData().Take(NoOFItem).ToList();
        }
        private void GetDataByCategoreName(string CategoreName)
        {
            ListOfPost = dataHelperForPost.GetAllData().Where(x => x.PostCategory == CategoreName).Take(NoOFItem).ToList();
        }
        private void SearchData(string SearchItem)
        {
            ListOfPost = dataHelperForPost.Search(SearchItem);
        }
        private void GetNextData(int id)
        {
            ListOfPost = dataHelperForPost.GetAllData(). Where(x => x.Id > id).Take(NoOFItem).ToList();
        }
    }
}
