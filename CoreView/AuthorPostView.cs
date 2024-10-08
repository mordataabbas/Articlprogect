﻿using ArticlProgect.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticlProgect.CoreView
{
    public class AuthorPostView
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        
        [Display(Name = "معرف المستخدم")]
        public string UserId { get; set; }
       
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
     
        [Display(Name = "اسم الكامل")]
        public string FullNamee { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصنف")]
        [DataType(DataType.Text)]
        public string PostCategory { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "العنوان")]
        [DataType(DataType.Text)]
        public string PostTitle { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الوصف")]
        [DataType(DataType.MultilineText)]
        public string PostDescription { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الصوره")]
        [DataType(DataType.Upload)]
        public IFormFile PostImageUrl { get; set; }
      
        [Display(Name = "اتاريخ لاضافه")]
        public DateTime AddedDate { get; set; }

        //Navigation Areea

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public category category { get; set; }
    }
}
