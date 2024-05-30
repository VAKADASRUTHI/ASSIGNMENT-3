using Newtonsoft.Json;
using System;

namespace LibraryManagement.Entities
{
    public class LibraryEntity
    {
        public class BookEntity
        {
            [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
            public string UId { get; set; }

            [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }

            [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
            public string Author { get; set; }

            [JsonProperty(PropertyName = "publishedDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime PublishedDate { get; set; }

            [JsonProperty(PropertyName = "isbn", NullValueHandling = NullValueHandling.Ignore)]
            public string ISBN { get; set; }

            [JsonProperty(PropertyName = "isIssued", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsIssued { get; set; }
        }

        public class MemberEntity
        {
            [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
            public string UId { get; set; }

            [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime DateOfBirth { get; set; }

            [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
            public string Email { get; set; }
        }

        public class IssueEntity
        {
            [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
            public string Id { get; set; }

            [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
            public string UId { get; set; }

            [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
            public string BookId { get; set; }

            [JsonProperty(PropertyName = "memberId", NullValueHandling = NullValueHandling.Ignore)]
            public string MemberId { get; set; }

            [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime IssueDate { get; set; }

            [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime? ReturnDate { get; set; }

            [JsonProperty(PropertyName = "isReturned", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsReturned { get; set; }
        }
    }
}