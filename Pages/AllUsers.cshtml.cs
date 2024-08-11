using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlProgect.Core;
using ArticlProgect.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArticlProgect.Pages
{
    public class AllUsersModel : PageModel
    {
     
      
      
        public readonly int NoOFItem;
        private readonly IDataHelper<Core.Author> dataHelper;

        public AllUsersModel(
          
        
            IDataHelper<Core.Author> dataHelper
            )
        {
           
            this. dataHelper = dataHelper;
            NoOFItem = 6;



            ListOfAuthor = new List<Core.Author>();
            this.dataHelper = dataHelper;
        }

      
        public List<Core.Author> ListOfAuthor {get; set; }
        public void OnGet(string LoadState, string search, int id)
        {
          

            if (LoadState == null || LoadState == "All")
            {
                GetALLAuthors();
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

       

        private void GetALLAuthors()
        {
          ListOfAuthor  =  dataHelper.GetAllData().Take(NoOFItem).ToList();
        }
      
        private void SearchData(string SearchItem)
        {
            ListOfAuthor =  dataHelper.Search(SearchItem);
        }
        private void GetNextData(int id)
        {
            ListOfAuthor =  dataHelper.GetAllData().Where(x => x.Id > id).Take(NoOFItem).ToList();
        }
    }
}
