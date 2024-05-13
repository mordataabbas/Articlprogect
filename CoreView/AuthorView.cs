using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArticlProgect.CoreView
{
    public class AuthorView
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "معرف المستخدم")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "اسم الكامل")]
        public string FullNamee { get; set; }
        [Display(Name = "الصوره")]
        public IFormFile ProfileImageUrl { get; set; }
        [Display(Name = "السيره الذاتيه")]
        public string Bio { get; set; }
        [Display(Name = "فيسبوك")]
        public string Facbook { get; set; }
        [Display(Name = "انستغرام")]
        public string Instagram { get; set; }
        [Display(Name = "تويتر")]
        public string Twitter { get; set; }
    }
}
