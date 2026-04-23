partial class Patient
{
    public static Patient[] GetPatientsByDiagnosis(Patient[] patients, string diagnosis)
    {
        return patients.Where(p => p.Diagnosis.Equals(diagnosis, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    public static Patient GetOldestPatient(Patient[] patients)
    {
        return patients.OrderByDescending(p => p.Age).FirstOrDefault();
    }
}
