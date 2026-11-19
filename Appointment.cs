using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMS
{
    class Appointment
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Description { get; set; }

        public Appointment(string doctorId, string patientId,string description)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Description = description;
        }
    }
}
