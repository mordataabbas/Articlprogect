using ArticlProgect.Core;
using ArticlProgect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticlProgect.Controllers
{
    [Authorize("Admin")]
    public class categoryController : Controller
    {
        private readonly IDataHelper<category> dataHelper;
        private int pageItem;

        public categoryController(IDataHelper<category>dataHelper)
        {
            this.dataHelper = dataHelper;
            pageItem = 10;
        }
        // GET: categoryController
        public ActionResult Index( int?id)
        {
            if(id == 0 || id==null)
            {
                return View(dataHelper.GetAllData().Take(pageItem));
            }
            else
            {
                var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                return View(data);
            }
           
        }

        public ActionResult search( string searchItem)
        {
            if (searchItem == null)
            {
                return View("Index", dataHelper.GetAllData());
            } 
            else
            {
                return View("Index", dataHelper.Search(searchItem));
            }
          
        }


        // GET: categoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(category collection)
        {
            try
            {
               var result= dataHelper.Add(collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: categoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: categoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, category collection)
        {
            try
            {
                var result = dataHelper.Edit(id,collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: categoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: categoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, category collection)
        {
            try
            {
                var result = dataHelper.Delete(id);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
