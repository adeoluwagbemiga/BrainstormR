using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.MyAccount
{
    public class MySubscriptionItemModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Picture { get; set; }
        public string url { get; set; }
        public int TransactionId { get; set; }
        public int Type { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}
