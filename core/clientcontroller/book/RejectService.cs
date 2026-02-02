using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.core;


namespace LibraryManagementSystem.core.clientcontroller.book
{
    internal class RejectService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("reject.json");

        public static RejectResponse GetRejectedBorrows()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new RejectResponse
                    {
                        Status = "SUCCESS",
                        Message = "No rejected borrow records available",
                        RejectedBorrows = new List<Reject>()
                    };
                }

                var content = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(content))
                {
                    return new RejectResponse
                    {
                        Status = "SUCCESS",
                        Message = "No rejected borrow records available",
                        RejectedBorrows = new List<Reject>()
                    };
                }

                var rejected = JsonConvert.DeserializeObject<List<Reject>>(content) ?? new List<Reject>();

                if (rejected == null || rejected.Count == 0)
                {
                    return new RejectResponse
                    {
                        Status = "SUCCESS",
                        Message = "No rejected borrow records available",
                        RejectedBorrows = new List<Reject>()
                    };
                }

                return new RejectResponse
                {
                    Status = "SUCCESS",
                    Message = "Rejected borrow records loaded",
                    RejectedBorrows = rejected
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reject service error: {ex.Message}");
                return new RejectResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }
    }
}
