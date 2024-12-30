export interface Patient {
    FirstName:string
    MiddleName:string
    LastName:string
    age:string
    gender:string
    phoneNumber:string
    email:string
    address:string
}

export interface Referral {
    PatientID: string;
    DoctorID: string;
    ClinicID: string;
    hospitalID: string;
    department?: string;
    clinicalFindings: string;
    diagnosisResult: string;
    attachment?: File | null;
    reasonsForReferral: string;
    rxGiven?: string;
    status: "Pending" | "Approved" | "Rejected";  
}