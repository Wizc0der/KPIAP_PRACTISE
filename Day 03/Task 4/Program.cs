using static System.Console;

var hospital = new Hospital
{
    Patients = new Patient[]
    {
                new Patient("Иванов", 45, "Грипп"),
                new Patient("Петрова", 32, "ОРВИ"),
                new Patient("Сидоров", 67, "Грипп"),
                new Patient("Козлова", 28, "Ангина"),
                new Patient("Новиков", 55, "Грипп")
    }
};

WriteLine("=== ВСЕ ПАЦИЕНТЫ ===");
foreach (var p in hospital.Patients) WriteLine(p);

WriteLine("\n=== ПАЦИЕНТЫ С ДИАГНОЗОМ 'Грипп' ===");
var fluPatients = Patient.GetPatientsByDiagnosis(hospital.Patients, "Грипп");
foreach (var p in fluPatients) WriteLine(p);

WriteLine("\n=== САМЫЙ СТАРШИЙ ПАЦИЕНТ ===");
var oldest = Patient.GetOldestPatient(hospital.Patients);
WriteLine(oldest != null ? oldest.ToString() : "Нет пациентов");