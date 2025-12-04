////using System;
////using System.ComponentModel.DataAnnotations;
////using System.Globalization;
////using System.Linq;

////namespace DocumentService.WebAPI.Models
////{
////    public class Document
////    {
////        public string Title { get; set; }
////        public int Size { get; set; }
////        public string Format { get; set; }
////    }
////}
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace DocumentService.WebAPI.Models
//{
//    public class Document
//    {
//        [Required(ErrorMessage = "Title is required")]
//        [StringLength(35, MinimumLength = 5, ErrorMessage = "Title must contain a minimum of 5 characters and a maximum of 35")]
//        public string Title { get; set; }

//        [Required(ErrorMessage = "Size is required")]
//        [Range(1, 499, ErrorMessage = "Size must be greater than 0 MB and less than 500 MB")]
//        public int Size { get; set; }

//        [Required(ErrorMessage = "Format is required")]
//        public string Format { get; set; }

//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            var errors = new List<ValidationResult>();

//            // Validate Title capitalization
//            if (!string.IsNullOrWhiteSpace(Title))
//            {
//                var words = Title.Split(' ');
//                if (words.Any(w => !char.IsUpper(w[0])))
//                {
//                    errors.Add(new ValidationResult("Title is invalid: Title must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter", new[] { nameof(Title) }));
//                }
//            }

//            // Validate Format
//            var allowedFormats = new[] { "txt", "pdf", "docx" };
//            if (string.IsNullOrWhiteSpace(Format) || !allowedFormats.Contains(Format))
//            {
//                errors.Add(new ValidationResult("Format is invalid: Format must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'", new[] { nameof(Format) }));
//            }

//            return errors;
//        }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace DocumentService.WebAPI.Models
{
    public class Document : IValidatableObject
    {
        public string Title { get; set; }
        public int Size { get; set; }
        public string Format { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate Title
            if (string.IsNullOrWhiteSpace(Title) ||
                Title.Length < 5 ||
                Title.Length > 35 ||
                !char.IsUpper(Title[0]))
            {
                yield return new ValidationResult(
                    "Title is invalid: Title must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter",
                    new[] { nameof(Title) });
            }

            // Validate Author
            if (Size >= 500 ||
                Size <= 0)
            {
                yield return new ValidationResult(
                    "Size is invalid: Size must be greater than 0 MB and less than 500 MB",
                    new[] { nameof(Size) });
            }

            // Validate FileFormat
            var allowedFormats = new HashSet<string> { "pdf", "docx", "txt" };

            if (string.IsNullOrWhiteSpace(Format) ||
                !allowedFormats.Contains(Format))
            {
                yield return new ValidationResult(
                    "Format is invalid: Format must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'",
                    new[] { nameof(Format) });
            }
        }
    }
}