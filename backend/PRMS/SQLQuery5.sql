with amir as (

SELECT TOP (200) Appointment.Appointment_Date, Appointment.Appointment_Description, Appointment.Appointment_Name, Doctor.Doctor_FirstName, Doctor.Doctor_MiddleName, Doctor.specialization_Id, Specializations.Specialization_Name, 
                  Pateient.Patient_FirstName, Hospital.Hospital_Name
FROM     Appointment INNER JOIN
                  Doctor ON Appointment.Doctor_Id = Doctor.Doctor_Id INNER JOIN
                  Pateient ON Appointment.Patient_Id = Pateient.Patient_ID INNER JOIN
                  Specializations ON Doctor.specialization_Id = Specializations.Specialization_Id INNER JOIN
                  Hospital ON Appointment.Hospital_Id = Hospital.Hospital_Id AND Doctor.Hospital_Id = Hospital.Hospital_Id ) 
select Doctor_FirstName , count(*) as  [patient number] from amir where Hospital_Name = 'Girum' and Doctor_FirstName = 'james' group by Doctor_FirstName
