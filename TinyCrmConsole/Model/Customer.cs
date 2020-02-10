namespace TinyCrm.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Firstname { get; set; }
        ///

        ///
        public int Age { get; set; }




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
     


        