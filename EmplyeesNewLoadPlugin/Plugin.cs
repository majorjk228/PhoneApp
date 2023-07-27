using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace EmplyeesNewLoadPlugin
{
    [Author(Name = "Alexandr Ivanov")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Прогрузился");

            var employeesList = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(File.ReadAllText("data.json"));

            List<EmployeesDTO> employees = new List<EmployeesDTO>();

            foreach (var user in employeesList.Users)
            {
                employees.Add(new EmployeesDTO()
                {
                    Name = $"{user.FirstName}  {user.LastName}"
                });
            }

            logger.Info($"Loaded {employeesList.Users.Count()} employees");

            return employees.Cast<DataTransferObject>();
        }
    }
}
