using System.Net;

namespace Assignment_Session_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank_Account account1 = new Bank_Account();
            Bank_Account account2 = new Bank_Account("Aya Al-Refaey","12345678912345","01022244567",1000);
            Bank_Account account3 = new Bank_Account("Doha Harby", "98765432187654", "01086532156","Sadat City");
            account1.ShowAccountDetails();
            account2.ShowAccountDetails();
            account3.ShowAccountDetails();
            Console.ReadKey();
        }

      
    }
      public class Bank_Account
        {
            //Fields --------------------

            public const string BankCode = "BNK001";
            public readonly DateTime CreatedDate;
            private int _accountNumber;
            private string _fullName;
            private string _nationalID;
            private string _phoneNumber;
            private string _address;
            private decimal _balance;
            private static int _nextAccount = 1;


            //Properties --------------------

            public string FullName {
                get{
                    return _fullName; 
                }
                set {
                    if (string.IsNullOrWhiteSpace(value)) {
                        Console.WriteLine("Full Name can't be empty.");
                    }
                    else {
                        _fullName = value;
                    }
                        
                } }

            public string NationalID
            {
                get
                {
                    return _nationalID;
                }
                set
                {
                    value = value.Trim();
                    if (value.Length==14 && ulong.TryParse(value, out _)&& !string.IsNullOrWhiteSpace(value))
                    {
                        _nationalID = value;
                      
                    }
                    else
                    {
                        Console.WriteLine("National_Id Must be Exactly 14 digits");
                    }

                }
            }

            public string PhoneNumber
            {
                get
                {
                    return _phoneNumber;
                }
                set
                {
                    value = value.Trim();
                    if (!string.IsNullOrWhiteSpace(value)&&value.Length==11&& value.StartsWith("01") && ulong.TryParse(value, out _)) //  value[0]=='0'&& value[1] =='1'
                    {
                        _phoneNumber = value;
                    }
                    else
                    {
                        Console.WriteLine("Phone number Must start With '01' and must be 11 digit");
                       
                    }

                }
            }

            public decimal Balance
            {
                get
                {
                    return _balance;
                }
                set
                {
                    if (value>=0)
                    {
                        _balance = value;
                       
                    }
                    else
                    {
                        Console.WriteLine("Balance Must be greater than or equal to '0'");
                    }

                }
            }

            public string Address { 
                get { 
                    return _address;
                }
                set {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine("Address Can not be empty");
                    }
                    else
                    {
                        _address = value;
                    }
                       
                } 
            }

            //Constructors ------------------------------- 

            public Bank_Account()
            {
                _accountNumber = _nextAccount++;
                CreatedDate = DateTime.Now;
                FullName = "Bank User";
                NationalID = "00000000000000";
                PhoneNumber = "01000000000";
                Address = "N/A";
                Balance = 0;
            }

            public Bank_Account(string fullname,string nationalid , string phonenumber,decimal balance)
            {
                FullName = fullname;
                NationalID = nationalid;
                PhoneNumber = phonenumber;
                Balance = balance;
                CreatedDate = DateTime.Now;
                _accountNumber=_nextAccount++;
                Address = "Not inserted";
            }

            public Bank_Account(string fullname, string nationalid, string phonenumber ,string address)
            {
                FullName = fullname;
                NationalID = nationalid;
                PhoneNumber = phonenumber;
                Balance = 0;
                Address = address;
                CreatedDate=DateTime.Now;
                _accountNumber = _nextAccount++;
            }

            // Methods -------------------

            public void ShowAccountDetails()
            {
                Console.WriteLine("----- Account Info -----");
                Console.WriteLine($"Bank Code     : {BankCode}");
                Console.WriteLine($"Account No    : {_accountNumber}");
                Console.WriteLine($"Full Name     : {FullName}");
                Console.WriteLine($"National ID   : {NationalID}");
                Console.WriteLine($"Phone Number  : {PhoneNumber}");
                Console.WriteLine($"Address       : {Address}");
                Console.WriteLine($"Balance       : ${Balance}");
                Console.WriteLine($"Created Date  : {CreatedDate}");
                Console.WriteLine("-------------------------\n");
            }

            public bool IsValidNationalID()
            {
             
                return (!string.IsNullOrWhiteSpace(_nationalID) && _nationalID.Length == 14
                    && ulong.TryParse(_nationalID, out _));
             
            }
            public bool IsValidPhoneNumber()
            {

                return (!string.IsNullOrWhiteSpace(_phoneNumber) &&
                    _phoneNumber.Length == 11 && _phoneNumber.StartsWith("01") &&
                    ulong.TryParse(_phoneNumber, out _));

            }


        }
}

