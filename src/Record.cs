public class Record
{
    public int StudentId { get; set; }
    public string Surname { get; set; }
    public string Group { get; set; }
    public double AverageScore { get; set; }

    public Record(int studentId, string surname, string group, double averageScore)
    {
        StudentId = studentId;
        Surname = surname;
        Group = group;
        AverageScore = averageScore;
    }

    public override string ToString()
    {
        return $"ID: {StudentId}, Прізвище: {Surname}, Група: {Group}, Бал: {AverageScore}";
    }
}