using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.Tours;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Problem : ValueObject
    {
        public string Category { get; set; }
        public bool Priority { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public long TourId { get; set; } 
        public int TouristId { get; set; } 
        public string AuthorsSolution { get;  set; } 
        public bool IsSolved { get; set; } 
        public string UnsolvedProblemComment { get; set; } 
        public DateTime Deadline { get; set; } 

        [Newtonsoft.Json.JsonConstructor]
        public Problem(string category, bool priority, string description, DateTime time, long tourId, int touristId, string authorsSolution, bool isSolved, string unsolvedProblemComment, DateTime deadline)
        { 
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid category.");
            Category = category;
            Priority = priority;
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            Description = description;
            Time = time;
            TourId = tourId;
            TouristId = touristId;
            AuthorsSolution = authorsSolution;
            IsSolved = isSolved;
            UnsolvedProblemComment = unsolvedProblemComment;
            Deadline = deadline;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Category;
            yield return Priority;
            yield return Description;
            yield return Time;
            yield return TourId;
            yield return TouristId;
            yield return AuthorsSolution;
            yield return IsSolved;
            yield return UnsolvedProblemComment;
            yield return Deadline;
        }
        public void UpdateProblem(Problem newProblem)
        {
            IsSolved = newProblem.IsSolved;
            if (!IsSolved)
            {
                UnsolvedProblemComment = newProblem.UnsolvedProblemComment;
            }
        }

        public void RespondToProblem(string response)
        {
            AuthorsSolution = response;
        }

        public void LeaveUnsolvedComment(string comment)
        {
            IsSolved = false;
            UnsolvedProblemComment = comment;
        }

        public void SolveProblem()
        {
            IsSolved = true;
        }
        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }
        public bool IsDeadlineExpired()
        {
            return !IsSolved && Deadline < DateTime.Now;
        }
    }
}