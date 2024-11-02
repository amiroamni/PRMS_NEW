use PRMS_Database 

SELECT 
    d.Doctor_Id,
    d.Doctor_FirstName,
    h.Hospital_Name,
    h.Hospital_Location
FROM 
    [PRMS_Database].[dbo].[Doctor] d
INNER JOIN 
    [PRMS_Database].[dbo].[Hospital] h
ON 
    d.Hospital_Id = h.Hospital_Id;
