using System.Collections.Generic;

namespace TinyCrm.Core.Model

{
    public class Customer
    {
        public int Id { get; set; }

        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string Lastname { get; set; }
        
        public string VatNumber { get; set; }
        
        public string Firstname { get; set; }

        public int Age { get; set; }
        
        public decimal TotalGross { get; set; }

        public List<Order> Orders { get; set; }


        public Customer()
        {
            Orders = new List<Order>();
            TotalGross = 0;
        }
        

        public bool EmailIsValid(string email)
        {
            return email.Contains("@") ? true : false;
        }


        public bool VatNumberIsValid(string vatnumber)
        {
            foreach (char x in vatnumber)

                if (char.IsLetter(x))
                {
                    return false;
                }
            
            if (vatnumber.Length != 9)
            {
                return false;
            }

            return true;
        }
    }
}



