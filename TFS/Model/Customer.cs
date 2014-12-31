using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS.Model
{
    class Customer
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ReferralCode { get; set; }
        public string ReferralUrl { get; set; }
        public bool Phonenumber_Verified { get; set; }
        public bool Wallet_Enabled { get; set; }
    }
}
