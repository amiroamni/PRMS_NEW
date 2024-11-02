
select a.Doctor_FirstName , a.Doctor_Id , a.Doctor_LastName , a.Doctor_Phone ,b.Hospital_Name

into #kedir
 From [dbo].Doctor  a
 inner join [dbo].Hospital b on b.Hospital_Id = a.Hospital_Id  


select * from  #kedir 
 drop table #kedir
truncate table #kedir



select A.Appointment_Name , D.Doctor_FirstName
 From [dbo].Appointment A  
 inner join [dbo].Doctor D on A.Appointment_Id = D.Doctor_Id

