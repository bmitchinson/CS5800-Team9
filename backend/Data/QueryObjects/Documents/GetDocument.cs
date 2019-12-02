using System.Linq;
using backend.Data.Models;

namespace backend.Data.QueryObjects
{
    public static class GetDocument
    {
            public static IQueryable<Document> GetDocumentsWithSubmissions(
                this IQueryable<Document> documents, 
                int docId) =>
                    documents
                    .Where(_ => _.DocumentId == docId)
                    .Select(_ => new Document
                    {
                        DocumentId = _.DocumentId,
                        RegistrationId = _.RegistrationId,
                        ResourceLink = _.ResourceLink,
                        DocType = _.DocType,
                        Registration = new Registration
                        {
                            RegistrationId = _.Registration.RegistrationId,
                            CourseId = _.Registration.CourseId,
                            InstructorId = _.Registration.InstructorId,
                            EnrollmentLimit = _.Registration.EnrollmentLimit,
                            Section = _.Registration.Section,
                            StartTime = _.Registration.StartTime,
                            EndTime = _.Registration.EndTime,
                        },
                        Submissions = _.Submissions
                            .Select(sub => new Submission
                            {
                                SubmissionId = sub.SubmissionId,
                                DocumentId = sub.DocumentId,
                                StudentEnrollmentId = sub.StudentEnrollmentId,
                                Grade = sub.Grade,
                                ResourceLink = sub.ResourceLink,
                                SubmissionTime = sub.SubmissionTime,
                                StudentEnrollment = new StudentEnrollment
                                {
                                    StudentEnrollmentId = sub.StudentEnrollment.StudentEnrollmentId,
                                    StudentId = sub.StudentEnrollment.StudentId,
                                    RegistrationId = sub.StudentEnrollment.RegistrationId,
                                    IsCompleted = sub.StudentEnrollment.IsCompleted,
                                    Grade = sub.StudentEnrollment.Grade,
                                    Student = new Student
                                    {
                                        StudentId = sub.StudentEnrollment.Student.StudentId,
                                        FirstName = sub.StudentEnrollment.Student.FirstName,
                                        LastName = sub.StudentEnrollment.Student.LastName,
                                        BirthDate = sub.StudentEnrollment.Student.BirthDate,
                                        Email = sub.StudentEnrollment.Student.Email
                                    }
                                }
                            }).ToList()
                    });
    }
}