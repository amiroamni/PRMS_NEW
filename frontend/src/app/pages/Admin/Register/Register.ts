export interface Hospital{

    HospitalID:Number
    hospitalName:string
    hospitalType:string
    hospitalPhone:Number
    hospitalEmail:string
    hospitalLocation:string

    // WorkPermit_license:string
    // Hospital_License:string
}

export interface Clinic{
    ClinicID:Number
    clinicName:string
    clinicType:string
    clinicPhone:string
    clinicEmail:Number
    clinicLocation:string
}

export interface Credential{
    Email:string
    Password:string
}