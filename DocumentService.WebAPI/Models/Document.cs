using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentApi.Models
{
    public class Document
    {
        public string Title { get; set; }
        public int? Size { get; set; }
        public string Format { get; set; }

        // All validation logic is inside the model (not in controller)
        public List<string> Validate()
        {
            var errors = new List<string>();

            // 1. Title validation
            if (string.IsNullOrWhiteSpace(Title) ||
                Title.Length < 5 ||
                Title.Length > 35 ||
                !IsTitleWordsStartWithUppercase(Title))
            {
                errors.Add("Title is invalid: Title must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter");
            }

            // 2. Size validation
            if (!Size.HasValue || Size.Value <= 0 || Size.Value >= 500)
            {
                errors.Add("Size is invalid: Size must be greater than 0 MB and less than 500 MB");
            }

            // 3. Format validation
            var allowedFormats = new[] { "txt", "pdf", "docx" };

            if (string.IsNullOrWhiteSpace(Format) ||
                Format != Format.ToLower() ||
                !allowedFormats.Contains(Format))
            {
                errors.Add("Format is invalid: Format must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'");
            }

            return errors;
        }

        private bool IsTitleWordsStartWithUppercase(string title)
        {
            var words = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (word.Length == 0)
                    return false;

                // First character must be uppercase
                if (!char.IsUpper(word[0]))
                    return false;
            }

            return true;
        }
    }
}