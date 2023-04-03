using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; 

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
             Doctor d1 = new Doctor();
             
            DoctorGen ds = new DoctorGen();
             
             
             Console.WriteLine($"Id,FirstName,LastName,Age,Salary");
             for (int i = 0; i < 50; i++)
             {
                 d1 = ds.GenerateDoctor();
                 Console.WriteLine($"{d1.Id},{d1.FirstName},{d1.LastName},{d1.Age},{d1.Salary:F2}");
             
             }

           // Patient p1 = new Patient();
           // PatientGen pg = new PatientGen();


           // Console.WriteLine($"PatientId,FirstName,LastName,Age,EntryDate,ExitDate,AddressId,RoomId,DoctorId");
           // for (int i = 0; i < 200; i++)
           // {
           //     p1 = pg.GeneratePatient();

           //     Console.WriteLine($"{p1.PatientId},{p1.FirstName},{p1.LastName},{p1.Age},{p1.EntryDate},{p1.ExitDate},{p1.AddressId},{p1.RoomId},{p1.DoctorId}");
           // }

           // Room room = new Room();
           // RoomGen roomGen = new RoomGen();

           //List<Room> roomList = new List<Room>();

           //for (int i = 0; i < 30; i++)
           // {
           //     room = roomGen.GenerateRoom();
           //     roomList.Add(room);
           //     //Console.WriteLine($"{room.RoomId},{room.RoomName}");
           // }
           //
           //List<Room> uniqueRooms = new List<Room>();
           // uniqueRooms = roomList.Distinct().ToList();


           // Console.WriteLine($"RoomId,RoomName");
           // foreach (var i in uniqueRooms)
           // {
           //     Console.WriteLine($"{i.RoomId},{i.RoomName}");
           // }

            //Address a1 = new Address();
            //AddressGen ag = new AddressGen();
            //
            //Console.WriteLine($"Id,Name,Country,City,PostalCode");
            //for (int i = 0; i < 30; i++)
            //{
            //    a1 = ag.GeneratePatient();
            //    Console.WriteLine($"{a1.Id},{a1.Name},{a1.Country},{a1.City},{a1.PostalCode}");
            //}


           // Console.WriteLine($"PatientId,FirstName,LastName,Age,EntryDate,ExitDate,DoctorId");
           // for (int i = 0; i < 200; i++)
           // {
           //     p1 = pg.GeneratePatient();
           //     Console.WriteLine($"{p1.PatientId},{p1.FirstName},{p1.LastName},{p1.Age},{p1.EntryDate},{p1.ExitDate}, {p1.DoctorId}");
           // }

        }

    }

} 

public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

    }

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime ExitDate { get; set; }
    public int AddressId { get; set; }
    public int RoomId { get; set; }
    public int DoctorId { get; set; }

}

public class Room
{
    public int RoomId { get; set; }
    public string RoomName { get; set; }
}

enum Floor
{
    A,
    B,
    C,
    D
}
public class Address
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }


}
public class AddressGen
{
    Faker<Address> addressFake;

    public AddressGen()
    {
        Randomizer.Seed = new Random(123);

        addressFake = new Faker<Address>()
            .RuleFor(u => u.Id, f => (f.IndexFaker) + 1)
            .RuleFor(u => u.Name, f => f.Address.StreetAddress())
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.PostalCode, f => f.Address.ZipCode()
            );
    }
    public Address GeneratePatient()
    {
        return addressFake.Generate();
    }
}

public class PatientGen 
    {
        Faker<Patient> patientFake;

        public PatientGen()
        {
            Randomizer.Seed = new Random(123);

            patientFake = new Faker<Patient>()
                .RuleFor(u => u.PatientId, f => (f.IndexFaker)+ 1)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Age, f => f.Random.Int(30, 75))
                .RuleFor(u => u.EntryDate, f => f.Date.Past().Date)
                .RuleFor(u => u.ExitDate, f => f.Date.Future().Date)
                .RuleFor(u => u.AddressId, f => f.Random.Int(1,30 ))
                .RuleFor(u => u.RoomId, f => f.Random.Int(1,30 ))
                .RuleFor(u => u.DoctorId, f => f.Random.Int(1,50 )
                );
        }

        public Patient GeneratePatient()
        {
            return patientFake.Generate();
        }
    }

    public class RoomGen 
    {
        Faker<Room> RoomFake;

        public RoomGen()
        {
            Randomizer.Seed = new Random(123);

            RoomFake= new Faker<Room>()
                .RuleFor(u => u.RoomId, f => (f.IndexFaker)+ 1)
                .RuleFor(u => u.RoomName, f => 
                {
                    string floor = f.PickRandom<Floor>().ToString();
                    return floor + f.Random.Replace("##");
                }
                );
        }

        public Room GenerateRoom()
        {
            return RoomFake.Generate();
        }
    }

    public class  DoctorGen
    {
        Faker<Doctor> doctorFake;

        public DoctorGen()
        {
            Randomizer.Seed = new Random(123);

            doctorFake = new Faker<Doctor>()
                .RuleFor(u => u.Id, f => (f.IndexFaker)+ 1)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Age, f => f.Random.Int(30, 75))
                .RuleFor(u => u.Salary, f => f.Random.Float(40000, 90000)
                );
        }

        public Doctor GenerateDoctor()
        {
            return doctorFake.Generate();
        }
    }


