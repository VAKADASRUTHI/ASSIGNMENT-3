using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using LibraryManagement.Entities;
using LibraryManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using static LibraryManagement.Entities.LibraryEntity;
using static LibraryManagement.Models.LibraryModel;

namespace LibraryManagement.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class LibraryController : Controller
    {
        public Container BookContainer;
        public Container MemberContainer;
        public Container IssueContainer;

        public LibraryController()
        {
            BookContainer = GetContainer("Book");
            MemberContainer = GetContainer("Member");
            IssueContainer = GetContainer("Issue");
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library";

        private Container GetContainer(string containerName)
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(containerName);
            return container;
        }

        [HttpPost]
        public async Task<BookModel> AddBook(BookModel bookModel)
        {
            BookEntity book = new BookEntity
            {
                Id = Guid.NewGuid().ToString(),
                UId = Guid.NewGuid().ToString(),
                Title = bookModel.Title,
                Author = bookModel.Author,
                PublishedDate = bookModel.PublishedDate,
                ISBN = bookModel.ISBN,
                IsIssued = false
            };

            BookEntity response = await BookContainer.CreateItemAsync(book);

            bookModel.UId = response.UId;
            return bookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByUId(string uId)
        {
            var book = BookContainer.GetItemLinqQueryable<BookEntity>(true).Where(b => b.UId == uId).FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = book.UId,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                IsIssued = book.IsIssued
            };

            return bookModel;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = BookContainer.GetItemLinqQueryable<BookEntity>(true).ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel
                {
                    UId = book.UId,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    ISBN = book.ISBN,
                    IsIssued = book.IsIssued
                };

                bookModels.Add(model);
            }

            return bookModels;
        }

        [HttpPost]
        public async Task<MemberModel> AddMember(MemberModel memberModel)
        {
            MemberEntity member = new MemberEntity
            {
                Id = Guid.NewGuid().ToString(),
                UId = Guid.NewGuid().ToString(),
                Name = memberModel.Name,
                DateOfBirth = memberModel.DateOfBirth,
                Email = memberModel.Email
            };

            MemberEntity response = await MemberContainer.CreateItemAsync(member);

            memberModel.UId = response.UId;
            return memberModel;
        }

        [HttpGet]
        public async Task<MemberModel> GetMemberByUId(string uId)
        {
            var member = MemberContainer.GetItemLinqQueryable<MemberEntity>(true).Where(m => m.UId == uId).FirstOrDefault();

            MemberModel memberModel = new MemberModel
            {
                UId = member.UId,
                Name = member.Name,
                DateOfBirth = member.DateOfBirth,
                Email = member.Email
            };

            return memberModel;
        }

        [HttpGet]
        public async Task<List<MemberModel>> GetAllMembers()
        {
            var members = MemberContainer.GetItemLinqQueryable<MemberEntity>(true).ToList();

            List<MemberModel> memberModels = new List<MemberModel>();

            foreach (var member in members)
            {
                MemberModel model = new MemberModel
                {
                    UId = member.UId,
                    Name = member.Name,
                    DateOfBirth = member.DateOfBirth,
                    Email = member.Email
                };

                memberModels.Add(model);
            }

            return memberModels;
        }

        [HttpPost]
        public async Task<IssueModel> IssueBook(IssueModel issueModel)
        {
            IssueEntity issue = new IssueEntity
            {
                Id = Guid.NewGuid().ToString(),
                UId = Guid.NewGuid().ToString(),
                BookId = issueModel.BookId,
                MemberId = issueModel.MemberId,
                IssueDate = DateTime.Now,
                ReturnDate = null,
                IsReturned = false
            };

            IssueEntity response = await IssueContainer.CreateItemAsync(issue);

            issueModel.UId = response.UId;
            return issueModel;
        }

        [HttpGet]
        public async Task<IssueModel> GetIssueByUId(string uId)
        {
            var issue = IssueContainer.GetItemLinqQueryable<IssueEntity>(true).Where(i => i.UId == uId).FirstOrDefault();

            IssueModel issueModel = new IssueModel
            {
                UId = issue.UId,
                BookId = issue.BookId,
                MemberId = issue.MemberId,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned
            };

            return issueModel;
        }

        [HttpPost]
        public async Task<IssueModel> UpdateIssue(IssueModel issue)
        {
            var existingIssue = IssueContainer.GetItemLinqQueryable<IssueEntity>(true).Where(i => i.UId == issue.UId).FirstOrDefault();

            existingIssue.BookId = issue.BookId;
            existingIssue.MemberId = issue.MemberId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.ReturnDate = issue.ReturnDate;
            existingIssue.IsReturned = issue.IsReturned;

            await IssueContainer.ReplaceItemAsync(existingIssue, existingIssue.Id);

            return issue;
        }
    }
}