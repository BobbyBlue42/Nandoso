using Nandoso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nandoso.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string submitter { get; set; }
        public string appliesTo { get; set; }
        public int reviewValue { get; set; }
        public DateTime reviewDate { get; set; }
        public string review { get; set; }

        public bool repliedTo { get; set; }
        public string reply { get; set; }
    }
}